using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cgpe.Du.Domain.Entities
{

    public class Province
    {

        public string ProvinceId { get; set; }

        public string ProvinceName { get; set; }

        public string ProvinceCode { get; set; }

        [IgnoreDataMember]
        public List<City> Cities { get; set; }

        public Province()
        {
            this.Cities = new List<City>();
        }

    }

}
