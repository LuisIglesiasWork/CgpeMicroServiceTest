using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Ministry.WcfApi.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Cgpe.Du.CrossCuttings;

namespace Cgpe.Du.Ministry.WcfApi.Maps
{
    public class MinistryMapper
    {

        public colegiadosResponseColegiadosActualizacionColegiado[] MapProcurators(List<Procurator> procs, List<Procurator> updateSet)
        {
            List<colegiadosResponseColegiadosActualizacionColegiado> mappedProcs = new List<colegiadosResponseColegiadosActualizacionColegiado>();
            if (updateSet == null)
            {
                updateSet = new List<Procurator>();
            }

            if (procs != null && procs.Count > 0)
            {
                Procurator currentProcurator;
                colegiadosResponseColegiadosActualizacionColegiado mappedProc = new colegiadosResponseColegiadosActualizacionColegiado();
                for (int i = 0; i < procs.Count; i++)
                {
                    currentProcurator = procs[i];
                    mappedProc = this.Map(currentProcurator, updateSet);
                    if (mappedProc != null)
                    {
                        mappedProcs.Add(mappedProc);
                    }
                }
            }
            return mappedProcs.ToArray();
        }

        private colegiadosResponseColegiadosActualizacionColegiado Map(Procurator proc, List<Procurator> updateSet)
        {
            colegiadosResponseColegiadosActualizacionColegiadoOperacion? theOp = this.GetMinistryOperation(proc, updateSet);
            if (theOp.HasValue)
            {
                return new colegiadosResponseColegiadosActualizacionColegiado()
                {
                    operacion = theOp.Value,
                    colegiado = this.Map(proc)
                };
            }

            return null;
        }

        private colegiado Map(Procurator source)
        {

            colegiado target = new colegiado();

            target.nombre = source.FirstName.GetLimitedLengthString(50);
            target.primerApellido = source.SecondName1.GetLimitedLengthString(50);
            target.segundoApellido = source.SecondName2.GetLimitedLengthString(50);
            target.sexo = source.Sex.SexId == SexEnum.Female ? sexo.M : source.Sex.SexId == SexEnum.Male ? sexo.H : sexo.N;

            target.numeroIdentificacion = source.Nif.ToUpper();
            target.tipoIdentificacion = this.GetNifType(target.numeroIdentificacion);

            if (!string.IsNullOrWhiteSpace(source.LastNifSentToMinistry) && !source.LastNifSentToMinistry.Equals(source.Nif))
            {
                target.numeroIdentificacionOld = source.LastNifSentToMinistry;
                target.tipoIdentificacionOld = this.GetNifType(source.LastNifSentToMinistry);
            }

            target.direcciones = this.MapAssociationsProcuratorsAddresses(source.AssociationsProcurators);
            if (target.direcciones != null && target.direcciones.Length == 0)
            {
                target.direcciones = null;
            }

            target.correosElectronicos = this.MapAssociationsProcuratorsContacts(source.AssociationsProcurators, ContactTypes.Email);
            // En el Ministerio esperan al menos un correo electrónico. Por tanto, si el procurador no tiene correo, nos lo inventamos.
            if (target.correosElectronicos != null && target.correosElectronicos.Length == 0)
            {
                target.correosElectronicos = new string[] { "correo_no_existe@cgpe.net" };
            }


            target.faxes = this.MapAssociationsProcuratorsContacts(source.AssociationsProcurators, ContactTypes.Fax);
            if (target.faxes != null && target.faxes.Length == 0)
            {
                target.faxes = null;
            }

            target.telefonos =
                this.MapAssociationsProcuratorsContacts(source.AssociationsProcurators, ContactTypes.Mobile)
                .Concat(this.MapAssociationsProcuratorsContacts(source.AssociationsProcurators, ContactTypes.Phone))
                .Concat(this.MapAssociationsProcuratorsContacts(source.AssociationsProcurators, ContactTypes.Switchboard)).ToArray();
            if (target.telefonos != null && target.telefonos.Length == 0)
            {
                target.telefonos = null;
            }

            target.colegios = this.MapAssociationsProcurators(source.AssociationsProcurators, source.CheckIfMembershipsCancelledOrSuspended());

            if (target.colegios != null && target.colegios.Length == 0)
            {
                target.colegios = null;
            }

            target.situacionProfesional = new situacionProfesional()
            {
                situacion = source.CheckIfCanUseSoftwarePlatforms() ? situacionEjercicio.EJ : situacionEjercicio.NE,
                fechaFinNoEjercicio = default(DateTime),
                fechaIncioNoEjercicio = source.CheckIfCanUseSoftwarePlatforms() ? default(DateTime)
                : (from ap in source.AssociationsProcurators select ap.CurrentSituationDate).Max()
            };

            return target;
        }

