using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class CityEfMap
    {
        public void Map(CityEntity source, City target)
        {
            target.CityId = source.CityId;
            target.CityCode = source.CityCode;
            target.CityName = source.CityName;
        }

        public void Map(City source, CityEntity target)
        {
            target.CityId = source.CityId;
            target.CityCode = source.CityCode;
            target.CityName = source.CityName;
            if (source.Province != null)
            {
                target.ProvinceId = source.Province.ProvinceId;
            }
        }
    }

}
