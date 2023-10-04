using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class PositionEfMap
    {

        public void Map(PositionEntity source, Position target)
        {
            target.PositionId = source.PositionId;

            if (source.Organ != null)
            {
                OrganEfMap organMapper = new OrganEfMap();
                target.Organ = new Organ();
                organMapper.Map(source.Organ, target.Organ);
            }

            if (source.OrganizationType != null)
            {
                OrganizationTypeEfMap organizationTypeMapper = new OrganizationTypeEfMap();
                target.OrganizationType = new OrganizationType();
                organizationTypeMapper.Map(source.OrganizationType, target.OrganizationType);
            }

            if (source.Organization != null)
            {
                OrganizationEfMap organizationMapper = new OrganizationEfMap();
                target.Organization = new Organization();
                organizationMapper.Map(source.Organization, target.Organization);
            }

            if (source.PositionType != null)
            {
                PositionTypeEfMap positionTypeMapper = new PositionTypeEfMap();
                target.PositionType = new PositionType();
                positionTypeMapper.Map(source.PositionType, target.PositionType);
            }

            target.ElectedDate = source.ElectedDate;
            target.FiredDate = source.FiredDate;
        }

        public void Map(Position source, PositionEntity target, string procuratorId, bool isNew = false)
        {
            if (isNew)
            {
                source.PositionId = Guid.NewGuid().ToString();
            }
            target.PositionId = source.PositionId;

            if (procuratorId != null)
            {
                target.ProcuratorId = procuratorId;
            }

            target.OrganizationTypeId = source.OrganizationType.OrganizationTypeId;
            target.OrganizationId = source.Organization.OrganizationId;
            target.OrganId = source.Organ.OrganId;
            target.PositionTypeId = source.PositionType.TypeId;

            target.ElectedDate = source.ElectedDate;
            target.FiredDate = source.FiredDate;
        }
    }
}