        private colegio[] MapAssociationsProcurators(List<AssociationProcurator> source, bool generalSuspension)
        {
            if (source == null || source.Count == 0)
            {
                return new colegio[0];
            }
            else
            {
                colegio[] coles = new colegio[source.Count];
                for (int i = 0; i < source.Count; i++)
                {
                    coles[i] = this.MapAssociationProcurator(source[i], generalSuspension);
                }

                return coles;
            }
        }

        private colegio MapAssociationProcurator(AssociationProcurator source, bool generalSuspension)
        {
            char[] trimChars = "0".ToCharArray();
            colegio target = new colegio();

            target.baja = !source.ChekIfCanUseSoftwarePlatforms() || source.CheckIfAssociationMembershipCancelled() || generalSuspension;
            target.codigoColegio = source.Association.AssociationCode;
            target.numeroColegiado = string.IsNullOrWhiteSpace(source.MemberNumber.TrimStart(trimChars)) ? "Desconocido" : source.MemberNumber.TrimStart(trimChars);
            target.principal = source.IsDefault;

            return target;
        }

        private string[] MapProcuratorContacts(Procurator source, string contactTypeId)
        {
            if (source == null || source.AssociationsProcurators == null || source.AssociationsProcurators.Count == 0)
            {
                return new string[0];
            }
            else
            {
                return this.MapAssociationsProcuratorsContacts(source.AssociationsProcurators, contactTypeId);
            }
        }

        private direccion[] MapProcuratorAddresses(Procurator source)
        {
            if (source == null || source.AssociationsProcurators == null || source.AssociationsProcurators.Count == 0)
            {
                return new direccion[0];
            }
            else
            {
                direccion[] addresses = this.MapAssociationsProcuratorsAddresses(source.AssociationsProcurators);
                if (!addresses.Where(a => a.tipoDireccion == tipoDireccion.PRINCIPAL).Any())
                {
                    addresses[0].tipoDireccion = tipoDireccion.PRINCIPAL;
                }
                return addresses;
            }
        }

        private direccion[] MapAssociationsProcuratorsAddresses(List<AssociationProcurator> source)
        {
            if (source == null || source.Count == 0)
            {
                return new direccion[0];
            }
            else
            {
                direccion[] addressArray = new direccion[0];

                foreach (AssociationProcurator assoProc in source)
                {
                    addressArray = addressArray.Concat(this.MapAssociationProcuratorAddresses(assoProc)).ToArray();
                }

                return addressArray;
            }
        }

        private direccion[] MapAssociationProcuratorAddresses(AssociationProcurator source)
        {
            if (source == null || (
                (source.AssociationProcuratorAddresses == null || source.AssociationProcuratorAddresses.Count == 0)
                && (source.Association.HeadquartersAddress == null))
                )
            {
                return new direccion[0];
            }
            else
            {
                List<Address> addressesToMap = new List<Address>();
                addressesToMap.AddRange(source.AssociationProcuratorAddresses);
                if (source.Association != null && source.Association.HeadquartersAddress != null)
                {
                    // En DU1 sólo se mapeaban las direcciones del colegio, que siempre era 1, aunque en el modelo se contemplara
                    // la posibilidad de varias.
                    addressesToMap.Add(source.Association.HeadquartersAddress);
                }
                return this.MapAddresses(addressesToMap);
            }
        }

