using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class AdHocProcuratorResultEfMap
    {
        public void Map(AdHocProcuratorResultEntity source, AdHocProcuratorResult target)
        {
            target.ProcuratorId = source.ProcuratorId;
            target.FirstName = source.FirstName;
            target.SecondName1 = source.SecondName1;
            target.SecondName2 = source.SecondName2;
            target.SexId = source.SexId;
            target.SexName = source.SexName;
            target.Nif = source.Nif;
            target.UniqueNumber = source.UniqueNumber;
            target.IsPractising = source.IsPractising;
            target.EmailAddresses = source.EmailAddresses;
            target.FullAddress = source.FullAddress;
            target.ZipCode = source.ZipCode;
            target.ProvinceId = source.ProvinceId;
            target.ProvinceName = source.ProvinceName;
            target.CityId = source.CityId;
            target.CityName = source.CityName;
        }
    }
}
