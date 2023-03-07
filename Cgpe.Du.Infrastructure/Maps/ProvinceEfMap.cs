using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class ProvinceEfMap
    {
        public void Map(ProvinceEntity source, Province target)
        {
            target.ProvinceId = source.ProvinceId;
            target.ProvinceCode = source.ProvinceCode;
            target.ProvinceName = source.ProvinceName;
            if (source.Cities != null && source.Cities.Count > 0)
            {
                CityEfMap cityMap = new CityEfMap();
                target.Cities = new List<City>();
                foreach(CityEntity cityEntity in source.Cities)
                {
                    City aCity = new City();
                    cityMap.Map(cityEntity, aCity);
                    target.Cities.Add(aCity);
                }
            }
        }

        public void Map(Province source, ProvinceEntity target)
        {
            target.ProvinceId = source.ProvinceId;
            target.ProvinceCode = source.ProvinceCode;
            target.ProvinceName = source.ProvinceName;
            if (source.Cities != null && source.Cities.Count > 0)
            {
                CityEfMap cityMap = new CityEfMap();
                target.Cities = new List<CityEntity>();
                foreach (City city in source.Cities)
                {
                    CityEntity aCityEntity = new CityEntity();
                    cityMap.Map(city, aCityEntity);
                    target.Cities.Add(aCityEntity);
                }
            }
        }
    }

}
