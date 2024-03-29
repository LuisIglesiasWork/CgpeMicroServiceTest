﻿using System;


namespace Cgpe.Du.Domain.Entities
{

    public class SituationChange
    {
        public Guid SituationChangeId { get; set; }
        public DateTime OperationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLinkedToRegistration { get; set; }

        public ProcuratorSituation ProcuratorSituation { get; set; }


        public Guid? AssociationProcuratorId { get; set; }
        public Guid? AssociationId { get; set; }
        public Guid? ProcuratorId { get; set; }

        public Procurator Procurator { get; set; }

        public Association Association { get; set; }
        public OperationType OperationType { get; set; }
    }

}
