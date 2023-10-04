using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class WayTypeEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TypeId { get; set; }

        [StringLength(2, MinimumLength = 2)]
        public string TypeCode { get; set; }

        [Required(AllowEmptyStrings =false), MaxLength(30)]
        public string TypeName { get; set; }

    }

}
