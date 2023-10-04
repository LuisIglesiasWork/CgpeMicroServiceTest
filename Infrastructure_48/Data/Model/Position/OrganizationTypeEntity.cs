using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class OrganizationTypeEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string OrganizationTypeId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(30)]
        public string OrganizationTypeCode { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(100)]
        public string OrganizationTypeName { get; set; }

    }

}
