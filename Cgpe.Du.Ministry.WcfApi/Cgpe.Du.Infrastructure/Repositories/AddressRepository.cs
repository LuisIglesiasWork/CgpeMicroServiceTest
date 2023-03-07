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

    public class AddressRepository : IAddressRepository
    {

        private DuUnitOfWork uow;

        public AddressRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }

        public void Create(Address address)
        {
            AddressEntity entity = new AddressEntity()
            {
                AddressId = Guid.NewGuid()
            };
            new AddressEfMap().Map(address, entity, null, false);
            address.AddressId = entity.AddressId;
            ContactEfMap contactMap = new ContactEfMap();
            ContactEntity contactEntity;
            foreach (Contact contact in address.Contacts)
            {
                contactEntity = new ContactEntity() { ContactId = Guid.NewGuid() };
                contactMap.Map(contact, contactEntity, null, address.AddressId, null, true);
                contactEntity.Address = entity;
                entity.Contacts.Add(contactEntity);
                contact.ContactId = contactEntity.ContactId;
            }
            uow.DbContext.Addresses.Add(entity);
        }

        public Address Read(Guid addressId)
        {
            AddressEntity entity =
                uow.DbContext.Addresses
                .Include("AddressType")
                .Include("City")
                .Include("Province")
                .Include("WayType")
                .Include("Contacts.ContactType")
                .Where(a => a.AddressId == addressId).Select(a => a).Take(1).FirstOrDefault();
            if (entity == null)
                return null;
            Address item = new Address();
            new AddressEfMap().Map(entity, item);
            Contact contact;
            ContactEfMap contactMap = new ContactEfMap();
            if (entity.Contacts != null)
            {
                foreach (ContactEntity contactEntity in entity.Contacts)
                {
                    contact = new Contact();
                    contactMap.Map(contactEntity, contact);
                    contact.Address = item;
                    item.Contacts.Add(contact);
                }
            }

            return item;
        }

        public void Update(Address address)
        {
            AddressEntity entity = uow.DbContext.Addresses.Where(a => a.AddressId == address.AddressId).Select(a => a).Take(1).FirstOrDefault();
            if (entity == null)
                throw new Exception($"Address with Id \"{address.AddressId}\" was not found.");
            new AddressEfMap().Map(address, entity, null, false);
        }

        public List<Address> GetAddressesWithExecutingSituationAndMagazine()
        {
            List<AddressEntity> entities = uow.DbContext.Addresses
                .Include(x => x.AssociationProcurator.SituationHistory.Select(y => y.ProcuratorSituation))
                .Where(x => x.IsReceivingMagazine
                            && x.AssociationProcurator.CurrentSituationId == ProcuratorSituationEnum.Practising
                            )
                .ToList();
            List<Address> items = new List<Address>();
            Address address;
            AddressEfMap map = new AddressEfMap();
            foreach (AddressEntity entity in entities)
            {
                address = new Address();
                map.Map(entity, address);
                items.Add(address);
            }
            return items;
        }

        public void Delete(Guid addressId)
        {
            AddressEntity entity = uow.DbContext.Addresses.Where(a => a.AddressId == addressId).Select(a => a).Take(1).FirstOrDefault();
            if (entity == null)
                throw new Exception($"Address with Id \"{addressId}\" was not found.");
            if (entity.Contacts != null)
            {
                foreach (ContactEntity contactEntity in entity.Contacts)
                {
                    uow.DbContext.Contacts.Remove(contactEntity);
                }
            }
            uow.DbContext.Addresses.Remove(entity);
        }

    }

}
