using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class HonourEfMap
    {

        public void Map(HonourEntity source, Honour target)
        {
            target.HonourId = source.HonourId;

            HonourTypeEfMap honTypeMap = new HonourTypeEfMap();
            HonourType honType = new HonourType();
            honTypeMap.Map(source.HonourType, honType);
            target.HonourType = honType;
            target.HonourLiteral = source.HonourLiteral;
            target.HonourDate = source.HonourDate;
        }

        public void Map(Honour source, HonourEntity target, string procuratorId, bool isNew = false)
        {
            if (isNew)
            {
                source.HonourId = Guid.NewGuid().ToString();
            }

            target.HonourId = source.HonourId;

            if (procuratorId != null)
            {
                target.ProcuratorId = procuratorId;
            }
            target.HonourTypeId = source.HonourType.TypeId;
            target.HonourLiteral = source.HonourLiteral;
            target.HonourDate = source.HonourDate;
        }
    }
}