        private string[] MapAssociationsProcuratorsContacts(List<AssociationProcurator> source, string contactTypeId)
        {
            if (source == null || source.Count == 0)
            {
                return new string[0];
            }
            else
            {
                string[] contactArray = new string[0];

                foreach (AssociationProcurator assoProc in source)
                {
                    contactArray = contactArray.Concat(this.MapAssociationProcuratorContacts(assoProc, contactTypeId)).ToArray();
                }

                return contactArray;
            }
        }

        private string[] MapAssociationProcuratorContacts(AssociationProcurator source, string contactTypeId)
        {
            if (source == null || source.AssociationProcuratorAddresses == null || source.AssociationProcuratorAddresses.Count == 0)
            {
                return new string[0];
            }
            else
            {
                return this.MapAddressesContacts(source.AssociationProcuratorAddresses, contactTypeId);
            }
        }

        private string[] MapAddressesContacts(List<Address> source, string contactTypeId)
        {
            if (source == null || source.Count == 0)
            {
                return new string[0];
            }
            else
            {
                string[] contactArray = new string[0];
                foreach (Address addr in source)
                {
                    contactArray = contactArray.Concat(this.MapAddressContacts(addr, contactTypeId)).ToArray();
                }
                return contactArray;
            }
        }

        private string[] MapAddressContacts(Address source, string contactTypeId)
        {
            if (source == null || source.Contacts == null || source.Contacts.Count == 0)
            {
                return new string[0];
            }
            else
            {
                return this.MapContacts(source.Contacts, contactTypeId);
            }
        }


        private string[] MapContacts(List<Contact> source, string contactTypeId)
        {
            if (source == null || source.Count == 0)
            {
                return new string[0];
            }
            else
            {
                IEnumerable<string> contacts = source.Where(s => s.ContactType.TypeId == contactTypeId).Select(s => s.Value);
                return contacts.ToArray();
            }
        }

        private direccion[] MapAddresses(List<Address> source)
        {
            if (source == null || source.Count == 0)
            {
                return new direccion[0];
            }
            else
            {

                direccion[] target = new direccion[source.Count];

                for (int i = 0; i < source.Count; i++)
                {
                    if (
                        (string.IsNullOrWhiteSpace(source[i].FullAddress) && string.IsNullOrWhiteSpace(source[i].WayName))
                        || (source[i].Province == null || source[i].Province.ProvinceId == null || source[i].Province.ProvinceId == string.Empty)
                        || (source[i].City == null || source[i].City.CityId == null || source[i].City.CityId == string.Empty)
                        )
                    {
                        continue;
                    }

                    target[i] = this.MapAddress(source[i], target[i]);
                }

                return target;
            }
        }

        private direccion MapAddress(Address source, direccion target)
        {
            if (target == null)
            {
                target = new direccion();
            }
            string spainCountryCode = "724";

            if (string.IsNullOrWhiteSpace(source.WayName) && string.IsNullOrWhiteSpace(source.FullAddress))
            {
                throw new NullReferenceException("Address is not valid. Field \"WayName\" is required.");
            }

            if (source.Province == null || string.IsNullOrWhiteSpace(source.Province.ProvinceCode))
            {
                throw new NullReferenceException("Address is not valid. Field \"Province\" is required.");
            }

            if (source.City == null || string.IsNullOrWhiteSpace(source.City.CityCode))
            {
                throw new NullReferenceException("Address is not valid. Field \"City\" is required.");
            }

            // Pongo como principal la de la sede, porque antes era la única que se mandaba e iba, por tanto, marcada como principal.
            target.tipoDireccion = source.AddressType != null && source.AddressType.TypeId == AddressTypes.Sede ? tipoDireccion.PRINCIPAL : tipoDireccion.ADICIONAL;

            target.datosDireccion = new datosDireccion();

            if (!string.IsNullOrWhiteSpace(source.WayName))
            {
                target.datosDireccion.nombreVia = source.WayName.GetLimitedLengthString(250);
            }
            else
            {
                target.datosDireccion.nombreVia = source.FullAddress.GetLimitedLengthString(250);
            }
            


            target.datosDireccion.pais = spainCountryCode;



            // TIPO "complementoDireccion" del xsd
            target.datosDireccion.numero = source.WayNumber.GetLimitedLengthString(15);
            target.datosDireccion.piso = source.Floor.GetLimitedLengthString(15);
            target.datosDireccion.escalera = source.Stairway.GetLimitedLengthString(15);
            target.datosDireccion.puerta = source.Door.GetLimitedLengthString(15);
            target.datosDireccion.apartadoCorreos = source.MailBox.GetLimitedLengthString(15);
            target.datosDireccion.codigoPostal = source.ZipCode.GetLimitedLengthString(15);




            if (source.WayType != null && !string.IsNullOrWhiteSpace(source.WayType.TypeCode))
            {
                target.datosDireccion.tipoVia = source.WayType.TypeCode;
            }

            if (source.Province != null && !string.IsNullOrWhiteSpace(source.Province.ProvinceCode))
            {
                target.datosDireccion.provincia = source.Province.ProvinceCode;
            }

            if (source.City != null && !string.IsNullOrWhiteSpace(source.City.CityCode))
            {
                target.datosDireccion.localidad = source.City.CityCode;
                target.datosDireccion.municipio = source.City.CityCode.Substring(0, 5);
            }

            return target;
        }


