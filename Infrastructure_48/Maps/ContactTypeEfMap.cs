using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class ContactTypeEfMap
    {
        public void Map(ContactTypeEntity source, ContactType target)
        {
            target.TypeId = source.TypeId;
            target.TypeName = source.TypeName;
        }

        public void Map(ContactType source, ContactTypeEntity target)
        {
            target.TypeId = source.TypeId;
            target.TypeName = source.TypeName;
        }
    }

}
