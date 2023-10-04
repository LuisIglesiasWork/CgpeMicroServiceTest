using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AssociationEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AssociationId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [StringLength(6, MinimumLength = 6)]
        public string AssociationCode { get; set; }

        [MaxLength(14)]
        public string Cif { get; set; }

        public virtual List<AssociationProcuratorEntity> AssociationsProcurators { get; set; }

        [ForeignKey("HeadquartersAddress")]
        public string HeadquartersAddressId { get; set; }
        public virtual AddressEntity HeadquartersAddress { get; set; }

        public virtual IList<DirectoryUserEntity> Users { get; set; }

        public virtual IList<ContactEntity> Contacts { get; set; }

    }

}
