using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class DirectoryUserRoleEntity
    {

        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual DirectoryUserEntity User { get; set; }

        [Required, ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public virtual DirectoryRoleEntity Role { get; set; }

    }

}
