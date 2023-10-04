using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Cgpe.Du.Domain.Entities
{

    public class DirectoryUser : ClaimsPrincipal
    {
        private const int nameMaxLength = 150;


        private X509Certificate2 clientCertificate;

        public string UserId { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string SecondName1 { get; set; }

        public string SecondName2 { get; set; }

        public string Nif { get; set; }

        public bool IsInitialized { get; set; }

        public List<DirectoryUserCertificate> DirectoryUserCertificates { get; set; }

        public List<DirectoryRole> DirectoryRoles { get; set; }

        public Association Association { get; set; }

        public X509Certificate2 ClientCertificate { get { return clientCertificate; } }

        public DirectoryUser(IIdentity identity, X509Certificate2 clientCertificate) : base(identity)
        {
            this.clientCertificate = clientCertificate;
            this.DirectoryUserCertificates = new List<DirectoryUserCertificate>();
            this.DirectoryRoles = new List<DirectoryRole>();
            this.IsInitialized = false;
            Nif = (identity as ClaimsIdentity)?.FindFirst(x => x.Type == ClaimTypes.Name).Value;
        }

        public DirectoryUser()
        {
        }

        public void Validate()
        {
            Regex nameAndSurnamesRegex = new Regex(@"^.*\S{1,}.*$");
            Regex nifRegex = new Regex(@"^\s*((\d{6,9}[A-Z\u00D1])|([A-Z\u00D1]\d{7}[A-Z\u00D1]))\s*$");
            Regex cifRegex = new Regex(@"^[a-zA-Z]{1}\d{7}[a-zA-Z0-9]{1}$");

            if (string.IsNullOrWhiteSpace(this.Nif))
            {
                throw new Exception(Resources.NifRequiredValidation);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.Nif) && !nifRegex.IsMatch(this.Nif) && !cifRegex.IsMatch(this.Nif))
                {
                    throw new Exception(Resources.NifInvalidValidation);
                }
            }

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
                if (this.FirstName.Length > nameMaxLength)
                {
                    throw new Exception(string.Format(Resources.NameTooLongValidation, 150));
                }
            }


            // Primer apellido: No obligatorio y con patrón texto
            if (!string.IsNullOrWhiteSpace(this.SecondName1))
            {
                if (!nameAndSurnamesRegex.IsMatch(this.SecondName1))
                {
                    throw new Exception(Resources.SecondName1InvalidValidation);
                }
                if (this.SecondName1.Length > nameMaxLength)
                {
                    throw new Exception(string.Format(Resources.SecondName1TooLongValidation, nameMaxLength));
                }
            }

            // Segundo apellido: NO obligatorio; si va relleno, debe seguir patrón texto
            if (!string.IsNullOrWhiteSpace(this.SecondName2))
            {
                if (!nameAndSurnamesRegex.IsMatch(this.SecondName2))
                {
                    throw new Exception(Resources.SecondName2InvalidValidation);
                }
                if (this.SecondName2.Length > nameMaxLength)
                {
                    throw new Exception(string.Format(Resources.SecondName2TooLongValidation, nameMaxLength));
                }
            }

            // Al menos un rol
            if (this.DirectoryRoles == null || this.DirectoryRoles.Count == 0)
            {
                throw new Exception(Resources.RoleAtLeastOneRequiredValidation);
            }
            else
            {
                if (this.DirectoryRoles.Where(r => r.RoleId == DirectoryRoleTypes.Colegio.Key).FirstOrDefault() != null)
                {
                    if (this.DirectoryRoles.Where(r => r.RoleId == DirectoryRoleTypes.Consejo.Key).FirstOrDefault() != null)
                    {
                        throw new Exception(Resources.RolesAssociationAndCpgeIncompatibleValidation);
                    }
                    if (this.Association == null || this.Association.AssociationId == string.Empty)
                    {
                        throw new Exception(Resources.AssociationRequiredValidation);

                    }
                }
            }

        }
    }

}
