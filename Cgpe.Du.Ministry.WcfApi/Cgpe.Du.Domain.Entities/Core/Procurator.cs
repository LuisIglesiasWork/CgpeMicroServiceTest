using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cgpe.Du.Domain.Entities
{

    public class Procurator
    {

        public Guid ProcuratorId { get; set; }

        public MinistryIntegrationStatesEnum StateInMinistry { get; set; }



        #region Personal Data

        public string LastNifSentToMinistry { get; set; }

        public string Nif { get; set; }

        public string FirstName { get; set; }

        public string SecondName1 { get; set; }

        public string SecondName2 { get; set; }

        public string BirthCity { get; set; }

        public DateTime? BirthDate { get; set; }

        public Sex Sex { get; set; }

        public Address PersonalAddress { get; set; }

        public List<IdentificationDocumentFile> IdentificationDocumentFiles { get; set; }
        #endregion

        #region Professional Data

        public string UniqueNumber { get; set; }

        public DateTime? GraduadedLawDegreeDate { get; set; }

        public DateTime? LicencedLawDegreeDate { get; set; }

        public DateTime? MasterDate { get; set; }

        public DateTime? ProcuratorDegreeDate { get; set; }

        public Int32? LicencedLawDegreeAvailableCertificateYear { get; set; }
        public Int32? LicencedLawDegreeAvailableCertificateMonth { get; set; }
        public Int32? LicencedLawDegreeAvailableCertificateDayOfMonth { get; set; }

        public string OtherStudies { get; set; }

        public List<Suspension> Suspensions { get; set; }

        public List<Language> ProcuratorLanguages { get; set; }

        public bool DontSendNotifications { get; set; }

        #endregion

        public List<AssociationProcurator> AssociationsProcurators { get; set; }


        public List<Contact> PersonalContacts { get; set; }

        public List<Position> Positions { get; set; }

        public List<Honour> Honours { get; set; }

        public List<ProcuratorAdherence> ProcuratorAdherences { get; set; }

        public string Observations { get; set; }



        #region Estado de solicitud de alta

        public CreationStateEnum CreationStateId { get; set; }
        public DateTime? CreationRequestDate { get; set; }
        public DateTime LatestCreationStateDate { get; set; }
        public Guid CreatorUserId { get; set; }
        public Guid? CreatorAssociationId { get; set; }
        public Association CreatorAssociation { get; set; }
        public string CreationStateReason { get; set; }

        #endregion Estado de solicitud de alta




        public bool IsInAssociation(Guid AssociationId)
        {
            if (AssociationsProcurators == null || !AssociationsProcurators.Any())
                return false;
            AssociationProcurator associationProcurator = (from ap in AssociationsProcurators where ap.Association.AssociationId == AssociationId select ap).FirstOrDefault();
            if (associationProcurator == null)
                return false;
            return true;
        }


        public DateTime? GetCurrentThirdPartySuspensionDate()
        {

            if (CheckIfAnyThirdPartySuspensions())
            {
                DateTime today = new DateTime();
                return this.Suspensions.Where(s => s.StartDate <= today && (!s.EndDate.HasValue || s.EndDate >= today)).Select(c => c.StartDate).FirstOrDefault();
            }

            return null;
        }

        public void Validate()
        {
            // DATOS PERSONALES

            // NIF: obilgatorio y con patrón correcto
            if (string.IsNullOrWhiteSpace(this.Nif))
            {
                throw new Exception(Resources.NifRequiredValidation);
            }
            else
            {
                Regex nifRegex = new Regex(@"^\s*((\d{6,9}[A-Z\u00D1])|([A-Z\u00D1]\d{7}[A-Z\u00D1]))\s*$");
                if (!string.IsNullOrWhiteSpace(this.Nif) && !nifRegex.IsMatch(this.Nif))
                    throw new Exception(Resources.NifInvalidValidation);
            }

            // Nombre: obligatorio y con patrón texto (que, en este caso, es al menos un carácter que no sea un whitespace, o sea que es redundante con lo anterior).
            Regex nameAndSurnamesRegex = new Regex(@"^.*\S{1,}.*$");

            if (string.IsNullOrWhiteSpace(this.FirstName))
            {
                throw new Exception(Resources.NameRequiredValidation);
            }
            else
            {
                if (!nameAndSurnamesRegex.IsMatch(this.FirstName))
                {
                    throw new Exception(Resources.NameInvalidValidation);
                }
                if (this.FirstName.Length > 50)
                {
                    throw new Exception(string.Format(Resources.NameTooLongValidation, 50));
                }
            }

            // Primer apellido: obligatorio y con patrón texto
            if (string.IsNullOrWhiteSpace(this.SecondName1))
            {
                throw new Exception(Resources.SecondName1RequiredValidation);
            }
            else
            {
                if (!nameAndSurnamesRegex.IsMatch(this.SecondName1))
                {
                    throw new Exception(Resources.SecondName1InvalidValidation);
                }
                if (this.SecondName1.Length > 50)
                {
                    throw new Exception(string.Format(Resources.SecondName1TooLongValidation, 50));
                }
            }

            // Segundo apellido: NO obligatorio; si va relleno, debe seguir patrón texto
            if (!string.IsNullOrWhiteSpace(this.SecondName2))
            {
                if (!nameAndSurnamesRegex.IsMatch(this.SecondName2))
                {
                    throw new Exception(Resources.SecondName2InvalidValidation);
                }
                if (this.SecondName2.Length > 50)
                {
                    throw new Exception(string.Format(Resources.SecondName2TooLongValidation, 50));
                }
            }

            // Lugar de nacimiento: obligatorio
            if (string.IsNullOrWhiteSpace(this.BirthCity))
            {
                throw new Exception(Resources.BirthCityRequiredValidation);
            }
            else
            {
                if (this.BirthCity.Length > 50)
                {
                    throw new Exception(string.Format(Resources.BirthCityTooLongValidation, 50));
                }
            }

            // Fecha de nacimiento: obligatoria
            if (this.BirthDate == null)
                throw new Exception(Resources.BirthDateRequiredValidation);

            // FIN DATOS PERSONALES


            // DATOS PROFESIONALES

            // Número único: obligatorio, patrón número único
            if (string.IsNullOrWhiteSpace(this.UniqueNumber))
                throw new Exception(Resources.UniqueNumberOfAssociatedRequiredValidation);
            Regex uniqueNumberRegex = new Regex(@"^\s*P\d{11}\s*$");
            if (!uniqueNumberRegex.IsMatch(this.UniqueNumber))
                throw new Exception(Resources.UniqueNumberOfAssociatedInvalidValidation);

            // Fecha de expedición del título de procurador: obligatoria
            if (this.ProcuratorDegreeDate == null || !this.ProcuratorDegreeDate.HasValue)
            {
                throw new Exception(Resources.ProcuratorDegreeDateRequiredValidation);
            }

            // Es obligatorio rellenar la fecha de expedición de la licenciatura o el grado en derecho o aportar otros estudios. Uno de los tres campos, al menos.
            if ((this.LicencedLawDegreeDate == null || !this.LicencedLawDegreeDate.HasValue)
                && (this.GraduadedLawDegreeDate == null || !this.GraduadedLawDegreeDate.HasValue)
                && string.IsNullOrWhiteSpace(this.OtherStudies))
            {
                throw new Exception(Resources.LawDegreeOrEquivalentRequiredValidation);
            }

            // Si se rellena fecha de grado, hay que rellenar fecha de máster
            if ((this.GraduadedLawDegreeDate != null && this.GraduadedLawDegreeDate.HasValue)
                && (this.MasterDate == null || !this.MasterDate.HasValue))
            {
                throw new Exception(Resources.MasterDegreeDateRequiredValidation);
            }

            //Colegiaciones: el procurator debe estar colegiado en al menos un colegio 
            if (this.AssociationsProcurators == null || this.AssociationsProcurators.Count == 0)
            {
                throw new Exception(Resources.AssociationsProcuratorsRequiredValidation);
            }
            else
            {
                foreach (AssociationProcurator associationProcurator in this.AssociationsProcurators)
                {
                    associationProcurator.Validate();
                }
            }


            //if (this.PersonalAddress != null)
            //{

            //    this.PersonalAddress.Validate();

            //}
            //if (this.PersonalContacts != null)
            //{
            //    foreach (Contact contact in this.PersonalContacts)
            //    {
            //        contact.Validate();
            //    }
            //}

            if (this.Positions != null)
            {
                foreach (Position pos in this.Positions)
                {
                    pos.Validate();
                }
            }

            if (this.Honours != null)
            {
                foreach (Honour hon in this.Honours)
                {
                    hon.Validate();
                }
            }
        }

        public bool CheckIfCanUseSoftwarePlatforms()
        {
            return this.AssociationsProcurators != null && !this.CheckIfAnySuspendedMemberships()
                && !this.CheckIfAnyThirdPartySuspensions() && !this.CheckIfAnyCancelledMemberships()
                && this.AssociationsProcurators.Any(ap => ap.ChekIfCanUseSoftwarePlatforms());
        }

        public bool CheckIfCanUsePrivateWebsite()
        {
            return this.AssociationsProcurators != null && !this.CheckIfAnySuspendedMemberships()
                && !this.CheckIfAnyThirdPartySuspensions() && !this.CheckIfAnyCancelledMemberships()
                && this.AssociationsProcurators.Any(ap => ap.CreationStateId == CreationStateEnum.Accepted && ap.CheckIfCanUsePrivateWebsite());
        }

        public void SetCreationStatusInformation()
        {
            DateTime now = DateTime.UtcNow;

            this.StateInMinistry = MinistryIntegrationStatesEnum.Unregistered;
            this.CreationStateId = CreationStateEnum.Accepted;
            if (this.CreationRequestDate == null || this.CreationRequestDate == DateTime.MinValue)
            {
                this.CreationRequestDate = now;
            }
            if (this.LatestCreationStateDate == null || this.LatestCreationStateDate == DateTime.MinValue)
            {
                this.LatestCreationStateDate = now;
            }

            if (this.AssociationsProcurators != null)
            {
                foreach (AssociationProcurator assoProc in this.AssociationsProcurators)
                {
                    assoProc.CreationStateId = CreationStateEnum.Accepted;
                    if (assoProc.CreationRequestDate == null || assoProc.CreationRequestDate == DateTime.MinValue)
                    {
                        assoProc.CreationRequestDate = now;
                    }
                    if (assoProc.LatestCreationStateDate == null || assoProc.LatestCreationStateDate == DateTime.MinValue)
                    {
                        assoProc.LatestCreationStateDate = now;
                    }
                    assoProc.OrderSituationChanges();
                }
            }
        }

        public bool CheckIfMembershipsCancelledOrSuspended()
        {
            return
                this.CheckIfAnyCancelledMemberships()
                || this.CheckIfAnySuspendedMemberships()
                || this.CheckIfAnyThirdPartySuspensions();
        }

        public bool CheckIfAnyThirdPartySuspensions()
        {
            DateTime today = new DateTime();
            return this.Suspensions != null && this.Suspensions.Any(s => s.StartDate <= today && (!s.EndDate.HasValue || s.EndDate >= today));
        }

        public bool CheckIfAnyCancelledMemberships()
        {
            return this.AssociationsProcurators != null && this.AssociationsProcurators.Any(ap => ap.CreationStateId == CreationStateEnum.Accepted && ap.CheckIfAssociationMembershipCancelled());

        }

        public bool CheckIfAnySuspendedMemberships()
        {
            return this.AssociationsProcurators != null
                && this.AssociationsProcurators.Any(ap => ap.CreationStateId == CreationStateEnum.Accepted
                && ap.CheckIfAssociationMembershipSuspended());
        }

        public bool CheckIfPractisingInAnyAssociation()
        {
            return this.AssociationsProcurators != null
                && this.AssociationsProcurators.Any(ap => ap.CreationStateId == CreationStateEnum.Accepted && ap.CheckIfPractisingWithinAssociation());

        }

        public bool CheckIfClosingInAnyAssociation()
        {
            return this.AssociationsProcurators != null && this.AssociationsProcurators.Any(ap => ap.CreationStateId == CreationStateEnum.Accepted && ap.CheckIfClosingWithinAssociation());

        }
    }

}
