using Cgpe.Du.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cgpe.Du.Infrastructure
{

    public class AssociationRepository : IAssociationRepository
    {

        private DuUnitOfWork uow;

        public AssociationRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }

        private void CreateUpdateDeleteContacts(List<Contact> newAssociationContacts, AssociationEntity associationEntity)
        {
            foreach (ContactEntity existingAssociationContact in associationEntity.Contacts)
            {
                if (!newAssociationContacts.Any(c => c.ContactId == existingAssociationContact.ContactId))
                {
                   // this.uow.DbContext.Remove(existingAssociationContact);
                    //this.uow.DbContext.Remove(existingAssociationContact);
                }
            }

            ContactEfMap posMapper = new ContactEfMap();
            foreach (Contact cont in newAssociationContacts)
            {
                ContactEntity existingContact = associationEntity.Contacts
                    .Where(c => c.ContactId == cont.ContactId)
                    .SingleOrDefault();

                if (existingContact != null)
                {
                    posMapper.Map(cont, existingContact, null, null, associationEntity.AssociationId, false);
                }
                else
                {
                    ContactEntity contEntity = new ContactEntity();
                    posMapper.Map(cont, contEntity, null, null, associationEntity.AssociationId, true);

                    associationEntity.Contacts.Add(contEntity);

                }
            }
        }


        private AssociationEntity ReadAssociationEntity(string associationId)
        {
            AssociationEntity associationEntity = uow.DbContext.Associations

                .Include("Contacts")
                .Include("Contacts.ContactType")

                .Include("HeadquartersAddress")
                .Include("HeadquartersAddress.AddressType")
                .Include("HeadquartersAddress.City")
                .Include("HeadquartersAddress.Province")
                .Include("HeadquartersAddress.WayType")

                .Where(a => a.AssociationId == associationId).FirstOrDefault();

            return associationEntity;
        }

        public void UpsertAssociation(Association newDomainAssociation, bool isNew)
        {
            AssociationEntity associationEntity = null;

            if (isNew)
            {
                associationEntity = new AssociationEntity()
                {
                    AssociationId = Guid.NewGuid().ToString()
                };
            }
            else
            {
                associationEntity = this.ReadAssociationEntity(newDomainAssociation.AssociationId);
                if (associationEntity == null)
                {
                    throw new Exception($"Association with Id \"{newDomainAssociation.AssociationId}\" was not found.");
                }
            }

            new AssociationEfMap().Map(newDomainAssociation, associationEntity);
            newDomainAssociation.AssociationId = associationEntity.AssociationId;

            CreateUpdateDeleteContacts(newDomainAssociation.Contacts, associationEntity);

            if (newDomainAssociation.HeadquartersAddress != null)
            {
                AddressEfMap addressMapper = new AddressEfMap();
                bool isNewAddress = false;
                if (isNew || associationEntity.HeadquartersAddress == null)
                {
                    isNewAddress = true;
                    associationEntity.HeadquartersAddress = new AddressEntity();
                }
                addressMapper.Map(newDomainAssociation.HeadquartersAddress, associationEntity.HeadquartersAddress, null, isNewAddress);
            }

            if (isNew)
            {
                this.uow.DbContext.Associations.Add(associationEntity);
            }
        }

        public void Create(Association newDomainAssociation)
        {
            this.UpsertAssociation(newDomainAssociation, true);
        }

        //public void Create(Association association)
        //{
        //    AssociationEntity entity = new AssociationEntity()
        //    {
        //        AssociationId = Guid.NewGuid()
        //    };
        //    new AssociationEfMap().Map(association, entity);
        //    association.AssociationId = entity.AssociationId;
        //    AddressEfMap addressMap = new AddressEfMap();
        //    ContactEfMap contactMap = new ContactEfMap();
        //    //AddressEntity addressEntity;
        //    ContactEntity contactEntity;
        //    //foreach (Address address in association.Addresses)
        //    //{
        //    //    addressEntity = new AddressEntity() { AddressId = Guid.NewGuid() };
        //    //    addressMap.Map(address, addressEntity);
        //    //    addressEntity.Association = entity;
        //    //    //entity.Addresses.Add(addressEntity);
        //    //    address.AddressId = addressEntity.AddressId;
        //    //    foreach (Contact contact in address.Contacts)
        //    //    {
        //    //        contactEntity = new ContactEntity() { ContactId = Guid.NewGuid() };
        //    //        contactMap.Map(contact, contactEntity);
        //    //        contactEntity.Address = addressEntity;
        //    //        addressEntity.Contacts.Add(contactEntity);
        //    //        contact.ContactId = contactEntity.ContactId;
        //    //    }
        //    //}
        //    foreach (Contact contact in association.Contacts)
        //    {
        //        contactEntity = new ContactEntity() { ContactId = Guid.NewGuid() };
        //        contactMap.Map(contact, contactEntity, null, null, association.AssociationId, true);
        //        contactEntity.Association = entity;
        //        entity.Contacts.Add(contactEntity);
        //        contact.ContactId = contactEntity.ContactId;
        //    }
        //    uow.DbContext.Associations.Add(entity);
        //}

        public Association Read(string associationId)
        {

            AssociationEntity entity = this.ReadAssociationEntity(associationId, null);
            if (entity == null)
                return null;
            Association association = new Association();
            new AssociationEfMap().Map(entity, association);

            return association;
        }

        public AssociationEntity ReadAssociationEntity(string associationId, string cif)
        {
            AssociationEntity entity =
                uow.DbContext.Associations
                .Include("HeadquartersAddress")
                .Include("HeadquartersAddress.AddressType")
                .Include("HeadquartersAddress.City")
                .Include("HeadquartersAddress.Province")
                .Include("HeadquartersAddress.WayType")
                .Include("Contacts.ContactType")
                .Include("Contacts.ContactType")
                .Where(a =>
                    (associationId == null || associationId != null || a.AssociationId == associationId)
                    && (string.IsNullOrWhiteSpace(cif) || a.Cif == cif)).FirstOrDefault();

            return entity;
        }

        public Association ReadByCif(string associationCif)
        {
            AssociationEntity entity = this.ReadAssociationEntity(null, associationCif);
            if (entity == null)
                return null;
            Association association = new Association();
            new AssociationEfMap().Map(entity, association);

            return association;
        }

        //public void Update(Association association)
        //{
        //    AssociationEntity entity = uow.DbContext.Associations.Where(a => a.AssociationId == association.AssociationId).Select(a => a).Take(1).FirstOrDefault();
        //    if (entity == null)
        //        throw new Exception($"Association with Id \"{association.AssociationId}\" was not found.");
        //    new AssociationEfMap().Map(association, entity);
        //}

        public void Update(Association newDomainAssociation)
        {
            this.UpsertAssociation(newDomainAssociation, false);
        }

        public bool CheckIfAssociationIdDocumentExists(string cif, string associationId)
        {
            AssociationEntity assoEntity =
               uow.DbContext.Associations
                   .Where(a => a.Cif == cif && (associationId == null || associationId != null || associationId != a.AssociationId)).FirstOrDefault();

            return assoEntity != null;
        }

        public List<Association> GetAssociations()
        {
            List<AssociationEntity> entities = uow.DbContext.Associations.ToList();
            List<Association> items = new List<Association>();
            Association association;
            AssociationEfMap map = new AssociationEfMap();
            foreach (AssociationEntity entity in entities)
            {
                association = new Association();
                map.Map(entity, association);
                items.Add(association);
            }
            return items.OrderBy(a => a.Name).ToList();
        }
    }

}
