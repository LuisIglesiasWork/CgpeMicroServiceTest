using Cgpe.Du.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class SituationChangeEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid SituationChangeId { get; set; }
        [Column(TypeName = "date")]
        public DateTime OperationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLinkedToRegistration { get; set; }


        [Required, ForeignKey("ProcuratorSituation")]
        public ProcuratorSituationEnum ProcuratorSituationId { get; set; }

        [Required, ForeignKey("Procurator")]
        public Guid ProcuratorId { get; set; }
        [Required, ForeignKey("Association")]
        public Guid AssociationId { get; set; }
        [Required, ForeignKey("AssociationProcurator")]
        public Guid AssociationProcuratorId { get; set; }

        [Required, ForeignKey("OperationType")]
        public Guid OperationTypeId { get; set; }

        public virtual ProcuratorSituationEntity ProcuratorSituation { get; set; }
        public virtual AssociationProcuratorEntity AssociationProcurator { get; set; }
        public virtual AssociationEntity Association { get; set; }
        public virtual ProcuratorEntity Procurator { get; set; }
        public virtual OperationTypeEntity OperationType { get; set; }
    }

}
