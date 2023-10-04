using System;
using System.Collections.Generic;

namespace Cgpe.Du.Domain.Entities
{

    public class DirectoryClaim
    {

        public string ClaimId { get; set; }

        public string ClaimValue { get; set; }

        public List<DirectoryRole> Roles { get; set; }

        public DirectoryClaim()
        {
            this.Roles = new List<DirectoryRole>();
        }

    }

}
