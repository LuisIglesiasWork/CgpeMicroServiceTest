using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class CityEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CityId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(30)]
        public string CityName { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(12, MinimumLength = 12)]
        public string CityCode { get; set; }

        [Required, ForeignKey("Province")]
        public string ProvinceId { get; set; }

        public virtual ProvinceEntity Province { get; set; }

    }

}
