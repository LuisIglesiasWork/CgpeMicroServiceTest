using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public class Audit
    {

        public Guid AuditId { get; set; }

        public Guid UserId { get; set; }

        public Guid RelatedTreeId { get; set; }

        public Guid RelatedTreeType { get; set; }

        public string CertificateSn { get; set; }

        public string SignedData { get; set; }

        public Guid OperationType { get; set; }

        public DateTime CreationDate { get; set; }

        public string DataBefore { get; set; }

        public string DataAfter { get; set; }

        public DirectoryUser User { get; set; }

        public int AppVersion { get; set; }
    }

}
