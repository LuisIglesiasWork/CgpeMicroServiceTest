using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class PositionTypeEfMap
    {
        public void Map(PositionTypeEntity source, PositionType target)
        {
            target.TypeId = source.TypeId;
            target.TypeCode = source.TypeCode;
            target.TypeName = source.TypeName;
        }

        public void Map(PositionType source, PositionTypeEntity target)
        {
            target.TypeId = source.TypeId;
            target.TypeCode = source.TypeCode;
            target.TypeName = source.TypeName;
        }
    }

}
