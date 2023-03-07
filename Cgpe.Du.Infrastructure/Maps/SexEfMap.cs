using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class SexEfMap
    {
        public void Map(SexEntity source, Sex target)
        {
            target.SexId = source.SexId;
            target.SexName = source.SexName;
        }

        public void Map(Sex source, SexEntity target)
        {
            target.SexId = source.SexId;
            target.SexName = source.SexName;
        }
    }

}
