using Cgpe.Du.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Internal;
//using Remotion.Linq.Parsing.Structure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
//using System.Data.SqlClient;
using Cgpe.Du.Infrastructure.Properties;

namespace Cgpe.Du.Infrastructure
{

    public class ProcuratorRepository : IProcuratorRepository
    {

        private DuUnitOfWork uow;
        //private AzureStorageProxy storageProxy;

        public ProcuratorRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");

            //this.storageProxy = new AzureStorageProxy();
        }


        private List<ContactEntity> GetAllContactsByType(ProcuratorEntity procuratorEntity, Guid contactTypeId)
        {
            List<ContactEntity> contacts = new List<ContactEntity>();

            if (procuratorEntity != null)
            {
                if (procuratorEntity.Contacts != null && procuratorEntity.Contacts.Count > 0)
                {
                    contacts.AddRange(procuratorEntity.Contacts.Where(c => c.ContactTypeId == contactTypeId));
                }

                if (procuratorEntity.AssociationsProcurators != null && procuratorEntity.AssociationsProcurators.Count > 0)
                {
                    foreach (AssociationProcuratorEntity ap in procuratorEntity.AssociationsProcurators)
                    {
                        if (ap.AssociationProcuratorAddresses != null && ap.AssociationProcuratorAddresses.Count > 0)
                        {
                            foreach (AddressEntity addr in ap.AssociationProcuratorAddresses)
                            {
                                if (addr.Contacts != null && addr.Contacts.Count > 0)
                                {
                                    contacts.AddRange(addr.Contacts.Where(c => c.ContactTypeId == contactTypeId));
                                }
                            }
                        }
                    }
                }
            }

            return contacts;
        }

        private List<AddressEntity> GetAllAddresses(ProcuratorEntity procuratorEntity)
        {
            List<AddressEntity> addresses = new List<AddressEntity>();

            if (procuratorEntity != null)
            {
                if (procuratorEntity.PersonalAddress != null && !string.IsNullOrWhiteSpace(procuratorEntity.PersonalAddress.FullAddress))
                {
                    addresses.Add(procuratorEntity.PersonalAddress);
                }

                if (procuratorEntity.AssociationsProcurators != null && procuratorEntity.AssociationsProcurators.Count > 0)
                {
                    foreach (AssociationProcuratorEntity ap in procuratorEntity.AssociationsProcurators)
                    {
                        if (ap.AssociationProcuratorAddresses != null && ap.AssociationProcuratorAddresses.Count > 0)
                        {
                            addresses.AddRange(ap.AssociationProcuratorAddresses.Where(a => !string.IsNullOrWhiteSpace(a.FullAddress)));

                        }
                    }
                }
            }

            return addresses;
        }


        private void CreateUpdateDeletePersonalContacts(List<Contact> newPersonalContacts, ProcuratorEntity procuratorEntity)
        {
            foreach (ContactEntity existingPersonalContact in procuratorEntity.Contacts)
            {
                if (!newPersonalContacts.Any(c => c.ContactId == existingPersonalContact.ContactId))
                {
                    this.uow.DbContext.Remove(existingPersonalContact);
                }
            }

            ContactEfMap posMapper = new ContactEfMap();
            foreach (Contact cont in newPersonalContacts)
            {
                ContactEntity existingContact = procuratorEntity.Contacts
                    .Where(c => c.ContactId == cont.ContactId)
                    .SingleOrDefault();

                if (existingContact != null)
                {
                    posMapper.Map(cont, existingContact, procuratorEntity.ProcuratorId, null, null, false);
                }
                else
                {
                    ContactEntity contEntity = new ContactEntity();
                    posMapper.Map(cont, contEntity, procuratorEntity.ProcuratorId, null, null, true);

                    procuratorEntity.Contacts.Add(contEntity);

                }
            }
        }

        private void CreateUpdateDeleteIdentificationDocumentFiles(List<IdentificationDocumentFile> newDocs, ProcuratorEntity procuratorEntity)
        {
            foreach (IdentificationDocumentFileEntity existingDoc in procuratorEntity.IdentificationDocumentFiles)
            {
                if (!newDocs.Any(c => c.IdentificationDocumentId == existingDoc.IdentificationDocumentId))
                {
                    this.uow.DbContext.Remove(existingDoc);
                    //this.storageProxy.DeleteBlob(existingDoc.OriginalFileName);
                }
            }

            IdentificationDocumentFileEfMap docMapper = new IdentificationDocumentFileEfMap();
            foreach (IdentificationDocumentFile doc in newDocs)
            {
                IdentificationDocumentFileEntity existingDoc = procuratorEntity.IdentificationDocumentFiles
                    .Where(c => c.IdentificationDocumentId == doc.IdentificationDocumentId)
                    .SingleOrDefault();

                if (existingDoc != null)
                {
                    docMapper.Map(doc, existingDoc, procuratorEntity.ProcuratorId);
                }
                else
                {
                    IdentificationDocumentFileEntity docEntity = new IdentificationDocumentFileEntity();
                    docMapper.Map(doc, docEntity, procuratorEntity.ProcuratorId);
                    procuratorEntity.IdentificationDocumentFiles.Add(docEntity);
                }
            }
        }

        private void CreateUpdateDeleteHonours(List<Honour> newHonours, ProcuratorEntity procuratorEntity)
        {
            foreach (HonourEntity existingHonour in procuratorEntity.Honours)
            {
                if (!newHonours.Any(c => c.HonourId == existingHonour.HonourId))
                {
                    this.uow.DbContext.Remove(existingHonour);
                }
            }

            HonourEfMap honourMapper = new HonourEfMap();
            foreach (Honour honour in newHonours)
            {
                HonourEntity existingHonour = procuratorEntity.Honours
                    .Where(c => c.HonourId == honour.HonourId)
                    .SingleOrDefault();

                if (existingHonour != null)
                {
                    honourMapper.Map(honour, existingHonour, procuratorEntity.ProcuratorId, false);
                }
                else
                {
                    HonourEntity honourEntity = new HonourEntity();
                    honourMapper.Map(honour, honourEntity, procuratorEntity.ProcuratorId, true);

                    procuratorEntity.Honours.Add(honourEntity);
                }
            }
        }

