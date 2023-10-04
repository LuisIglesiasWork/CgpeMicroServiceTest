using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class HonourTypeEfMap
    {
        public void Map(HonourTypeEntity source, HonourType target)
        {
            target.TypeId = source.TypeId;
            target.TypeName = source.TypeName;
        }

        public void Map(HonourType source, HonourTypeEntity target)
        {
            target.TypeId = source.TypeId;
            target.TypeName = source.TypeName;
        }
    }

}
