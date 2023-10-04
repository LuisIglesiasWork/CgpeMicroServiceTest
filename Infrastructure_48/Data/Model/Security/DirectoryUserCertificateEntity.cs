using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class DirectoryUserCertificateEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CertificateId { get; set; }

        public string PublicKey { get; set; }

        public string SerialNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime ActiveTo { get; set; }

        public bool Active { get; set; }

        [Required, ForeignKey("User")]
        public string UserId { get; set; }

        public virtual DirectoryUserEntity User { get; set; }

    }

}
