using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class ContactEfMap
    {

        public void Map(ContactEntity source, Contact target)
        {
            target.ContactId = source.ContactId;
            if (source.ContactType != null)
                target.ContactType = new ContactType() { TypeId = source.ContactType.TypeId, TypeName = source.ContactType.TypeName };
            target.IsPublic = source.IsPublic;
            target.Value = source.Value;
            target.IsRequired = source.IsRequired;
            if (source.AssociationId.HasValue)
                target.Association = new Association() { AssociationId = source.AssociationId.Value };
            if (source.ProcuratorId.HasValue)
                target.Procurator = new Procurator() { ProcuratorId = source.ProcuratorId.Value };
            if (source.AddressId.HasValue)
                target.Address = new Address() { AddressId = source.AddressId.Value };
        }

        public void Map(Contact source, ContactEntity target, Guid? procuratorId, Guid? addressId, Guid? associationId, bool isNew = false)
        {
            if (isNew)
            {
                source.ContactId = Guid.NewGuid();
            }
            target.ContactId = source.ContactId;
            target.ProcuratorId = procuratorId;
            target.AddressId = addressId;
            target.AssociationId = associationId;

            if (source.ContactType != null)
                target.ContactTypeId = source.ContactType.TypeId;
            target.IsPublic = source.IsPublic;
            target.Value = source.Value;
            target.IsRequired = source.IsRequired;
            if (source.Association != null)
                target.AssociationId = source.Association.AssociationId;
            if (source.Procurator != null)
                target.ProcuratorId = source.Procurator.ProcuratorId;
            if (source.Address != null)
                target.AddressId = source.Address.AddressId;
        }


    }

}
