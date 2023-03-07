using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class OperationTypeEfMap
    {
        public void Map(OperationTypeEntity source, OperationType target)
        {
            target.TypeId = source.TypeId;
            target.TypeName = source.TypeName;
        }

        public void Map(OperationType source, OperationTypeEntity target)
        {
            target.TypeId = source.TypeId;
            target.TypeName = source.TypeName;
        }
    }

}
