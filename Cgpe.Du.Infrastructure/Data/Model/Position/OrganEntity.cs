using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class OrganEntity
    {
        public OrganEntity()
        {
            this.IsActive = false;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid OrganId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(30)]
        public string OrganCode { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(100)]
        public string OrganName { get; set; }

        [Required, ForeignKey("Organization")]
        public Guid OrganizationId { get; set; }

        public virtual OrganizationEntity Organization { get; set; }

        public bool IsActive { get; set; }

    }

}