        private void CreateUpdateIdentificationFiles(List<IdentificationDocumentFile> newFiles, ProcuratorEntity procuratorEntity)
        {
            foreach (IdentificationDocumentFileEntity existingFile in procuratorEntity.IdentificationDocumentFiles)
            {
                if (!newFiles.Any(c => c.IdentificationDocumentId == existingFile.IdentificationDocumentId))
                {
                    this.uow.DbContext.Remove(existingFile);
                }
            }

            IdentificationDocumentFileEfMap fileMapper = new IdentificationDocumentFileEfMap();
            foreach (IdentificationDocumentFile file in newFiles)
            {
                IdentificationDocumentFileEntity existingFile = procuratorEntity.IdentificationDocumentFiles
                    .Where(c => c.IdentificationDocumentId == file.IdentificationDocumentId)
                    .SingleOrDefault();

                if (existingFile != null)
                {
                    fileMapper.Map(file, existingFile, procuratorEntity.ProcuratorId, false);
                }
                else
                {
                    IdentificationDocumentFileEntity fileEntity = new IdentificationDocumentFileEntity();
                    fileMapper.Map(file, fileEntity, procuratorEntity.ProcuratorId, true);

                    procuratorEntity.IdentificationDocumentFiles.Add(fileEntity);
                }
            }
        }

        private void CreateUpdateDeleteAdherences(List<ProcuratorAdherence> newAdherences, ProcuratorEntity procuratorEntity)
        {
            foreach (ProcuratorAdherenceEntity existingProcuratorAdherence in procuratorEntity.ProcuratorAdherences)
            {
                if (!newAdherences.Any(c => c.ProcuratorAdherenceId == existingProcuratorAdherence.ProcuratorAdherenceId))
                {
                    this.uow.DbContext.Remove(existingProcuratorAdherence);
                }
            }

            ProcuratorAdherenceEfMap procuratorAdherenceMapper = new ProcuratorAdherenceEfMap();
            foreach (ProcuratorAdherence procuratorAdherence in newAdherences)
            {
                ProcuratorAdherenceEntity existingProcuratorAdherenceEntity = procuratorEntity.ProcuratorAdherences
                    .Where(c => c.ProcuratorAdherenceId == procuratorAdherence.ProcuratorAdherenceId)
                    .SingleOrDefault();

                AddressEntity addEnt = GetAllAddresses(procuratorEntity).Where(a => procuratorAdherence.Address != null && a.FullAddress.Equals(procuratorAdherence.Address.FullAddress)).FirstOrDefault();
                ContactEntity phoneEnt = GetAllContactsByType(procuratorEntity, ContactTypes.Phone).Where(p => procuratorAdherence.Phone != null && p.Value.Equals(procuratorAdherence.Phone.Value)).FirstOrDefault();
                ContactEntity mobileEnt = GetAllContactsByType(procuratorEntity, ContactTypes.Mobile).Where(p => procuratorAdherence.Mobile != null && p.Value.Equals(procuratorAdherence.Mobile.Value)).FirstOrDefault();
                ContactEntity emailEnt = GetAllContactsByType(procuratorEntity, ContactTypes.Email).Where(p => procuratorAdherence.Email != null && p.Value.Equals(procuratorAdherence.Email.Value)).FirstOrDefault();

                if (existingProcuratorAdherenceEntity != null)
                {
                    procuratorAdherenceMapper.Map(procuratorAdherence, existingProcuratorAdherenceEntity, procuratorEntity.ProcuratorId, addEnt?.AddressId, phoneEnt?.ContactId, mobileEnt?.ContactId, emailEnt?.ContactId, false);
                }
                else
                {
                    ProcuratorAdherenceEntity adhEntity = new ProcuratorAdherenceEntity();
                    procuratorAdherenceMapper.Map(procuratorAdherence, adhEntity, procuratorEntity.ProcuratorId, addEnt?.AddressId, phoneEnt?.ContactId, mobileEnt?.ContactId, emailEnt?.ContactId, true);
                    procuratorEntity.ProcuratorAdherences.Add(adhEntity);
                }
            }
        }

        private void CreateUpdateDeleteSuspensions(List<Suspension> newSuspensions, ProcuratorEntity procuratorEntity)
        {
            foreach (SuspensionEntity existingSuspension in procuratorEntity.Suspensions)
            {
                if (!newSuspensions.Any(c => c.SuspensionId == existingSuspension.SuspensionId))
                {
                    this.uow.DbContext.Remove(existingSuspension);
                }
            }

            SuspensionEfMap suspensionMapper = new SuspensionEfMap();
            foreach (Suspension suspension in newSuspensions)
            {
                SuspensionEntity existingSuspension = procuratorEntity.Suspensions
                    .Where(c => c.SuspensionId == suspension.SuspensionId)
                    .SingleOrDefault();

                if (existingSuspension != null)
                {
                    suspensionMapper.Map(suspension, existingSuspension, procuratorEntity.ProcuratorId, false);
                }
                else
                {
                    SuspensionEntity suspensionEntity = new SuspensionEntity();
                    suspensionMapper.Map(suspension, suspensionEntity, procuratorEntity.ProcuratorId, true);

                    procuratorEntity.Suspensions.Add(suspensionEntity);
                }
            }
        }


        private void CreateUpdateDeletePositions(List<Position> newPositions, ProcuratorEntity procuratorEntity)
        {
            foreach (PositionEntity existingPosition in procuratorEntity.Positions)
            {
                if (!newPositions.Any(c => c.PositionId == existingPosition.PositionId))
                {
                    this.uow.DbContext.Remove(existingPosition);
                }
            }

            PositionEfMap positionMapper = new PositionEfMap();
            foreach (Position position in newPositions)
            {
                PositionEntity existingPosition = procuratorEntity.Positions
                    .Where(c => c.PositionId == position.PositionId)
                    .SingleOrDefault();

                if (existingPosition != null)
                {
                    positionMapper.Map(position, existingPosition, procuratorEntity.ProcuratorId, false);
                }
                else
                {
                    PositionEntity positionEntity = new PositionEntity();
                    positionMapper.Map(position, positionEntity, procuratorEntity.ProcuratorId, true);

                    procuratorEntity.Positions.Add(positionEntity);
                }
            }
        }

