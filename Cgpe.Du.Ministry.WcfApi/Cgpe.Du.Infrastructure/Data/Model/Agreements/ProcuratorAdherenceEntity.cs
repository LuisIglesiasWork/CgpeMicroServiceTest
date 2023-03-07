using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ProcuratorAdherenceEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProcuratorAdherenceId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public string BankAccount { get; set; }

        [MaxLength(500)]
        public string OtherData { get; set; }

        [Required, ForeignKey("Agreement")]
        public Guid AgreementId { get; set; }

        public virtual AgreementEntity Agreement { get; set; }

        [Required, ForeignKey("Procurator")]
        public Guid ProcuratorId { get; set; }

        public virtual ProcuratorEntity Procurator { get; set; }

        [ForeignKey("Association")]
        public Guid? AssociationId { get; set; }
        public virtual AssociationEntity Association { get; set; }

        [ForeignKey("Address")]
        public Guid? AddressId { get; set; }

        public virtual AddressEntity Address { get; set; }


        [ForeignKey("Phone")]
        public Guid? PhoneContactId { get; set; }
        public virtual ContactEntity Phone { get; set; }

        [ForeignKey("Mobile")]
        public Guid? MobileContactId { get; set; }
        public virtual ContactEntity Mobile { get; set; }

        [ForeignKey("Email")]
        public Guid? EmailContactId { get; set; }
        public virtual ContactEntity Email { get; set; }

    }

}
