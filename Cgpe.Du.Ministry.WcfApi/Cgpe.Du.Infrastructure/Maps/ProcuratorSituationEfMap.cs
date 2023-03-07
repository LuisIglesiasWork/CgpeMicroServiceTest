using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{
    internal class ProcuratorSituationEfMap
    {
        public void Map(ProcuratorSituationEntity source, ProcuratorSituation target)
        {
            target.ProcuratorSituationId = source.ProcuratorSituationId;
            target.ProcuratorSituationName = source.ProcuratorSituationName;
        }

        public void Map(ProcuratorSituation source, ProcuratorSituationEntity target)
        {
            target.ProcuratorSituationId = source.ProcuratorSituationId;
            target.ProcuratorSituationName = source.ProcuratorSituationName;
        }
    }

}
