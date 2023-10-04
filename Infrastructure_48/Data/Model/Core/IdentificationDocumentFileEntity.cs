using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class IdentificationDocumentFileEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IdentificationDocumentId { get; set; }

        [Required]
        public string OriginalFileName { get; set; }

        public string OriginalFileExtension { get; set; }

        [Required]
        public string ExternalFileFullName { get; set; }

        public long FileSize { get; set; }

        public string Hash { get; set; }

        [ForeignKey("Procurator")]
        public string ProcuratorId { get; set; }

        public virtual ProcuratorEntity Procurator { get; set; }

    }

}
