using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ProvinceEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProvinceId { get; set; }

        [MaxLength(30)]
        public string ProvinceName { get; set; }

        [StringLength(2, MinimumLength = 2)]
        public string ProvinceCode { get; set; }

        public virtual IList<CityEntity> Cities { get; set; }

    }

}
