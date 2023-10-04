using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AddressTypeEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TypeId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string TypeName { get; set; }

    }

}
