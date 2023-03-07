using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ProcuratorLanguageEntity
    {

        [Required, ForeignKey("Procurator")]
        public Guid ProcuratorId { get; set; }

        public virtual ProcuratorEntity Procurator { get; set; }

        [Required, ForeignKey("Language")]
        public Guid LanguageId { get; set; }

        public virtual LanguageEntity Language { get; set; }

    }

}
