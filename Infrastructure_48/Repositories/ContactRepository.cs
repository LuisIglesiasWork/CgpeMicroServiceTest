using Cgpe.Du.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cgpe.Du.Infrastructure
{

    public class ContactRepository : IContactRepository
    {

        private DuUnitOfWork uow;

        public ContactRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }

        public void Create(Contact contact)
        {
            ContactEntity entity = new ContactEntity()
            {
                ContactId = Guid.NewGuid().ToString()
            };
            new ContactEfMap().Map(contact, entity, contact.Procurator.ProcuratorId, null, null, true);
            contact.ContactId = entity.ContactId;
            uow.DbContext.Contacts.Add(entity);
        }

        public Contact Read(string contactId)
        {
            ContactEntity entity =
                uow.DbContext.Contacts
                .Include("ContactType")
                .Include("Association")
                .Include("Procurator")
                .Include("Address.Association")
                .Include("Address.Procurator")
                .Where(c => c.ContactId == contactId).Select(a => a).Take(1).FirstOrDefault();
            if (entity == null)
                return null;
            Contact item = new Contact();
            new ContactEfMap().Map(entity, item);
            if (entity.AssociationId != null)
            {
                item.Association = new Association();
                AssociationEfMap associationMap = new AssociationEfMap();
                associationMap.Map(entity.Association, item.Association);
            }
            if (entity.ProcuratorId != null)
            {
                item.Procurator = new Procurator();
                ProcuratorEfMap procuratorMap = new ProcuratorEfMap();
                procuratorMap.Map(entity.Procurator, item.Procurator);
            }
            if (entity.Address != null)
            {
                AddressEfMap addressMap = new AddressEfMap();
                item.Address = new Address();
                addressMap.Map(entity.Address, item.Address);
                //if (entity.Address.AssociationId.HasValue)
                //{
                //    item.Address.Association = new Association();
                //    AssociationEfMap associationMap = new AssociationEfMap();
                //    associationMap.Map(entity.Address.Association, item.Address.Association);
                //}
                //if (entity.Address.ProcuratorId.HasValue)
                //{
                //    item.Address.Procurator = new Procurator();
                //    ProcuratorEfMap procuratorMap = new ProcuratorEfMap();
                //    procuratorMap.Map(entity.Address.Procurator, item.Address.Procurator);
                //}
            }
            return item;
        }

        public void Update(Contact contact)
        {
            ContactEntity entity = uow.DbContext.Contacts.Where(c => c.ContactId == contact.ContactId).Select(a => a).Take(1).FirstOrDefault();
            if (entity == null)
                throw new Exception($"Contact with Id \"{contact.ContactId}\" was not found.");
            new ContactEfMap().Map(contact, entity, contact.Procurator.ProcuratorId, null, null, false);
        }

        public void Delete(string contactId)
        {
            ContactEntity entity = uow.DbContext.Contacts.Where(c => c.ContactId == contactId).Select(a => a).Take(1).FirstOrDefault();
            if (entity == null)
                return;
            uow.DbContext.Contacts.Remove(entity);
        }

    }

}
