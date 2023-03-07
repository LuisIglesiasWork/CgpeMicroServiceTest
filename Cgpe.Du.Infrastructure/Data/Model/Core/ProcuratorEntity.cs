using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ProcuratorEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProcuratorId { get; set; }


        [MaxLength(14)]
        public string LastNifSentToMinistry { get; set; }

        [MaxLength(14)]
        public string Nif { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string SecondName1 { get; set; }

        [MaxLength(50)]
        public string SecondName2 { get; set; }

        [Required, StringLength(12, MinimumLength = 12)]
        public string UniqueNumber { get; set; }

        [MaxLength(50)]
        public string BirthCity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LicencedLawDegreeDate { get; set; }

        public Int32? LicencedLawDegreeAvailableCertificateYear { get; set; }

        public Int32? LicencedLawDegreeAvailableCertificateMonth { get; set; }

        public Int32? LicencedLawDegreeAvailableCertificateDayOfMonth { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GraduadedLawDegreeDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MasterDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ProcuratorDegreeDate { get; set; }

        [MaxLength(250)]
        public string OtherStudies { get; set; }

        public bool DontSendNotifications { get; set; }

        [Required]
        public MinistryIntegrationStatesEnum StateInMinistry { get; set; }

        
        [Required, ForeignKey("Sex")]
        public SexEnum SexId { get; set; }

        public virtual SexEntity Sex { get; set; }

        public virtual IList<AssociationProcuratorEntity> AssociationsProcurators { get; set; }

        [ForeignKey("PersonalAddress")]
        public Guid? PersonalAddressId { get; set; }

        public virtual AddressEntity PersonalAddress { get; set; }

        public virtual IList<ContactEntity> Contacts { get; set; }

        public virtual IList<ProcuratorAdherenceEntity> ProcuratorAdherences { get; set; }

        public virtual IList<ProcuratorLanguageEntity> ProcuratorLanguages { get; set; }

        public virtual IList<HonourEntity> Honours { get; set; }

        public virtual IList<PositionEntity> Positions { get; set; }

        public string Observations { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CurrentThirdPartySuspensionDate { get; set; }

        public virtual IList<SuspensionEntity> Suspensions { get; set; }
        public virtual IList<SituationChangeEntity> SituationHistory { get; set; }
        public virtual IList<IdentificationDocumentFileEntity> IdentificationDocumentFiles { get; set; }


        #region Creation request state

        [ForeignKey("CreationState")]
        public CreationStateEnum CreationStateId { get; set; }
        public virtual CreationStateEntity CreationState { get; set; }

        public DateTime? CreationRequestDate { get; set; }
        public DateTime LatestCreationStateDate { get; set; }

        [ForeignKey("CreatorUser")]
        public Guid CreatorUserId { get; set; }
        public virtual DirectoryUserEntity CreatorUser { get; set; }

        [ForeignKey("CreatorAssociation")]
        public Guid? CreatorAssociationId { get; set; }
        public virtual AssociationEntity CreatorAssociation { get; set; }

        [MaxLength(300)]
        public string CreationStateReason { get; set; }

        #endregion


        /// <summary>
        /// Si el procurador tiene alguna colegiación suspendida.
        /// </summary>
        public bool HasSuspendedMemberships { get; set; }

        /// <summary>
        /// Si el procurador tiene alguna suspensión acordada por terceros ACTIVA.
        /// </summary>
        public bool HasThirdPartySuspensions { get; set; }

        /// <summary>
        /// Si el procuradot tiene alguna cancelación de colegiación ACTIVA.
        /// </summary>
        public bool HasCancelledMemberships { get; set; }

        /// <summary>
        /// Si el procurador tiene alguna colegiación activa.
        /// </summary>
        public bool IsPractisingInAnyAssociation { get; set; }

        public bool CanUseSoftwarePlatforms { get; set; }

        public bool CanUsePrivateWebsite { get; set; }


    }

}
