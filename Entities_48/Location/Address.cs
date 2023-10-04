using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Cgpe.Du.Domain.Entities
{

    public class Address
    {

        public string AddressId { get; set; }

        public AddressType AddressType { get; set; }

        public string FullAddress { get; set; }

        public string MailBox { get; set; }

        public string ZipCode { get; set; }

        public bool IsPublic { get; set; }

        public string Door { get; set; }

        public string Floor { get; set; }

        public string Stairway { get; set; }

        public string WayName { get; set; }

        public string WayNumber { get; set; }

        public string BuildingName { get; set; }

        public City City { get; set; }

        public Province Province { get; set; }

        public WayType WayType { get; set; }

        public bool IsReceivingMagazine { get; set; }

        [IgnoreDataMember]
        public Association Association { get; set; }

        [IgnoreDataMember]
        public Procurator Procurator { get; set; }

        public List<Contact> Contacts { get; set; }

        public Address()
        {
        }

        public void Validate()
        {
            if (this.Association != null)
            {
                if (this.Province == null || this.Province.ProvinceId == string.Empty)
                    throw new Exception(Resources.ProvinceRequiredValidation);
                if (this.City == null || this.City.CityId == string.Empty)
                    throw new Exception(Resources.CityRequiredValidation);
            }
            Regex regex = null;
            if (!string.IsNullOrWhiteSpace(this.MailBox))
            {
                regex = new Regex(@"^\s*\d{2,5}\s*$");
                if (!regex.IsMatch(this.MailBox))
                    throw new Exception(Resources.MailBoxInvalidValidation);
            }
            if (!string.IsNullOrWhiteSpace(this.ZipCode))
            {
                regex = new Regex(@"^\s*\d{4,5}\s*$");
                if (!regex.IsMatch(this.ZipCode))
                    throw new Exception(Resources.ZipCodeInvalidValidation);
            }
        }

    }

}
