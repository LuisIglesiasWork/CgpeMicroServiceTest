using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class DirectoryRoleEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RoleId { get; set; }

        [MaxLength(50)]
        public string RoleName { get; set; }

        public virtual IList<DirectoryRoleClaimEntity> Claims { get; set; }

        public virtual IList<DirectoryUserRoleEntity> Users { get; set; }

    }

}
