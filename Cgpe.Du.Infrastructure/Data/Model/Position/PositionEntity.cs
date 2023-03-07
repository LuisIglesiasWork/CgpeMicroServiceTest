using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class PositionEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PositionId { get; set; }

        [Required, ForeignKey("Procurator")]
        public Guid ProcuratorId { get; set; }

        public virtual ProcuratorEntity Procurator { get; set; }

        [Required, ForeignKey("OrganizationType")]
        public Guid OrganizationTypeId { get; set; }

        public virtual OrganizationTypeEntity OrganizationType { get; set; }

        [Required, ForeignKey("Organization")]
        public Guid OrganizationId { get; set; }

        public virtual OrganizationEntity Organization { get; set; }

        [Required, ForeignKey("Organ")]
        public Guid OrganId { get; set; }

        public virtual OrganEntity Organ { get; set; }

        [Required, ForeignKey("PositionType")]
        public Guid PositionTypeId { get; set; }

        public virtual PositionTypeEntity PositionType { get; set; }

        [Column(TypeName = "date")]
        public DateTime ElectedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FiredDate { get; set; }

    }

}
