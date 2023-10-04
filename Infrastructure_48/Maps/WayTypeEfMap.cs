using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class WayTypeEfMap
    {
        public void Map(WayTypeEntity source, WayType target)
        {
            target.TypeId = source.TypeId;
            target.TypeCode = source.TypeCode;
            target.TypeName = source.TypeName;
        }

        public void Map(WayType source, WayTypeEntity target)
        {
            target.TypeId = source.TypeId;
            target.TypeCode = source.TypeCode;
            target.TypeName = source.TypeName;
        }
    }

}
