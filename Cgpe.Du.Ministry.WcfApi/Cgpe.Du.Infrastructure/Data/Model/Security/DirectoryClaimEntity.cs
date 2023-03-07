using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class DirectoryClaimEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ClaimId { get; set; }

        [MaxLength(50)]
        public string ClaimValue { get; set; }

        public virtual IList<DirectoryRoleClaimEntity> Roles { get; set; }

    }

}
