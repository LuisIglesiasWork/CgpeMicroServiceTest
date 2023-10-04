using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public class Audit
    {

        public string AuditId { get; set; }

        public string UserId { get; set; }

        public string RelatedTreeId { get; set; }

        public string RelatedTreeType { get; set; }

        public string CertificateSn { get; set; }

        public string SignedData { get; set; }

        public string OperationType { get; set; }

        public DateTime CreationDate { get; set; }

        public string DataBefore { get; set; }

        public string DataAfter { get; set; }

        public DirectoryUser User { get; set; }

        public int AppVersion { get; set; }
    }

}
