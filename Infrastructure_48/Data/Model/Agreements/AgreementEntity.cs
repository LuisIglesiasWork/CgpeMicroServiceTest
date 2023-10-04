using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AgreementEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AgreementId { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(100)]
        public string AgreementName { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(10, MinimumLength = 3)]
        public string AgreementCode { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(250)]
        public string AgreementDescription { get; set; }

        [Column(TypeName = "date")]
        public DateTime ForceDate { get; set; }
        
        public bool RequiresAssociation { get; set; }

        public bool RequiresBankAccount { get; set; }

        public bool RequiresContactData { get; set; }

        public virtual IList<ProcuratorAdherenceEntity> ProcuratorAdherences { get; set; }

    }

}