        private void CreateUpdateDeleteProcuratorLanguages(List<Language> newLanguages, ProcuratorEntity procuratorEntity)
        {
            foreach (ProcuratorLanguageEntity existingLanguage in procuratorEntity.ProcuratorLanguages)
            {
                if (!newLanguages.Any(c => c.LanguageId == existingLanguage.LanguageId))
                {
                    this.uow.DbContext.Remove(existingLanguage);
                }
            }

            foreach (Language language in newLanguages)
            {
                ProcuratorLanguageEntity existingLanguage = procuratorEntity.ProcuratorLanguages
                    .Where(c => c.LanguageId == language.LanguageId)
                    .SingleOrDefault();

                if (existingLanguage == null)
                {
                    ProcuratorLanguageEntity procLangEntity = new ProcuratorLanguageEntity();
                    procLangEntity.LanguageId = language.LanguageId;
                    procLangEntity.ProcuratorId = procuratorEntity.ProcuratorId;
                    procuratorEntity.ProcuratorLanguages.Add(procLangEntity);
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

        private void CreateUpdateDeleteAssociationProcuratorCancellations(List<Cancellation> newCancellations, AssociationProcuratorEntity associationProcuratorEntity)
        {
            foreach (CancellationEntity existingCancellation in associationProcuratorEntity.Cancellations)
            {
                if (!newCancellations.Any(c => c.CancellationId == existingCancellation.CancellationId))
                {
                    this.uow.DbContext.Remove(existingCancellation);
                }
            }

            CancellationEfMap cancellationMapper = new CancellationEfMap();
            foreach (Cancellation cancellation in newCancellations)
            {
                CancellationEntity existingCancellationEntity = associationProcuratorEntity.Cancellations
                    .Where(c => c.CancellationId == cancellation.CancellationId)
                    .SingleOrDefault();

                if (existingCancellationEntity != null)
                {
                    cancellationMapper.Map(cancellation, existingCancellationEntity, associationProcuratorEntity.AssociationProcuratorId, false);
                }
                else
                {
                    CancellationEntity cancellationEntity = new CancellationEntity();
                    cancellationMapper.Map(cancellation, cancellationEntity, associationProcuratorEntity.AssociationProcuratorId, true);
                    associationProcuratorEntity.Cancellations.Add(cancellationEntity);
                }
            }

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


        private void CreateUpdateAssociationsProcurators(List<AssociationProcurator> newAssociationsProcurators, ProcuratorEntity procuratorEntity)
        {
            foreach (AssociationProcurator assoProc in newAssociationsProcurators)
            {
                AssociationProcuratorEfMap apMapper = new AssociationProcuratorEfMap();
                AssociationProcuratorEntity existingAssoProcEntity = procuratorEntity.AssociationsProcurators
                    .Where(c => c.AssociationProcuratorId == assoProc.AssociationProcuratorId)
                    .SingleOrDefault();

                if (existingAssoProcEntity != null)
                {
                    apMapper.Map(assoProc, existingAssoProcEntity, procuratorEntity.ProcuratorId, false);
                    CreateUpdateDeleteAssociationProcuratorAddresses(assoProc.AssociationProcuratorAddresses, existingAssoProcEntity);
                    CreateUpdateDeleteAssociationProcuratorSituationHistory(assoProc.SituationHistory, existingAssoProcEntity);
                    CreateUpdateDeleteAssociationProcuratorCancellations(assoProc.Cancellations, existingAssoProcEntity);

                }
                else
                {
                    AssociationProcuratorEntity apEntity = new AssociationProcuratorEntity();
                    apMapper.Map(assoProc, apEntity, procuratorEntity.ProcuratorId, true);
                    CreateUpdateDeleteAssociationProcuratorAddresses(assoProc.AssociationProcuratorAddresses, apEntity);
                    CreateUpdateDeleteAssociationProcuratorSituationHistory(assoProc.SituationHistory, apEntity);
                    CreateUpdateDeleteAssociationProcuratorCancellations(assoProc.Cancellations, apEntity);
                    procuratorEntity.AssociationsProcurators.Add(apEntity);
                }
            }
        }

        public Guid Create(Procurator newDomainProcurator)
        {
            return this.UpsertProcurator(newDomainProcurator, true, true);
        }

        public Guid UpsertProcurator(Procurator newDomainProcurator, bool isNew, bool mapStateInMinistry)
        {
            ProcuratorEntity procuratorEntity = null;

            if (isNew)
            {
                procuratorEntity = new ProcuratorEntity()
                {
                    ProcuratorId = Guid.NewGuid()
                };
            }
            else
            {
                procuratorEntity = this.ReadProcuratorEntity(newDomainProcurator.ProcuratorId);
                if (procuratorEntity == null)
                {
                    throw new Exception($"Procurator with Id \"{newDomainProcurator.ProcuratorId}\" was not found.");
                }
            }

            new ProcuratorEfMap().Map(newDomainProcurator, procuratorEntity, mapStateInMinistry);
            newDomainProcurator.ProcuratorId = procuratorEntity.ProcuratorId;
            if (isNew)
            {
                newDomainProcurator.StateInMinistry = MinistryIntegrationStatesEnum.Unregistered;
            }
            CreateUpdateDeletePersonalContacts(newDomainProcurator.PersonalContacts, procuratorEntity);
            CreateUpdateDeleteIdentificationDocumentFiles(newDomainProcurator.IdentificationDocumentFiles, procuratorEntity);

            if (newDomainProcurator.PersonalAddress != null)
            {
                AddressEfMap addressMapper = new AddressEfMap();
                bool isNewAddress = false;
                if (isNew || procuratorEntity.PersonalAddress == null)
                {
                    isNewAddress = true;
                    procuratorEntity.PersonalAddress = new AddressEntity();
                }
                addressMapper.Map(newDomainProcurator.PersonalAddress, procuratorEntity.PersonalAddress, null, isNewAddress);
            }

            CreateUpdateAssociationsProcurators(newDomainProcurator.AssociationsProcurators, procuratorEntity);
            CreateUpdateDeleteProcuratorLanguages(newDomainProcurator.ProcuratorLanguages, procuratorEntity);
            CreateUpdateDeleteSuspensions(newDomainProcurator.Suspensions, procuratorEntity);
            CreateUpdateDeletePositions(newDomainProcurator.Positions, procuratorEntity);
            CreateUpdateDeleteHonours(newDomainProcurator.Honours, procuratorEntity);
            CreateUpdateDeleteAdherences(newDomainProcurator.ProcuratorAdherences, procuratorEntity);

            if (isNew)
            {
                this.uow.DbContext.Procurators.Add(procuratorEntity);
            }

            return procuratorEntity.ProcuratorId;
        }

        private ProcuratorEntity ReadProcuratorEntity(Guid procuratorId)
        {
            try
            {
                ProcuratorEntity procuratorEntity = uow.DbContext.Procurators
                    .Include("Sex")

                    .Include("PersonalAddress")
                    .Include("PersonalAddress.AddressType")
                    .Include("PersonalAddress.City")
                    .Include("PersonalAddress.Province")
                    .Include("PersonalAddress.WayType")

                    .Include("Contacts")
                    .Include("Contacts.ContactType")

                    .Include("AssociationsProcurators")
                    .Include("AssociationsProcurators.Association")

                    .Include("AssociationsProcurators.CurrentSituation")

                    .Include("AssociationsProcurators.AssociationProcuratorAddresses")
                    .Include("AssociationsProcurators.AssociationProcuratorAddresses.AddressType")
                    .Include("AssociationsProcurators.AssociationProcuratorAddresses.City")
                    .Include("AssociationsProcurators.AssociationProcuratorAddresses.Province")
                    .Include("AssociationsProcurators.AssociationProcuratorAddresses.WayType")
                    .Include("AssociationsProcurators.AssociationProcuratorAddresses.Contacts")
                    .Include("AssociationsProcurators.AssociationProcuratorAddresses.Contacts.ContactType")

                    .Include("AssociationsProcurators.Cancellations")

                    .Include("AssociationsProcurators.SituationHistory")
                    .Include("AssociationsProcurators.SituationHistory.ProcuratorSituation")

                    .Include("Suspensions")
                    .Include("Suspensions.AssociationAgreeingOnSuspension")

                    .Include("ProcuratorAdherences")
                    .Include("ProcuratorAdherences.Agreement")
                    .Include("ProcuratorAdherences.Association")
                    .Include("ProcuratorAdherences.Address")
                    .Include("ProcuratorAdherences.Address.AddressType")
                    .Include("ProcuratorAdherences.Address.City")
                    .Include("ProcuratorAdherences.Address.Province")
                    .Include("ProcuratorAdherences.Address.WayType")
                    .Include("ProcuratorAdherences.Phone")
                    .Include("ProcuratorAdherences.Phone.ContactType")
                    .Include("ProcuratorAdherences.Mobile")
                    .Include("ProcuratorAdherences.Mobile.ContactType")
                    .Include("ProcuratorAdherences.Email")
                    .Include("ProcuratorAdherences.Email.ContactType")



                    .Include("ProcuratorLanguages")
                    .Include("ProcuratorLanguages.Language")

                    .Include("Honours")
                    .Include("Honours.HonourType")

                    .Include("Positions")
                    .Include("Positions.OrganizationType")
                    .Include("Positions.Organization")
                    .Include("Positions.Organ")
                    .Include("Positions.PositionType")

                    .Include("IdentificationDocumentFiles")


                    .Where(p => p.ProcuratorId == procuratorId).FirstOrDefault();

                if (procuratorEntity != null)
                {
                    if (procuratorEntity.AssociationsProcurators != null && procuratorEntity.AssociationsProcurators.Count > 0)
                    {
                        for (int i = 0; i < procuratorEntity.AssociationsProcurators.Count; i++)
                        {
                            procuratorEntity.AssociationsProcurators[i].SituationHistory = procuratorEntity.AssociationsProcurators[i].SituationHistory.OrderByDescending(p => p.OperationDate).ThenByDescending(p => p.CreationDate).ToList();
                        }
                    }

                    procuratorEntity.Honours = procuratorEntity.Honours.OrderByDescending(h => h.HonourDate).ToList();
                    procuratorEntity.Positions = procuratorEntity.Positions.OrderByDescending(p => p.ElectedDate).ThenByDescending(p => p.FiredDate).ToList();
                    procuratorEntity.ProcuratorAdherences = procuratorEntity.ProcuratorAdherences.OrderByDescending(p => p.StartDate).ThenByDescending(p => p.EndDate).ToList();
                    procuratorEntity.Suspensions = procuratorEntity.Suspensions.OrderByDescending(p => p.StartDate).ThenByDescending(p => p.EndDate).ToList();

                }

                return procuratorEntity;
            }
            catch (Exception ex)
            {
                var e = ex;
            }

            return null;
        }

        public Procurator Read(Guid procuratorId)
        {

            ProcuratorEntity entity = this.ReadProcuratorEntity(procuratorId);
            if (entity == null)
                return null;
            Procurator procurator = new Procurator();
            new ProcuratorEfMap().Map(entity, procurator);

            return procurator;
        }


        public Guid Update(Procurator newDomainProcurator, bool mapStateInMinistry)
        {
            return this.UpsertProcurator(newDomainProcurator, false, mapStateInMinistry);
        }

        public IQueryable<SituationChangeEntity> GetUseReportQuery(Guid? associationId, DateTime? startDate, DateTime? endDate,
            bool includeRegistrations, bool includeUpdates, bool includeDeregistrations)
        {
            List<Guid> operationTypes = new List<Guid>();
            if (includeRegistrations)
            {
                operationTypes.Add(OperationTypes.Add);
            }
            if (includeUpdates)
            {
                operationTypes.Add(OperationTypes.Update);
            }
            if (includeDeregistrations)
            {
                operationTypes.Add(OperationTypes.Delete);
            }

            IQueryable<SituationChangeEntity> query = uow.DbContext.ProcuratorSituationHistory.Where(
                sh => operationTypes.Contains(sh.OperationTypeId)
                && (!startDate.HasValue || startDate == null || sh.OperationDate >= startDate.Value)
                && (!endDate.HasValue || endDate == null || sh.OperationDate <= endDate.Value)
                && (!associationId.HasValue || associationId == null || associationId.Value == sh.AssociationId)
                )
                .Include("Procurator")
                .Include("Association")
                .Include("OperationType")
                .OrderByDescending(op => op.OperationDate)
                .AsQueryable<SituationChangeEntity>();

            return query;
        }

        public List<SituationChange> GetUseReportPage(Guid? associationId, DateTime? startDate, DateTime? endDate,
            bool includeRegistrations, bool includeUpdates, bool includeDeregistrations,
         int pageIndex, int pageSize, ref int totalRecords)
        {
            IQueryable<SituationChangeEntity> query = this.GetUseReportQuery(associationId, startDate, endDate,
                includeRegistrations, includeUpdates, includeDeregistrations);

            totalRecords = query.Select(p => p).Count();
            List<SituationChangeEntity> entities = query.Skip((pageSize) * pageIndex).Take(pageSize).ToList();
            List<SituationChange> situationChanges = new List<SituationChange>();
            SituationChange situationChange;
            foreach (SituationChangeEntity entity in entities)
            {
                situationChange = new SituationChange();
                new SituationChangeEfMap().Map(entity, situationChange);
                situationChanges.Add(situationChange);
            }
            return situationChanges;
        }


        public List<SituationChange> GetUseReportItems(Guid? associationId, DateTime? startDate, DateTime? endDate,
            bool includeRegistrations, bool includeUpdates, bool includeDeregistrations)
        {
            IQueryable<SituationChangeEntity> query =
                this.GetUseReportQuery(associationId, startDate, endDate,
                includeRegistrations, includeUpdates, includeDeregistrations);

            List<SituationChangeEntity> entities = query.ToList();
            List<SituationChange> situationChanges = new List<SituationChange>();
            SituationChange situationChange;
            foreach (SituationChangeEntity entity in entities)
            {
                situationChange = new SituationChange();
                new SituationChangeEfMap().Map(entity, situationChange);
                situationChanges.Add(situationChange);
            }
            return situationChanges;
        }

        private IQueryable<ProcuratorEntity> GetProcuratorsQuery(string fullName, string nif, Guid? associationId, string memberNumber,
             int? procuratorSituationId, string uniqueNumber, Guid? procuratorId, bool? onlyAccepted, Guid? agreementId)
        {
            IQueryable<ProcuratorEntity> query =
                uow.DbContext.Procurators
                .Include(procurator => procurator.Sex)
                .Include(procurator => procurator.AssociationsProcurators).ThenInclude(associationprocurator => associationprocurator.Association)
                .Include(procurator => procurator.AssociationsProcurators).ThenInclude(associationprocurator => associationprocurator.CurrentSituation)
                .Include(procurator => procurator.SituationHistory).ThenInclude(situationhistory => situationhistory.ProcuratorSituation)
                .Include(procurator => procurator.ProcuratorAdherences).ThenInclude(procuratoradherence => procuratoradherence.Agreement)
                .Include(procurator => procurator.ProcuratorAdherences).ThenInclude(procuratoradherence => procuratoradherence.Association)
                .Include(procurator => procurator.Suspensions)
                  .AsQueryable<ProcuratorEntity>();

            if (procuratorId.HasValue)
            {
                query = query.Where(p => p.ProcuratorId == procuratorId.Value);
            }
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                var splittedName = splitFullName(fullName);
                foreach (string value in splittedName)
                {
                    query = query.Where(p => p.FirstName.ToLower().Contains(value.ToLower()) || p.SecondName1.ToLower().Contains(value.ToLower()) || p.SecondName2.ToLower().Contains(value.ToLower()));
                }
            }
            if (!string.IsNullOrWhiteSpace(nif))
            {
                query = query.Where(p => p.Nif.ToLower().Equals(nif.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(uniqueNumber))
            {
                query = query.Where(p => p.UniqueNumber.ToLower().Equals(uniqueNumber.ToLower()));
            }

            bool onlyAcptd = true;
            if (onlyAccepted.HasValue)
            {
                onlyAcptd = onlyAccepted.Value;
            }

            if (associationId.HasValue)
            {

                query = query.Where(p => p.AssociationsProcurators.Where(a => a.AssociationId == associationId.Value && (!onlyAcptd || a.CreationStateId == CreationStateEnum.Accepted)).Count() > 0);

                if (!string.IsNullOrWhiteSpace(memberNumber))
                {
                    query = query.Where(p => p.AssociationsProcurators.Where(a => a.AssociationId == associationId.Value &&
                    (!onlyAcptd || a.CreationStateId == CreationStateEnum.Accepted) && a.MemberNumber == memberNumber).Count() > 0);
                }
                if (procuratorSituationId.HasValue)
                {
                    ProcuratorSituationEnum situ =
                        (ProcuratorSituationEnum)Enum.ToObject(typeof(ProcuratorSituationEnum), procuratorSituationId.Value);

                    query = query.Where(p => p.AssociationsProcurators.Where(a => a.AssociationId == associationId.Value &&
                    (
                        !onlyAcptd
                        || a.CreationStateId == CreationStateEnum.Accepted
                        || situ == ProcuratorSituationEnum.None)
                        && (a.CurrentSituationId == situ || (situ == ProcuratorSituationEnum.None && a.CreationStateId != CreationStateEnum.Accepted))).Any());

                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(memberNumber))
                {
                    query = query.Where(p => p.AssociationsProcurators.Where(a => (!onlyAcptd || a.CreationStateId == CreationStateEnum.Accepted) && a.MemberNumber == memberNumber).Count() > 0);
                }
                if (procuratorSituationId.HasValue)
                {
                    ProcuratorSituationEnum situ =
                        (ProcuratorSituationEnum)Enum.ToObject(typeof(ProcuratorSituationEnum), procuratorSituationId.Value);
                    query = query.Where(p => p.AssociationsProcurators.Where(a =>
                    (
                        !onlyAcptd
                        || a.CreationStateId == CreationStateEnum.Accepted
                        || situ == ProcuratorSituationEnum.None) && (a.CurrentSituationId == situ || (situ == ProcuratorSituationEnum.None && a.CreationStateId != CreationStateEnum.Accepted))).Any());
                }

            }


            if (onlyAcptd)
            {
                query = query.Where(p => p.CreationStateId == CreationStateEnum.Accepted);
            }

            if (agreementId.HasValue) // Sólo saco las adhesiones vivas.
            {
                DateTime today = new DateTime().GetMadridLocalDate();
                query = query.Where(p => p.ProcuratorAdherences.Where(a => a.AgreementId == agreementId.Value
                && a.StartDate <= today && (!a.EndDate.HasValue || a.EndDate > today)).Count() > 0);
            }

            return query;

        }


        #region Validación de prealtas de procuradores

        public List<Procurator> GetPendingProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords)
        {

            var query =
                uow.DbContext.Procurators
                .Include("CreatorAssociation")
                .Where(p =>
                p.CreationStateId == CreationStateEnum.Precreated
                && p.CreatorAssociationId.HasValue).OrderByDescending(p => p.CreationRequestDate).AsQueryable();

            var entities = query.Skip((pageSize) * pageIndex).Take(pageSize);

            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }

            totalRecords = query.Count();
            return procurators;
        }

        public List<Procurator> GetRejectedProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords)
        {

            var query =
                uow.DbContext.Procurators
                .Include("CreatorAssociation")
                .Where(p =>
                p.CreationStateId == CreationStateEnum.Rejected
                && p.CreatorAssociationId.HasValue).OrderByDescending(p => p.CreationRequestDate).AsQueryable();

            var entities = query.Skip((pageSize) * pageIndex).Take(pageSize);

            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }

            totalRecords = query.Count();
            return procurators;
        }

        public List<Procurator> GetAcceptedProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords)
        {

            var query =
                uow.DbContext.Procurators
                .Include("CreatorAssociation")
                .Where(p =>
                p.CreationStateId == CreationStateEnum.Accepted
                && p.CreatorAssociationId.HasValue).OrderByDescending(p => p.CreationRequestDate).AsQueryable();

            var entities = query.Skip((pageSize) * pageIndex).Take(pageSize);

            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }

