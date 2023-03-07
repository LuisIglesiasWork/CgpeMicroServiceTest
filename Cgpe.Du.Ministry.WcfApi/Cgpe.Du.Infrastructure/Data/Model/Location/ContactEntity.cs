using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ContactEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ContactId { get; set; }

        [MaxLength(100)]
        public string Value { get; set; }

        public bool IsPublic { get; set; }

        [ForeignKey("Procurator")]
        public Guid? ProcuratorId { get; set; }

        public virtual ProcuratorEntity Procurator { get; set; }

        [ForeignKey("Association")]
        public Guid? AssociationId { get; set; }

        public virtual AssociationEntity Association { get; set; }

        [ForeignKey("Address")]
        public Guid? AddressId { get; set; }

        public virtual AddressEntity Address { get; set; }

        [Required, ForeignKey("ContactType")]
        public Guid ContactTypeId { get; set; }

        public virtual ContactTypeEntity ContactType { get; set; }

        public bool IsRequired { get; set; }

        //public List<ProcuratorAdherenceEntity> PhoneProcuratorAdherences { get; set; }

        //public List<ProcuratorAdherenceEntity> MobileProcuratorAdherences { get; set; }

        //public List<ProcuratorAdherenceEntity> EmailProcuratorAdherences { get; set; }

    }

}
