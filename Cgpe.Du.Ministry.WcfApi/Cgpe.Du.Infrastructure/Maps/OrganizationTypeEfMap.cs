using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class OrganizationTypeEfMap
    {
        public void Map(OrganizationTypeEntity source, OrganizationType target)
        {
            target.OrganizationTypeId = source.OrganizationTypeId;
            target.OrganizationTypeCode = source.OrganizationTypeCode;
            target.OrganizationTypeName = source.OrganizationTypeName;
        }

        public void Map(OrganizationType source, OrganizationTypeEntity target)
        {
            target.OrganizationTypeId = source.OrganizationTypeId;
            target.OrganizationTypeCode = source.OrganizationTypeCode;
            target.OrganizationTypeName = source.OrganizationTypeName;
        }
    }

}
