using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class HonourEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid HonourId { get; set; }

        [Required, ForeignKey("HonourType")]
        public Guid HonourTypeId { get; set; }

        public virtual HonourTypeEntity HonourType { get; set; }

        [MaxLength(150)]
        public string HonourLiteral { get; set; }
        [Column(TypeName = "date")]
        public DateTime HonourDate { get; set; }

        [Required, ForeignKey("Procurator")]
        public Guid ProcuratorId { get; set; }

        public virtual ProcuratorEntity Procurator { get; set; }

    }

}
