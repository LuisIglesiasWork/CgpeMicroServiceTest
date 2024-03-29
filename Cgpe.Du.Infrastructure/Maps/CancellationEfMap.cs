﻿using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class CancellationEfMap
    {

        public void Map(CancellationEntity source, Cancellation target)
        {
            target.CancellationId = source.CancellationId;
            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;
            target.Reason = source.Reason;
        }

        public void Map(Cancellation source, CancellationEntity target, Guid? associationProcuratorId, bool isNew = false)
        {
            if (isNew)
            {
                source.CancellationId = Guid.NewGuid();
            }
            target.CancellationId = source.CancellationId;
            if (associationProcuratorId != null)
            {
                target.AssociationProcuratorId = associationProcuratorId.Value;
            }
            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;
            target.Reason = source.Reason;
            
        }
    }
}
