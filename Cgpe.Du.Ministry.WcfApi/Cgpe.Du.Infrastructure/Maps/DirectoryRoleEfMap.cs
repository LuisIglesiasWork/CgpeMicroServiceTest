using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class DirectoryRoleEfMap
    {

        public void Map(DirectoryRoleEntity source, DirectoryRole target)
        {
            target.RoleId = source.RoleId;
            target.RoleName = source.RoleName;
        }

        public void Map(DirectoryRole source, DirectoryRoleEntity target)
        {
            target.RoleId = source.RoleId;
            target.RoleName = source.RoleName;
        }

    }

}
