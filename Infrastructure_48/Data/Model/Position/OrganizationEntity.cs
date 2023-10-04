using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class OrganizationEntity
    {
        public OrganizationEntity()
        {
            this.IsActive = true;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string OrganizationId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(30)]
        public string OrganizationCode { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(100)]
        public string OrganizationName { get; set; }

        [Required, ForeignKey("OrganizationType")]
        public string OrganizationTypeId { get; set; }

        public virtual OrganizationTypeEntity OrganizationType { get; set; }

        public bool IsActive { get; set; }

    }

}
