using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Cgpe.Du.Domain.Entities
{

    public class Contact
    {

        public Guid ContactId { get; set; }

        public string Value { get; set; }

        public bool IsPublic { get; set; }

        [IgnoreDataMember]
        public Procurator Procurator { get; set; }

        [IgnoreDataMember]
        public Association Association { get; set; }

        [IgnoreDataMember]
        public Address Address { get; set; }

        public ContactType ContactType { get; set; }

        public bool IsRequired { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Value))
            {
                if (this.ContactType.TypeId == ContactTypes.Switchboard)
                    throw new Exception(Resources.CentralitaRequiredValidation);
                else if (this.ContactType.TypeId == ContactTypes.Email)
                    throw new Exception(Resources.EmailRequiredValidation);
                else if (this.ContactType.TypeId == ContactTypes.Fax)
                    throw new Exception(Resources.FaxRequiredValidation);
                else if (this.ContactType.TypeId == ContactTypes.Mobile)
                    throw new Exception(Resources.MobileRequiredValidation);
                else if (this.ContactType.TypeId == ContactTypes.Phone)
                    throw new Exception(Resources.PhoneRequiredValidation);
                else if (this.ContactType.TypeId == ContactTypes.Web)
                    throw new Exception(Resources.WebRequiredValidation);
            }
            if (this.Association == null && this.ContactType.TypeId == ContactTypes.Switchboard)
            {
                Regex regex = new Regex(@"^\s*\+?(\(?34\)?)?\s?\d{9}.*\s*$");
                if (!regex.IsMatch(this.Value))
                    throw new Exception(Resources.CentralitaInvalidValidation);
            }
            else if (this.ContactType.TypeId == ContactTypes.Email)
            {
                Regex regex = new Regex(@"^\s*[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\s*$");
                if (!regex.IsMatch(this.Value))
                    throw new Exception(Resources.EmailInvalidValidation);
            }
            else if (this.ContactType.TypeId == ContactTypes.Fax)
            {
                // Por petición del cliente, quito la validación de patrón de fax. Quieren poner
                // cosas como "no consta".
                //Regex regex = new Regex(@"^\s*\+?(\(?34\)?)?\s?\d{9}\s*$");
                //if (!regex.IsMatch(this.Value))
                //    throw new Exception(Resources.FaxInvalidValidation);
            }
            else if (this.Association == null && this.ContactType.TypeId == ContactTypes.Mobile)
            {
                Regex regex = new Regex(@"^\s*\+?(\(?34\)?)?\s?(6|7)\d{8}\s*$");
                if (!regex.IsMatch(this.Value))
                    throw new Exception(Resources.MobileInvalidValidation);
            }
            else if (this.ContactType.TypeId == ContactTypes.Phone)
            {
                Regex regex = new Regex(@"^\s*\+?(\(?34\)?)?\s?\d{9}\s*$");
                if (!regex.IsMatch(this.Value))
                    throw new Exception(Resources.PhoneInvalidValidation);
            }
            else if (this.Association == null && this.ContactType.TypeId == ContactTypes.Web)
            {
                Regex regex = new Regex(@"^(?:(?:https?|ftp)://)(?:\S+(?::\S*)?@)?(?:(?!10(?:\.\d{1,3}){3})(?!127(?:\.\d{1,3}){3})(?!169\.254(?:\.\d{1,3}){2})(?!192\.168(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]+-?)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:/[^\s]*)?$");
                if (!regex.IsMatch(this.Value))
                    throw new Exception(Resources.WebInvalidValidation);
            }
        }

    }

}
