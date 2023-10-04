using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class SituationChangeEfMap
    {

        public void Map(SituationChangeEntity source, SituationChange target)
        {
            target.SituationChangeId = source.SituationChangeId;
            target.OperationDate = source.OperationDate;
            target.CreationDate = source.CreationDate;
            target.IsDeleted = source.IsDeleted;
            target.IsLinkedToRegistration = source.IsLinkedToRegistration;

            if (source.ProcuratorSituation != null)
            {
                ProcuratorSituationEfMap sitMapper = new ProcuratorSituationEfMap();
                target.ProcuratorSituation = new ProcuratorSituation();
                sitMapper.Map(source.ProcuratorSituation, target.ProcuratorSituation);
            }

            target.AssociationProcuratorId = source.AssociationProcuratorId;
            target.AssociationId = source.AssociationId;
            target.ProcuratorId = source.ProcuratorId;

            if(source.Procurator != null)
            {
                // Sólo necesario para informe de uso, por eso sólo cargo estas propiedades.
                target.Procurator = new Procurator();
                target.Procurator.ProcuratorId = source.Procurator.ProcuratorId;
                target.Procurator.FirstName = source.Procurator.FirstName;
                target.Procurator.SecondName1 = source.Procurator.SecondName1;
                target.Procurator.SecondName2 = source.Procurator.SecondName2;
                target.Procurator.Nif = source.Procurator.Nif;
                target.Procurator.UniqueNumber = source.Procurator.UniqueNumber;

            }

            if(source.Association != null)
            {
                // Sólo necesario para informe de uso, por eso sólo cargo estas propiedades.
                target.Association = new Association();
                target.Association.AssociationId = source.Association.AssociationId;
                target.Association.Name = source.Association.Name;
            }

            if (source.OperationType != null)
            {
                target.OperationType = new OperationType();
                new OperationTypeEfMap().Map(source.OperationType, target.OperationType);
            }
        }

        public void Map(SituationChange source, SituationChangeEntity target, string associationProcuratorId, string procuratorId, string associationId, bool isNew = false)
        {
            if (isNew)
            {
                source.SituationChangeId = Guid.NewGuid().ToString();
            }
            target.SituationChangeId = source.SituationChangeId;
            target.OperationDate = source.OperationDate;
            target.CreationDate = source.CreationDate;
            target.IsDeleted = source.IsDeleted;
            target.IsLinkedToRegistration = source.IsLinkedToRegistration;

            if (source.ProcuratorSituation != null)
            {
                target.ProcuratorSituationId = source.ProcuratorSituation.ProcuratorSituationId;
            }
            if(source.OperationType != null)
            {
                target.OperationTypeId = source.OperationType.TypeId;
            }

            if (associationProcuratorId != null)
            {
                source.AssociationProcuratorId = associationProcuratorId;
            }
            if (source.AssociationProcuratorId != null)
            {
                target.AssociationProcuratorId = source.AssociationProcuratorId;
            }

            if (associationId != null)
            {
                source.AssociationId = associationId;
            }
            if (source.AssociationId != null)
            {
                target.AssociationId = source.AssociationId;
            }

            if(procuratorId != null)
            {
                source.ProcuratorId = procuratorId;
            }
            if (source.ProcuratorId != null)
            {
                target.ProcuratorId = source.ProcuratorId;
            }
        }
    }
}
