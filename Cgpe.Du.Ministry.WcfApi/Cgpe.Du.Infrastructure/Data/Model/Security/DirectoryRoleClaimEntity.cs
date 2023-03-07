using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class DirectoryRoleClaimEntity
    {

        [Required, ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public virtual DirectoryRoleEntity Role { get; set; }

        [Required, ForeignKey("Claim")]
        public Guid ClaimId { get; set; }

        public virtual DirectoryClaimEntity Claim { get; set; }

    }

}
