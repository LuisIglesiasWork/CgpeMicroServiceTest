using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AddressEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AddressId { get; set; }

        [ForeignKey("AddressType")]
        public string AddressTypeId { get; set; }

        public virtual AddressTypeEntity AddressType { get; set; }

        public string FullAddress { get; set; }

        [MaxLength(20)]
        public string MailBox { get; set; }

        [MaxLength(14)]
        public string ZipCode { get; set; }

        public bool IsPublic { get; set; }

        [MaxLength(50)]
        public string Door { get; set; }

        [MaxLength(50)]
        public string Floor { get; set; }

        [MaxLength(50)]
        public string Stairway { get; set; }

        [MaxLength(100)]
        public string WayName { get; set; }

        [MaxLength(50)]
        public string WayNumber { get; set; }

        [MaxLength(100)]
        public string BuildingName { get; set; }

        public bool IsReceivingMagazine { get; set; }

        [ForeignKey("City")]
        public string CityId { get; set; }

        public virtual CityEntity City { get; set; }

        [ForeignKey("Province")]
        public string ProvinceId { get; set; }

        public virtual ProvinceEntity Province { get; set; }

        [ForeignKey("WayType")]
        public string WayTypeId { get; set; }

        public virtual WayTypeEntity WayType { get; set; }

        [ForeignKey("AssociationProcurator")]
        public string AssociationProcuratorId { get; set; }
        public virtual AssociationProcuratorEntity AssociationProcurator { get; set; }

        public virtual IList<ContactEntity> Contacts { get; set; }

        public virtual IList<ProcuratorAdherenceEntity> ProcuratorAdherences { get; set; }

    }

}
