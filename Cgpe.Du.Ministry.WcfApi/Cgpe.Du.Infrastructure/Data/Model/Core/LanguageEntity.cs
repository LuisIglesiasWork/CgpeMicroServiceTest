using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class LanguageEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid LanguageId { get; set; }

        [Required, StringLength(2, MinimumLength = 2)]
        public string LanguageCode { get; set; }

        [Required, MaxLength(50)]
        public string LanguageName { get; set; }

        public virtual IList<ProcuratorLanguageEntity> ProcuratorLanguages { get; set; }

    }

}
