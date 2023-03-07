using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class OrganizationEfMap
    {
        public void Map(OrganizationEntity source, Organization target)
        {
            target.OrganizationId = source.OrganizationId;
            target.OrganizationCode = source.OrganizationCode;
            target.OrganizationName = source.OrganizationName;
            target.OrganizationTypeId = source.OrganizationTypeId;
        }

        public void Map(Organization source, OrganizationEntity target)
        {
            target.OrganizationId = source.OrganizationId;
            target.OrganizationCode = source.OrganizationCode;
            target.OrganizationName = source.OrganizationName;
            target.OrganizationTypeId = source.OrganizationTypeId;
        }
    }

}
