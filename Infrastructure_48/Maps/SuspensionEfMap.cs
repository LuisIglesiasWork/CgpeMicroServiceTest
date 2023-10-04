using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class SuspensionEfMap
    {

        public void Map(SuspensionEntity source, Suspension target)
        {
            target.SuspensionId = source.SuspensionId;
            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;
            target.Reason = source.Reason;
            if (source.AssociationAgreeingOnSuspension != null)
            {
                Association asso = new Association();
                AssociationEfMap assoMapper = new AssociationEfMap();
                assoMapper.Map(source.AssociationAgreeingOnSuspension, asso);
                target.AssociationAgreeingOnSuspension = asso;
            }

        }

        public void Map(Suspension source, SuspensionEntity target, string procuratorId, bool isNew = false)
        {
            if (isNew)
            {
                source.SuspensionId = Guid.NewGuid().ToString();
            }
            target.SuspensionId = source.SuspensionId;
            if (procuratorId != null)
            {
                target.ProcuratorId = procuratorId;
            }
            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;
            target.Reason = source.Reason;
            if (source.AssociationAgreeingOnSuspension != null)
            {
                target.AssociationId = source.AssociationAgreeingOnSuspension.AssociationId;
            }
        }
    }
}
