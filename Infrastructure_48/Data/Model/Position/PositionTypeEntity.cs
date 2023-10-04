using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class PositionTypeEntity
    {
        public PositionTypeEntity()
        {
            this.IsActive = true;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TypeId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(30)]
        public string TypeCode { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(100)]
        public string TypeName { get; set; }

        public bool IsActive { get; set; }

    }

}
