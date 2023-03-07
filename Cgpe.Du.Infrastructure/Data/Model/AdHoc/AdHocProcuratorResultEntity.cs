using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AdHocProcuratorResultEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 RowNum { get; set; }
        public Guid ProcuratorId { get; set; }
        public string FirstName { get; set; }
        public string SecondName1 { get; set; }
        public string SecondName2 { get; set; }
        public int SexId { get; set; }
        public string SexName { get; set; }
        public string Nif { get; set; }
        public string UniqueNumber { get; set; }
        public bool IsPractising { get; set; }
        public string EmailAddresses { get; set; }

        public string FullAddress { get; set; }
        public string ZipCode { get; set; }
        public Guid? ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public Guid? CityId { get; set; }
        public string CityName { get; set; }

    }

}
