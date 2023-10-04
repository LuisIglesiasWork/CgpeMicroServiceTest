using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Cgpe.Du.Domain.Entities
{

    public class Association
    {
        private const int nameMaxLength = 50;

        public string AssociationId { get; set; }

        public string Name { get; set; }

        public string AssociationCode { get; set; }

        public string Cif { get; set; }

        public List<AssociationProcurator> AssociationProcurators { get; set; }

        public Address HeadquartersAddress { get; set; }

        [IgnoreDataMember]
        public List<DirectoryUser> Users { get; set; }

        public List<Contact> Contacts { get; set; }

        public Association()
        {
            this.AssociationProcurators = new List<AssociationProcurator>();
            this.Users = new List<DirectoryUser>();
            this.Contacts = new List<Contact>();
        }

        public void Validate()
        {
            Regex associationCodeRegex = new Regex(@"^P\d{5}$");
            Regex cifRegex = new Regex(@"^[a-zA-Z]{1}\d{7}[a-zA-Z0-9]{1}$");
            Regex nameAndSurnamesRegex = new Regex(@"^.*\S{1,}.*$");


            if (string.IsNullOrWhiteSpace(this.AssociationCode))
            {
                throw new Exception(Resources.AssociationCodeRequiredValidation);
            }
            else
            {
                if (!associationCodeRegex.IsMatch(this.AssociationCode))
                {
                    throw new Exception(Resources.AssociationCodeInvalidValidation);
                }
            }

            if (string.IsNullOrWhiteSpace(this.Cif))
            {
                throw new Exception(Resources.CifRequiredValidation);
            }
            else
            {
                if (!cifRegex.IsMatch(this.Cif))
                {
                    throw new Exception(Resources.CifInvalidValidation);
                }
            }


            // Primer apellido: No obligatorio y con patrón texto
            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                if (!nameAndSurnamesRegex.IsMatch(this.Name))
                {
                    throw new Exception(Resources.NameInvalidValidation);
                }
                if (this.Name.Length > nameMaxLength)
                {
                    throw new Exception(string.Format(Resources.NameTooLongValidation, nameMaxLength));
                }
            }

            if (this.HeadquartersAddress != null)
            {
                // si valido esto van a reventar todas las direcciones que hay en el sistema.
                //this.HeadquartersAddress.Validate();
            }

            if (this.Contacts != null)
            {
                foreach (Contact contact in this.Contacts)
                {
                    contact.Validate();
                }
            }
        }

    }

}
