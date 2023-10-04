using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AuditEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AuditId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public string RelatedTreeId { get; set; }

        public string RelatedTreeType { get; set; }

        [MaxLength(50)]
        public string CertificateSn { get; set; }

        public string SignedData { get; set; }

        public string OperationType { get; set; }

        public DateTime CreationDate { get; set; }

        public string DataBefore { get; set; }

        public string DataAfter { get; set; }

        public virtual DirectoryUserEntity User { get; set; }

        public int AppVersion { get; set; }

    }

}
