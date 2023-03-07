using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cgpe.Du.Domain;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cgpe.Du.Infrastructure
{
    public class AssociationProcuratorRepository : IAssociationProcuratorRepository
    {
        private DuUnitOfWork uow;

        public AssociationProcuratorRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }

        #region Validación de prealtas de colegiaciones

        public Guid Create(AssociationProcurator newDomainAssociationProcurator)
        {
            return this.UpsertAssociationProcurator(newDomainAssociationProcurator, true);
        }

        public Guid Update(AssociationProcurator newDomainAssociationProcurator)
        {
            return this.UpsertAssociationProcurator(newDomainAssociationProcurator, false);
        }

        public AssociationProcurator Read(Guid associationProcuratorId)
        {

            AssociationProcuratorEntity entity = this.ReadAssociationProcuratorEntity(associationProcuratorId);
            if (entity == null)
                return null;
            AssociationProcurator assoProc = new AssociationProcurator();
            new AssociationProcuratorEfMap().Map(entity, assoProc);

            return assoProc;
        }

        private AssociationProcuratorEntity ReadAssociationProcuratorEntity(Guid associationProcuratorId)
        {
            try
            {
                AssociationProcuratorEntity associationProcuratorEntity = this.uow.DbContext.AssociationProcurators
                    .Include("AssociationProcuratorAddresses")
                    .Include("AssociationProcuratorAddresses.AddressType")
                    .Include("AssociationProcuratorAddresses.City")
                    .Include("AssociationProcuratorAddresses.Province")
                    .Include("AssociationProcuratorAddresses.WayType")
                    .Include("AssociationProcuratorAddresses.Contacts")
                    .Include("AssociationProcuratorAddresses.Contacts.ContactType")
                    .Include("SituationHistory")
                    .Include("SituationHistory.ProcuratorSituation")
                    .Where(ap => ap.AssociationProcuratorId == associationProcuratorId).SingleOrDefault(); ;


                return associationProcuratorEntity;
            }
            catch (Exception ex)
            {
                var e = ex;
            }

            return null;
        }


        public Guid UpsertAssociationProcurator(AssociationProcurator newDomainAssociationProcurator, bool isNew)
        {
            AssociationProcuratorEntity associationProcuratorEntity = null;

            if (!isNew)
            {
                associationProcuratorEntity = this.ReadAssociationProcuratorEntity(newDomainAssociationProcurator.AssociationProcuratorId);
                if (associationProcuratorEntity == null)
                {
                    throw new Exception($"Association-Procurator with Id \"{newDomainAssociationProcurator.AssociationProcuratorId}\" was not found.");
                }
            }
            else
            {
                associationProcuratorEntity = new AssociationProcuratorEntity();
            }


            new AssociationProcuratorEfMap().Map(newDomainAssociationProcurator, associationProcuratorEntity, newDomainAssociationProcurator.ProcuratorId, isNew);
            CreateUpdateDeleteAssociationProcuratorAddresses(newDomainAssociationProcurator.AssociationProcuratorAddresses, associationProcuratorEntity);
            CreateUpdateDeleteAssociationProcuratorSituationHistory(newDomainAssociationProcurator.SituationHistory, associationProcuratorEntity);

            if (isNew)
            {
                this.uow.DbContext.AssociationProcurators.Add(associationProcuratorEntity);
            }

            return associationProcuratorEntity.AssociationProcuratorId;
        }

        private void CreateUpdateDeleteAssociationProcuratorSituationHistory(List<SituationChange> newSituationChanges, AssociationProcuratorEntity associationProcuratorEntity)
        {
            foreach (SituationChangeEntity existingSituationChange in associationProcuratorEntity.SituationHistory)
            {
                if (!newSituationChanges.Any(c => c.SituationChangeId == existingSituationChange.SituationChangeId))
                {
                    this.uow.DbContext.Remove(existingSituationChange);
                }
            }

            SituationChangeEfMap situationChangeMapper = new SituationChangeEfMap();
            foreach (SituationChange situationChange in newSituationChanges)
            {
                SituationChangeEntity existingSituationChangeEntity = associationProcuratorEntity.SituationHistory
                    .Where(c => c.SituationChangeId == situationChange.SituationChangeId)
                    .SingleOrDefault();

                if (existingSituationChangeEntity != null)
                {
                    situationChangeMapper.Map(situationChange, existingSituationChangeEntity, associationProcuratorEntity.AssociationProcuratorId,
                        associationProcuratorEntity.ProcuratorId, associationProcuratorEntity.AssociationId, false);
                }
                else
                {
                    SituationChangeEntity situationChangeEntity = new SituationChangeEntity();
                    situationChangeMapper.Map(situationChange, situationChangeEntity, associationProcuratorEntity.AssociationProcuratorId,
                        associationProcuratorEntity.ProcuratorId, associationProcuratorEntity.AssociationId, true);
                    associationProcuratorEntity.SituationHistory.Add(situationChangeEntity);
                }
            }

        }

        private void CreateUpdateDeleteAssociationProcuratorAddresses(List<Address> newAddresses, AssociationProcuratorEntity associationProcuratorEntity)
        {

            foreach (AddressEntity existingAddress in associationProcuratorEntity.AssociationProcuratorAddresses)
            {
                if (!newAddresses.Any(c => c.AddressId == existingAddress.AddressId))
                {
                    if (existingAddress.Contacts != null && existingAddress.Contacts.Count > 0)
                    {
                        foreach (ContactEntity contact in existingAddress.Contacts)
                        {
                            this.uow.DbContext.Remove(contact);
                        }
                    }
                    this.uow.DbContext.Remove(existingAddress);
                }
            }

            AddressEfMap addressMapper = new AddressEfMap();
            foreach (Address address in newAddresses)
            {
                AddressEntity existingAddress = associationProcuratorEntity.AssociationProcuratorAddresses
                    .Where(c => c.AddressId == address.AddressId)
                    .SingleOrDefault();

                if (existingAddress != null)
                {
                    addressMapper.Map(address, existingAddress, associationProcuratorEntity.AssociationProcuratorId, false);
                    this.CreateUpdateDeleteAddressContacts(address.Contacts, existingAddress);
                }
                else
                {
                    AddressEntity addressEntity = new AddressEntity();
                    addressMapper.Map(address, addressEntity, associationProcuratorEntity.AssociationProcuratorId, true);
                    associationProcuratorEntity.AssociationProcuratorAddresses.Add(addressEntity);
                    this.CreateUpdateDeleteAddressContacts(address.Contacts, addressEntity);
                }
            }

        }

        private void CreateUpdateDeleteAddressContacts(List<Contact> newContacts, AddressEntity addressEntity)
        {
            foreach (ContactEntity existingContact in addressEntity.Contacts)
            {
                if (!newContacts.Any(c => c.ContactId == existingContact.ContactId))
                {
                    this.uow.DbContext.Remove(existingContact);
                }
            }

            ContactEfMap contactMapper = new ContactEfMap();
            foreach (Contact contact in newContacts)
            {
                ContactEntity existingContactEntity = addressEntity.Contacts
                    .Where(c => c.ContactId == contact.ContactId)
                    .SingleOrDefault();

                if (existingContactEntity != null)
                {
                    contactMapper.Map(contact, existingContactEntity, null, addressEntity.AddressId, null, false);
                }
                else
                {
                    ContactEntity contactEntity = new ContactEntity();
                    contactMapper.Map(contact, contactEntity, null, addressEntity.AddressId, null, true);
                    addressEntity.Contacts.Add(contactEntity);
                }
            }
        }

        public List<AssociationProcurator> GetPendingMembershipsPage(int pageIndex, int pageSize, ref int totalRecords)
        {

            var query = uow.DbContext.AssociationProcurators
                .Include("Procurator")
                .Include("Association")
                .Where(ap =>
                !ap.IsFirst
                && ap.CreationStateId == CreationStateEnum.Precreated
                && ap.Procurator != null
                && ap.Procurator.CreationStateId == CreationStateEnum.Accepted
                && ap.CreatorUser != null
                && ap.CreatorUser.AssociationId.HasValue).OrderByDescending(ap => ap.CreationRequestDate).AsQueryable();

            totalRecords = query.Count();

            var entities = query.Skip((pageSize) * pageIndex).Take(pageSize);

            List<AssociationProcurator> assoprocs = new List<AssociationProcurator>();
            AssociationProcurator assoproc;

            foreach (AssociationProcuratorEntity entity in entities)
            {
                assoproc = new AssociationProcurator();
                new AssociationProcuratorEfMap().ShallowMap(entity, assoproc);
                assoprocs.Add(assoproc);
            }

            return assoprocs;

        }

        public List<AssociationProcurator> GetRejectedMembershipsPage(int pageIndex, int pageSize, ref int totalRecords)
        {

            var query = uow.DbContext.AssociationProcurators
                .Include("Procurator")
                .Include("Association")
                .Where(ap =>
                !ap.IsFirst
                && ap.CreationStateId == CreationStateEnum.Rejected
                && ap.Procurator != null
                && ap.Procurator.CreationStateId == CreationStateEnum.Accepted
                && ap.CreatorUser != null
                && ap.CreatorUser.AssociationId.HasValue).OrderByDescending(ap => ap.CreationRequestDate).AsQueryable();

            var entities = query.Skip((pageSize) * pageIndex).Take(pageSize);

            List<AssociationProcurator> assoProcs = new List<AssociationProcurator>();
            AssociationProcurator assoProc;
            foreach (AssociationProcuratorEntity entity in entities)
            {
                assoProc = new AssociationProcurator();
                new AssociationProcuratorEfMap().ShallowMap(entity, assoProc);
                assoProcs.Add(assoProc);
            }

            totalRecords = query.Count();
            return assoProcs;
        }

        public List<AssociationProcurator> GetAcceptedMembershipsPage(int pageIndex, int pageSize, ref int totalRecords)
        {

            var query = uow.DbContext.AssociationProcurators
                .Include("Procurator")
                .Include("Association")
                .Include("CreatorUser")
                .Include("CreatorUser")
                .Where(ap =>
                !ap.IsFirst
                && ap.CreationStateId == CreationStateEnum.Accepted
                && ap.Procurator != null
                && ap.Procurator.CreationStateId == CreationStateEnum.Accepted
                && ap.CreatorUser != null
                && ap.CreatorUser.AssociationId.HasValue).OrderByDescending(ap => ap.CreationRequestDate).AsQueryable();

            var entities = query.Skip((pageSize) * pageIndex).Take(pageSize);

            List<AssociationProcurator> assoProcs = new List<AssociationProcurator>();
            AssociationProcurator assoProc;
            foreach (AssociationProcuratorEntity entity in entities)
            {
                assoProc = new AssociationProcurator();
                new AssociationProcuratorEfMap().ShallowMap(entity, assoProc);
                assoProcs.Add(assoProc);
            }

            totalRecords = query.Count();
            return assoProcs;
        }

        public List<AssociationProcurator> GetMembershipCreationsByAssociationPage(Guid associationId, int pageIndex, int pageSize, ref int totalRecords)
        {

            var query = uow.DbContext.AssociationProcurators
                .Include("Procurator")
                .Include("Association")
                .Where(ap =>
                    !ap.IsFirst
                    && ap.Procurator != null
                    && ap.Procurator.CreationStateId == CreationStateEnum.Accepted
                    && ap.AssociationId == associationId).OrderByDescending(ap => ap.CreationRequestDate).AsQueryable();

            var entities = query.Skip((pageSize) * pageIndex).Take(pageSize);

            List<AssociationProcurator> assoProcs = new List<AssociationProcurator>();
            AssociationProcurator assoProc;
            foreach (AssociationProcuratorEntity entity in entities)
            {
                assoProc = new AssociationProcurator();
                new AssociationProcuratorEfMap().ShallowMap(entity, assoProc);
                assoProcs.Add(assoProc);
            }

            totalRecords = query.Count();
            return assoProcs;
        }



        #endregion Validación de prealtas de colegiaciones

        public CountResultByAssociation Count(Guid associationId)
        {
            var associationProcuratorEntities = uow.DbContext.AssociationProcurators
                //.Include(x => x.SituationHistory.Select(s => s.ProcuratorSituation))
                .Where(a => a.AssociationId == associationId);


            var procuratorEntities = uow.DbContext.AssociationProcurators
                //.Include(x => x.Procurator.ProcuratorAdherences.Select(a => a.Agreement))
                .Where(a => a.AssociationId == associationId);

            List<ProcuratorSituationEnum> exercisingSituationIds = new List<ProcuratorSituationEnum>();
            exercisingSituationIds.Add(ProcuratorSituationEnum.Practising);
            exercisingSituationIds.Add(ProcuratorSituationEnum.UnregisteredTemporarily);

            //var exercisingCount = associationProcuratorEntities.Count(a => exercisingSituationIds.Contains(a.GetCurrentSituation().ProcuratorSituation.ProcuratorSituationId));
            //var nonExercisingCount = associationProcuratorEntities.Count(a => !exercisingSituationIds.Contains(a.GetCurrentSituation().ProcuratorSituation.ProcuratorSituationId));

            var exercisingCount = -1;
            var nonExercisingCount = -1;

            //TODO: Lo que nos interesa son procuradores que están adheridos al convenio UIHJ,
            // es decir, que tengan una adherence cuyo agreement sea UIHJ.
            //var count1 = procuratorEntities.Count(a => a.Association.HeadquartersAddress.AddressTypeId == AddressTypes.UIHJ);
            var count1 = procuratorEntities.Count(a => a.Procurator.ProcuratorAdherences.Any(b => b.Agreement.AgreementId == Agreements.UIHJ));
            return new CountResultByAssociation
            {
                AssociationName = uow.DbContext.Associations.First(a => a.AssociationId == associationId).Name,
                Exercising = exercisingCount,
                NonExercising = nonExercisingCount,
                Total = exercisingCount + nonExercisingCount,
                //AdscritosUIHJ = count1
                AdheredToUIHJ = count1//-1 // TODO: Cambiar. Esto es sólo para que compile
            };
        }
    }
}
