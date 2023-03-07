using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class AddressTypeEfMap
    {
        public void Map(AddressTypeEntity source, AddressType target)
        {
            target.TypeId = source.TypeId;
            target.TypeName = source.TypeName;
        }

        public void Map(AddressType source, AddressTypeEntity target)
        {
            target.TypeId = source.TypeId;
            target.TypeName = source.TypeName;
        }
    }

}
