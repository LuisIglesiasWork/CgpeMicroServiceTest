using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class OrganEfMap
    {
        public void Map(OrganEntity source, Organ target)
        {
            target.OrganId = source.OrganId;
            target.OrganCode = source.OrganCode;
            target.OrganName = source.OrganName;
            target.OrganizationId = source.OrganizationId;
        }

        public void Map(Organ source, OrganEntity target)
        {
            target.OrganId = source.OrganId;
            target.OrganCode = source.OrganCode;
            target.OrganName = source.OrganName;
            target.OrganizationId = source.OrganizationId;
        }
    }

}
