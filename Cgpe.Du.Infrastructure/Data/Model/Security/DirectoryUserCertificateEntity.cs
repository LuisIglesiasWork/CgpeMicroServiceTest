using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class DirectoryUserCertificateEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid CertificateId { get; set; }

        public string PublicKey { get; set; }

        public string SerialNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime ActiveTo { get; set; }

        public bool Active { get; set; }

        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual DirectoryUserEntity User { get; set; }

    }

}
