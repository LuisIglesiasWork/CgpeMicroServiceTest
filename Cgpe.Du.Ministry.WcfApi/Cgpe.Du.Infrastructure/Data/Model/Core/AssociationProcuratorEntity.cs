using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AssociationProcuratorEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid AssociationProcuratorId { get; set; }

        public string MemberNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegistrationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegistrationRequestDate { get; set; }

        public bool IsDefault { get; set; }
        public bool IsFirst { get; set; }

        

        [Required, ForeignKey("Association")]
        public Guid AssociationId { get; set; }

        public virtual AssociationEntity Association { get; set; }

        [Required, ForeignKey("Procurator")]
        public Guid ProcuratorId { get; set; }

        public virtual ProcuratorEntity Procurator { get; set; }

        // TODO: Ir actualizando esto cada día
        public bool IsCancelled { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CurrentCancellationDate { get; set; }

        public virtual IList<CancellationEntity> Cancellations { get; set; }
        public virtual IList<AddressEntity> AssociationProcuratorAddresses { get; set; }
        public virtual IList<SituationChangeEntity> SituationHistory { get; set; }

        [Column(TypeName = "date")]
        public DateTime CurrentSituationDate { get; set; }
        [Required, ForeignKey("CurrentSituation")]
        public ProcuratorSituationEnum CurrentSituationId { get; set; }
        public virtual ProcuratorSituationEntity CurrentSituation { get; set; }


        #region Registration state

        #region Registration state

        [ForeignKey("CreationState")]
        public CreationStateEnum CreationStateId { get; set; }
        public virtual CreationStateEntity CreationState { get; set; }

        public DateTime? CreationRequestDate { get; set; }
        public DateTime LatestCreationStateDate { get; set; }

        [ForeignKey("CreatorUser")]
        public Guid CreatorUserId { get; set; }
        public virtual DirectoryUserEntity CreatorUser { get; set; }

        [MaxLength(300)]
        public string CreationStateReason { get; set; }

        #endregion

        #endregion

        public bool CanUseSoftwarePlatforms()
        {
            return (this.CurrentSituationId == ProcuratorSituationEnum.Practising
                || this.CurrentSituationId == ProcuratorSituationEnum.NonPractisingAuthorisedPersonalAffairs
                || this.CurrentSituationId == ProcuratorSituationEnum.NonPractisingAuthorisedClaimWages
                || this.CurrentSituationId == ProcuratorSituationEnum.RetiredClosing
                || this.CurrentSituationId == ProcuratorSituationEnum.UnregisteredTemporarily);
        }

        public bool CanUsePrivateWebsite()
        {
            return (this.CurrentSituationId == ProcuratorSituationEnum.Practising
                || this.CurrentSituationId == ProcuratorSituationEnum.NonPractising
                || this.CurrentSituationId == ProcuratorSituationEnum.NonPractisingAuthorisedPersonalAffairs
                || this.CurrentSituationId == ProcuratorSituationEnum.NonPractisingAuthorisedClaimWages
                || this.CurrentSituationId == ProcuratorSituationEnum.RetiredClosing
                || this.CurrentSituationId == ProcuratorSituationEnum.UnregisteredTemporarily);
        }

        public bool IsPractisingInAssociation()
        {
            
                return (this.CurrentSituationId == ProcuratorSituationEnum.Practising
                           || this.CurrentSituationId == ProcuratorSituationEnum.UnregisteredTemporarily);
            
        }


        public bool IsAssociationMembershipClosed()
        {
            return (
                this.CurrentSituationId == ProcuratorSituationEnum.None
            || this.CurrentSituationId == ProcuratorSituationEnum.PassedAway
            || this.CurrentSituationId == ProcuratorSituationEnum.UnregisteredForever
            || this.CurrentSituationId == ProcuratorSituationEnum.UnregisteredNotPaying
            || this.CurrentSituationId == ProcuratorSituationEnum.Expelled);
        }

        public bool IsAssociationMembershipSuspended()
        {
            return this.CurrentSituationId == ProcuratorSituationEnum.Suspended;
        }




        // TODO: Ver qué tengo que hacer con esto. Si lo añado a la entidad AssociationProcurator como propiedad que acaba en la BDD o no.

    }

}
