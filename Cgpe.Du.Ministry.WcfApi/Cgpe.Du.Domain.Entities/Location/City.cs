using System;

namespace Cgpe.Du.Domain.Entities
{

    public class City
    {

        public Guid CityId { get; set; }

        public string CityName { get; set; }

        public string CityCode { get; set; }

        public Province Province { get; set; }

    }

}
