using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class DirectoryUserEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        
        public bool Active { get; set; }

        [Required, MaxLength(150)]
        public string FirstName { get; set; }

        [MaxLength(150)]
        public string SecondName1 { get; set; }

        [MaxLength(150)]
        public string SecondName2 { get; set; }

        [Required, MaxLength(14)]
        public string Nif { get; set; }

        public virtual IList<DirectoryUserCertificateEntity> DirectoryUserCertificates { get; set; }

        public virtual IList<DirectoryUserRoleEntity> DirectoryRoles { get; set; }

        [ForeignKey("Association")]
        public string AssociationId { get; set; }

        public virtual AssociationEntity Association { get; set; }

    }

}
