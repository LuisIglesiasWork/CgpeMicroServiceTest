using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ProcuratorAdherenceEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProcuratorAdherenceId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public string BankAccount { get; set; }

        [MaxLength(500)]
        public string OtherData { get; set; }

        [Required, ForeignKey("Agreement")]
        public string AgreementId { get; set; }

        public virtual AgreementEntity Agreement { get; set; }

        [Required, ForeignKey("Procurator")]
        public string ProcuratorId { get; set; }

        public virtual ProcuratorEntity Procurator { get; set; }

        [ForeignKey("Association")]
        public string AssociationId { get; set; }
        public virtual AssociationEntity Association { get; set; }

        [ForeignKey("Address")]
        public string AddressId { get; set; }

        public virtual AddressEntity Address { get; set; }


        [ForeignKey("Phone")]
        public string PhoneContactId { get; set; }
        public virtual ContactEntity Phone { get; set; }

        [ForeignKey("Mobile")]
        public string MobileContactId { get; set; }
        public virtual ContactEntity Mobile { get; set; }

        [ForeignKey("Email")]
        public string EmailContactId { get; set; }
        public virtual ContactEntity Email { get; set; }

    }

}
