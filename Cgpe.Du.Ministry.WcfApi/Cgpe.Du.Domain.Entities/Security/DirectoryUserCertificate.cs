using System;
using System.Runtime.Serialization;

namespace Cgpe.Du.Domain.Entities
{

    public class DirectoryUserCertificate
    {

        public Guid CertificateId { get; set; }

        public string PublicKey { get; set; }

        public string SerialNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime ActiveTo { get; set; }

        public bool Active { get; set; }

        [IgnoreDataMember]
        public DirectoryUser User { get; set; }

    }

}
