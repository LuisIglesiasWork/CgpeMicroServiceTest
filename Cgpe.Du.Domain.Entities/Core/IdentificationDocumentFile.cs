using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cgpe.Du.Domain.Entities
{
    public class IdentificationDocumentFile
    {
        
        public Guid IdentificationDocumentId { get; set; }

        public string OriginalFileName { get; set; }

        public string OriginalFileExtension { get; set; }

        public string ExternalFileFullName { get; set; }

        public long FileSize { get; set; }

        public string Hash { get; set; }

        public Guid? ProcuratorId { get; set; }
    }
}