        private tipoIdentificacion GetNifType(string nif)
        {
            Regex regex = new Regex("([ABCDEFGHJKLMNPQRSUVW]{1})(\\d{7})([ABCDEFGHIJ0123456789]{1})");
            Match match = regex.Match(nif);
            if (match.Success)
                return tipoIdentificacion.N;
            regex = new Regex("(\\d{8})([TRWAGMYFPDXBNJZSQVHLCKE]{1})");
            match = regex.Match(nif);
            if (match.Success)
                return tipoIdentificacion.N;
            regex = new Regex("([XYZ]{1})(\\d{7,8})([TRWAGMYFPDXBNJZSQVHLCKE]{1})");
            match = regex.Match(nif);
            if (match.Success)
                return tipoIdentificacion.X;
            regex = new Regex("([KLM]{1})(\\d{7})([ABCDEFGHIJ0123456789]{1})");
            match = regex.Match(nif);
            if (match.Success)
                return tipoIdentificacion.N;
            throw new NotSupportedException("NIF has an unknown format.");
        }

        private colegiadosResponseColegiadosActualizacionColegiadoOperacion? GetMinistryOperation(Procurator proc, List<Procurator> updateSet)
        {
            MinistryIntegrationStatesEnum startState = proc.StateInMinistry;
            MinistryIntegrationStatesEnum endState = MinistryIntegrationStatesEnum.RegisteredSent;
            if (!proc.CheckIfCanUseSoftwarePlatforms())
            {
                endState = MinistryIntegrationStatesEnum.UnregisteredSent;
            }

            if (endState == MinistryIntegrationStatesEnum.UnregisteredSent && (startState == MinistryIntegrationStatesEnum.Registered || startState == MinistryIntegrationStatesEnum.RegisteredSent))
            {
                proc.StateInMinistry = MinistryIntegrationStatesEnum.UnregisteredSent;
                updateSet.Add(proc);
                return colegiadosResponseColegiadosActualizacionColegiadoOperacion.B;
            }
            if (endState == MinistryIntegrationStatesEnum.RegisteredSent && (startState == MinistryIntegrationStatesEnum.Unregistered || startState == MinistryIntegrationStatesEnum.UnregisteredSent))
            {
                proc.StateInMinistry = MinistryIntegrationStatesEnum.RegisteredSent;
                updateSet.Add(proc);
                return colegiadosResponseColegiadosActualizacionColegiadoOperacion.A;
            }
            if (endState == MinistryIntegrationStatesEnum.RegisteredSent && (startState == MinistryIntegrationStatesEnum.Registered || startState == MinistryIntegrationStatesEnum.RegisteredSent))
            {
                updateSet.Add(proc);
                return colegiadosResponseColegiadosActualizacionColegiadoOperacion.M;
            }
            return null;
        }
    }


}