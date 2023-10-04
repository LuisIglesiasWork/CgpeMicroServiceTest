using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cgpe.Du.Domain.Entities
{

    public class DirectoryRole
    {

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        [IgnoreDataMember]
        public List<DirectoryClaim> Claims { get; set; }

        [IgnoreDataMember]
        public List<DirectoryUser> Users { get; set; }

        public DirectoryRole()
        {
            this.Claims = new List<DirectoryClaim>();
            this.Users = new List<DirectoryUser>();
        }

    }

}
