using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ProcuratorPositionEntity
    {

        [Required, ForeignKey("Procurator")]
        public Guid ProcuratorId { get; set; }

        public virtual ProcuratorEntity Procurator { get; set; }

        [Required, ForeignKey("Position")]
        public Guid PositionId { get; set; }

        public virtual PositionEntity Position { get; set; }

    }

}