            totalRecords = query.Count();
            return procurators;
        }

        #endregion Validación de prealtas de procuradores


        #region Lista de procuradores creados por colegio

        public List<Procurator> GetProcuratorCreationsByAssociationPage(Guid associationId, int pageIndex, int pageSize, ref int totalRecords)
        {

            var query =
                  uow.DbContext.Procurators
                  .Include("CreatorAssociation")
                  .Where(p => (p.CreatorAssociationId == associationId)).OrderByDescending(p => p.CreationRequestDate).AsQueryable();

            var entities = query.Skip((pageSize) * pageIndex).Take(pageSize);

            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }

            totalRecords = query.Count();
            return procurators;
        }

        #endregion Lista de procuradores creados por colegio





        public List<Procurator> GetProcuratorsPage(string fullName, string nif, Guid? associationId, string memberNumber,
         int? procuratorSituationId, string uniqueNumber, Guid? procuratorId, bool? onlyAccepted, Guid? agreementId,
         int pageIndex, int pageSize, ref int totalRecords)
        {

            IQueryable<ProcuratorEntity> query = this.GetProcuratorsQuery(fullName, nif, associationId, memberNumber,
                procuratorSituationId, uniqueNumber, procuratorId, onlyAccepted, agreementId);


            var entities = query.OrderBy(p => p.SecondName1).ThenBy(p => p.SecondName2).ThenBy(p => p.FirstName).Skip((pageSize) * pageIndex).Take(pageSize); ;

            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator, false);
                procurators.Add(procurator);
            }

            totalRecords = query.Count();
            return procurators;
        }

        public List<Procurator> GetProcuratorsListForCgpj()
        {
            var entities = uow.DbContext.Procurators
                .Include("Contacts")
                .Include("Contacts.ContactType")
                .Include("AssociationsProcurators")
                .Include("AssociationsProcurators.CurrentSituation")
                .Include("AssociationsProcurators.Association")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.Contacts")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.Contacts.ContactType")
                .Include("Suspensions")
                .Include("Suspensions.AssociationAgreeingOnSuspension")
                .Include("AssociationsProcurators.Cancellations")

            .Where(p => p.CreationStateId == CreationStateEnum.Accepted && p.IsPractisingInAnyAssociation && !p.HasCancelledMemberships && !p.HasThirdPartySuspensions && !p.HasSuspendedMemberships);
            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator, true);
                procurators.Add(procurator);
            }
            return procurators;
        }

        public Procurator GetProcuratorByNif(string nif)
        {
            ProcuratorEntity entity = uow.DbContext.Procurators
                .Include("AssociationsProcurators")
                .Include("AssociationsProcurators.CurrentSituation")
                .Include("AssociationsProcurators.Association")
                .Include("Suspensions")
                .Include("Suspensions.AssociationAgreeingOnSuspension")
                .Include("AssociationsProcurators.Cancellations")
                .Where(p => p.Nif == nif && p.CreationStateId == CreationStateEnum.Accepted).FirstOrDefault();

            if (entity != null)
            {

                Procurator proc = new Procurator();
                new ProcuratorEfMap().Map(entity, proc, true);
                return proc;
            }

            return null;
        }

        public List<Procurator> GetProcurators(string fullName, string nif, Guid? associationId, string memberNumber,
         int? procuratorSituationId, string uniqueNumber, Guid? procuratorId, Guid? agreementId)
        {
            IQueryable<ProcuratorEntity> query =
                this.GetProcuratorsQuery(fullName, nif, associationId, memberNumber,
                    procuratorSituationId, uniqueNumber, procuratorId, true, agreementId);

            List<ProcuratorEntity> entities = query.ToList();
            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }
            return procurators;
        }



        public List<Procurator> GetProcuratorsListForNotary()
        {
            var entities = uow.DbContext.Procurators
                .Include("AssociationsProcurators")
                .Include("AssociationsProcurators.CurrentSituation")
                .Include("AssociationsProcurators.Association")
                .Include("Suspensions")
                .Include("Suspensions.AssociationAgreeingOnSuspension")
                .Include("AssociationsProcurators.Cancellations")
                .Where(p => p.CreationStateId == CreationStateEnum.Accepted);

            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator, true);
                procurators.Add(procurator);
            }
            return procurators;
        }

        public List<Procurator> GetProcuratorsListForRegistrators()
        {
            var entities = uow.DbContext.Procurators
                .Include("AssociationsProcurators")
                .Include("AssociationsProcurators.CurrentSituation")
                .Include("AssociationsProcurators.Association")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.Contacts")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.Contacts.ContactType")
                .Include("Suspensions")
                .Include("Suspensions.AssociationAgreeingOnSuspension")
                .Include("AssociationsProcurators.Cancellations")
                .Where(p => p.CreationStateId == CreationStateEnum.Accepted && p.IsPractisingInAnyAssociation && !p.HasCancelledMemberships && !p.HasThirdPartySuspensions && !p.HasSuspendedMemberships);

            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator, true);
                procurators.Add(procurator);
            }
            return procurators;
        }

      
        private static string[] splitFullName(string fullName)
        {
            string[] splittedFullName = fullName.Split(' ');
            return splittedFullName;
        }

        #region Ministry Integration

        public List<Procurator> GetProcuratorsListByNifs(List<string> procuratorsNifs)
        {
            List<ProcuratorEntity> entities = uow.DbContext.Procurators.Where(p => procuratorsNifs.Contains(p.Nif)).Select(p => p).ToList();
            List<Procurator> items = new List<Procurator>();
            ProcuratorEfMap map = new ProcuratorEfMap();
            Procurator item;
            foreach (ProcuratorEntity entity in entities)
            {
                item = new Procurator();
                map.Map(entity, item);
                items.Add(item);
            }
            return items;
        }

        public List<Procurator> SyncGetPageOfRecords(Guid SyncId, int pageSize)
        {
            List<Procurator> procurators = new List<Procurator>();

            //Obtenemos la página a enviar
            var procuratorsChanged = this.uow.DbContext.ProcuratorSyncs.Where(a => a.TransacctionId == SyncId && a.Processed == false).Take(pageSize);
            var entities = this.uow.DbContext.Procurators
                .Include("Sex")

                .Include("Suspensions")
                .Include("Suspensions.AssociationAgreeingOnSuspension")

                .Include("AssociationsProcurators")

                .Include("AssociationsProcurators.Association")
                .Include("AssociationsProcurators.Association.HeadquartersAddress")
                .Include("AssociationsProcurators.Association.HeadquartersAddress.City")
                .Include("AssociationsProcurators.Association.HeadquartersAddress.Province")
                .Include("AssociationsProcurators.Association.HeadquartersAddress.WayType")
                .Include("AssociationsProcurators.Association.HeadquartersAddress.Contacts")
                .Include("AssociationsProcurators.Association.HeadquartersAddress.Contacts.ContactType")
                .Include("AssociationsProcurators.Association.HeadquartersAddress.AddressType")

                .Include("AssociationsProcurators.Cancellations")

                .Include("AssociationsProcurators.AssociationProcuratorAddresses")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.City")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.Province")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.WayType")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.Contacts")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.Contacts.ContactType")
                .Include("AssociationsProcurators.AssociationProcuratorAddresses.AddressType")

                .Include("AssociationsProcurators.CurrentSituation")

                .Where(p => procuratorsChanged.Any(p2 => p2.ProcuradorId == p.ProcuratorId));

            Procurator procurator = null;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator);

                if (procurator.AssociationsProcurators != null && procurator.AssociationsProcurators.Count > 0)
                {
                    for (int i = procurator.AssociationsProcurators.Count - 1; i >= 0; i--)
                    {
                        if (procurator.AssociationsProcurators[i].CreationStateId != CreationStateEnum.Accepted
                            || procurator.AssociationsProcurators[i].CurrentSituation.ProcuratorSituationId == ProcuratorSituationEnum.None)
                        {
                            procurator.AssociationsProcurators.RemoveAt(i);
                        }
                    }
                }
                procurators.Add(procurator);
            }
            return procurators;

            // PARECE QUE ESTÁ BIEN. YA VEREMOS SI NO FALTA ALGÚN INCLUDE.

        }

        public int SyncInitLoadTransaction(Guid syncId)
        {
            MinistryIntegrationStatesEnum startState;
            MinistryIntegrationStatesEnum endState;
            ProcuratorSyncEntity syncRecord;
            Guid AssociationId = Guid.Empty;
            int resultCount = 0;
            var result = this.uow.DbContext.Procurators.Include("AssociationsProcurators")
                .Where(p => p.CreationStateId == CreationStateEnum.Accepted &&
                p.StateInMinistry == MinistryIntegrationStatesEnum.Unregistered).AsNoTracking().ToList(); // TODO: ¿Viene bien el notracking?
            foreach (ProcuratorEntity procuratorEntity in result)
            {
                startState = procuratorEntity.StateInMinistry;
                endState = MinistryIntegrationStatesEnum.RegisteredSent;
                if (!procuratorEntity.CanUseSoftwarePlatforms) // TODO: Ojo con esto, tengo que asegurarme de que esto se está guardando bien. 
                {
                    endState = MinistryIntegrationStatesEnum.UnregisteredSent;
                }
                if (endState == MinistryIntegrationStatesEnum.UnregisteredSent && (startState == MinistryIntegrationStatesEnum.Unregistered || startState == MinistryIntegrationStatesEnum.UnregisteredSent))
                {
                    continue;
                }

                syncRecord = new ProcuratorSyncEntity()
                {
                    ProcuratorSyncId = Guid.NewGuid(),
                    Processed = false,
                    Accepted = false,
                    ProcuradorId = procuratorEntity.ProcuratorId,
                    TransacctionId = syncId
                };
                this.uow.DbContext.ProcuratorSyncs.Add(syncRecord);
                resultCount++;
            }
            return resultCount;
            // HASTA AQUÍ PARECE QUE ESTÁ BIEN 
        }

        public int SyncInitTransaction(Guid syncId, DateTime dateFrom, DateTime dateTo, string associationCode)
        {
            // Recupero los IDs de los procuradores que han sufrido cambios. Tal vez debería filtrar por tipo de árbol, pero eso no está en DU1.
            IQueryable<Guid> procuratorsChanged = this.uow.DbContext.Audits
                .Where(a => a.CreationDate >= dateFrom && a.CreationDate < dateTo)
                .AsNoTracking()
                .Select(a => a.RelatedTreeId); // Related tree = related object


            var result = this.uow.DbContext.Procurators.Include("AssociationsProcurators")
                .Where(p => p.CreationStateId == CreationStateEnum.Accepted
                            && procuratorsChanged.Contains(p.ProcuratorId)).AsNoTracking().ToList();

            Guid AssociationId = Guid.Empty;
            //Recuperamos el id del colegio en el caso de que se haya indicado como filtro
            if (!string.IsNullOrEmpty(associationCode))
            {
                var asso = this.uow.DbContext.Associations.Where(c => c.AssociationCode == associationCode).AsNoTracking().FirstOrDefault();
                if (asso == null)
                {
                    throw new System.ArgumentException(String.Format(Resources.InvalidAssociationCode, associationCode));
                }
                AssociationId = asso.AssociationId;
            }
            int resultCount = 0;
            ProcuratorEntity procurator;
            MinistryIntegrationStatesEnum startState;
            MinistryIntegrationStatesEnum endState;
            ProcuratorSyncEntity syncRecord;
            foreach (var cambio in result)
            {
                procurator = cambio;
                startState = procurator.StateInMinistry;
                endState = MinistryIntegrationStatesEnum.RegisteredSent;
                if (!procurator.CanUseSoftwarePlatforms)
                {
                    endState = MinistryIntegrationStatesEnum.UnregisteredSent;
                }
                if (endState == MinistryIntegrationStatesEnum.UnregisteredSent && (startState == MinistryIntegrationStatesEnum.Unregistered || startState == MinistryIntegrationStatesEnum.UnregisteredSent))
                {
                    continue;
                }
                if (AssociationId != Guid.Empty)
                {
                    if (this.uow.DbContext.AssociationProcurators
                        .Where(ap => ap.CreationStateId == CreationStateEnum.Accepted
                                        && ap.ProcuratorId == cambio.ProcuratorId
                                        && ap.AssociationId == AssociationId
                                        && ap.IsPractisingInAssociation())
                        .AsNoTracking().Any())
                    {
                        syncRecord = new ProcuratorSyncEntity()
                        {
                            ProcuratorSyncId = Guid.NewGuid(),
                            Processed = false,
                            Accepted = false,
                            ProcuradorId = cambio.ProcuratorId,
                            TransacctionId = syncId
                        };
                        this.uow.DbContext.ProcuratorSyncs.Add(syncRecord);
                        resultCount++;
                    }
                }
                else
                {
                    syncRecord = new ProcuratorSyncEntity()
                    {
                        ProcuratorSyncId = Guid.NewGuid(),
                        Processed = false,
                        Accepted = false,
                        ProcuradorId = cambio.ProcuratorId,
                        TransacctionId = syncId
                    };
                    this.uow.DbContext.ProcuratorSyncs.Add(syncRecord);
                    resultCount++;
                }
            }
            return resultCount;
            // EL MÉTODO PARECE BIEN MIGRADO. (2018-11-15 15:17)
        }

        public void SyncMarkAcceptedPage(Guid syncId, bool accepted)
        {

            var syncRecords = this.uow.DbContext.ProcuratorSyncs.Where(a => a.TransacctionId == syncId && a.Accepted != accepted);
            foreach (var cambio in syncRecords)
            {
                cambio.Accepted = accepted;
            }

            var procurators = this.uow.DbContext.Procurators.Where(p => (p.StateInMinistry == MinistryIntegrationStatesEnum.RegisteredSent || p.StateInMinistry == MinistryIntegrationStatesEnum.UnregisteredSent)
            && syncRecords.Select(s => s.ProcuradorId).Contains(p.ProcuratorId));

            foreach (var proc in procurators)
            {
                if (proc.StateInMinistry == MinistryIntegrationStatesEnum.RegisteredSent)
                {
                    proc.StateInMinistry = MinistryIntegrationStatesEnum.Registered;
                }
                else if (proc.StateInMinistry == MinistryIntegrationStatesEnum.UnregisteredSent)
                {
                    proc.StateInMinistry = MinistryIntegrationStatesEnum.Unregistered;
                }
            }

        }

        public void SyncRegisterSentNifs(Guid syncId, List<string> procuratorsWithErrorsNifs)
        {
            var syncRecords = this.uow.DbContext.ProcuratorSyncs.Where(a => a.TransacctionId == syncId && a.Accepted);

            var procurators = this.uow.DbContext
                .Procurators
                .Where(p => (procuratorsWithErrorsNifs == null || !procuratorsWithErrorsNifs.Contains(p.Nif))
                            && syncRecords.Select(s => s.ProcuradorId).Contains(p.ProcuratorId));

            foreach (var proc in procurators)
            {
                proc.LastNifSentToMinistry = proc.Nif;
            }

        }

        public void SyncMarkProcessedPage(Guid syncId, bool proccesed, List<Guid> ids)
        {
            var syncRecords = this.uow.DbContext.ProcuratorSyncs.Where(a => a.TransacctionId == syncId && ids.Contains(a.ProcuradorId));
            foreach (var cambio in syncRecords)
            {
                cambio.Processed = proccesed;
            }
        }

        public void UpdateProcuratorsSentState(List<Procurator> updateSet)
        {
            foreach (Procurator proc in updateSet)
            {
                ProcuratorEntity ent = this.uow.DbContext.Procurators.Where(a => a.ProcuratorId == proc.ProcuratorId).FirstOrDefault();
                if (ent != null)
                {
                    ent.StateInMinistry = proc.StateInMinistry;
                }
            }
        }

        #endregion


        #region BOE

        public List<Procurator> GetProcuratorsListForBoe()
        {

            DateTime localDate = new DateTime().GetMadridLocalDate();

            var entities =
                uow.DbContext.Procurators
                .Include("ProcuratorAdherences")
                .Include("ProcuratorAdherences.Agreement")
                .Include("ProcuratorAdherences.Address")
                .Include("ProcuratorAdherences.Address.Province")
                .Include("ProcuratorAdherences.Address.City")
                .Include("ProcuratorAdherences.Address.WayType")
                .Include("ProcuratorAdherences.Phone")
                .Include("ProcuratorAdherences.Mobile")
                .Include("ProcuratorAdherences.Email")
                .Where(p => p.CreationStateId == CreationStateEnum.Accepted
                            && !p.HasCancelledMemberships
                            && !p.HasSuspendedMemberships
                            && !p.HasThirdPartySuspensions
                            && p.ProcuratorAdherences
                            .Any(pa => pa.AgreementId == Agreements.BOE
                            && pa.StartDate <= localDate && (!pa.EndDate.HasValue || pa.EndDate.Value >= localDate)));

            List<Procurator> procurators = new List<Procurator>();
            Procurator procurator;
            foreach (ProcuratorEntity entity in entities)
            {
                procurator = new Procurator();
                new ProcuratorEfMap().Map(entity, procurator, true);
                procurators.Add(procurator);
            }

            return procurators;
        }


        #endregion BOE

    }
    public static class IQueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly PropertyInfo NodeTypeProviderField = QueryCompilerTypeInfo.DeclaredProperties.Single(x => x.Name == "NodeTypeProvider");

        private static readonly MethodInfo CreateQueryParserMethod = QueryCompilerTypeInfo.DeclaredMethods.First(x => x.Name == "CreateQueryParser");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        //public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        //{
        //    if (!(query is EntityQueryable<TEntity>) && !(query is InternalDbSet<TEntity>))
        //    {
        //        throw new ArgumentException("Invalid query");
        //    }

        //    var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
        //    var nodeTypeProvider = (INodeTypeProvider)NodeTypeProviderField.GetValue(queryCompiler);
        //    var parser = (IQueryParser)CreateQueryParserMethod.Invoke(queryCompiler, new object[] { nodeTypeProvider });
        //    var queryModel = parser.GetParsedQuery(query.Expression);
        //    var database = DataBaseField.GetValue(queryCompiler);
        //    var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
        //    var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
        //    var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
        //    modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
        //    var sql = modelVisitor.Queries.First().ToString();

        //    return sql;
        //}
    }
}
