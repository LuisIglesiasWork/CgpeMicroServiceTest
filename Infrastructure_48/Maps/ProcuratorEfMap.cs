using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class ProcuratorEfMap
    {

        public void Map(ProcuratorEntity source, Procurator target, bool onlyAcceptedAssociationsProcurators = false)
        {
            
            if (target.PersonalContacts == null)
            {
                target.PersonalContacts = new List<Contact>();
            }
            if (target.IdentificationDocumentFiles == null)
            {
                target.IdentificationDocumentFiles = new List<IdentificationDocumentFile>();
            }

            if (target.AssociationsProcurators == null)
                target.AssociationsProcurators = new List<AssociationProcurator>();
            if (target.ProcuratorAdherences == null)
                target.ProcuratorAdherences = new List<ProcuratorAdherence>();
            if (target.ProcuratorLanguages == null)
            {
                target.ProcuratorLanguages = new List<Language>();
            }
            if (target.Suspensions == null)
            {
                target.Suspensions = new List<Suspension>();
            }
            if (target.Positions == null)
            {
                target.Positions = new List<Position>();
            }
            if (target.Honours == null)
            {
                target.Honours = new List<Honour>();
            }
           

            target.BirthCity = source.BirthCity;
            target.BirthDate = source.BirthDate;
            target.FirstName = source.FirstName;
            target.LicencedLawDegreeDate = source.LicencedLawDegreeDate;

            /*target.LicencedLawDegreeAvailableCertificateYear = source.LicencedLawDegreeAvailableCertificateYear;
            target.LicencedLawDegreeAvailableCertificateMonth = source.LicencedLawDegreeAvailableCertificateMonth;
            target.LicencedLawDegreeAvailableCertificateDayOfMonth = source.LicencedLawDegreeAvailableCertificateDayOfMonth;*/

            target.MasterDate = source.MasterDate;
            target.Nif = source.Nif;
            target.LastNifSentToMinistry = source.LastNifSentToMinistry;
            target.ProcuratorDegreeDate = source.ProcuratorDegreeDate;
            target.ProcuratorId = source.ProcuratorId;
            target.SecondName1 = source.SecondName1;
            target.SecondName2 = source.SecondName2;

            if (source.Sex != null)
            {
                SexEfMap sexMapper = new SexEfMap();
                Sex sex = new Sex();
                sexMapper.Map(source.Sex, sex);
                target.Sex = sex;
            }

            target.GraduadedLawDegreeDate = source.GraduadedLawDegreeDate;
            target.UniqueNumber = source.UniqueNumber;
            target.StateInMinistry = source.StateInMinistry;
            target.Observations = source.Observations;
            target.OtherStudies = source.OtherStudies;
            target.DontSendNotifications = source.DontSendNotifications;

            if (source.PersonalAddress != null)
            {
                target.PersonalAddress = new Address();
                AddressEfMap addrMapper = new AddressEfMap();
                addrMapper.Map(source.PersonalAddress, target.PersonalAddress);
            }

            if (source.Contacts != null && source.Contacts.Count > 0)
            {
                Contact cont;
                ContactEfMap contMapper = new ContactEfMap();
                foreach (ContactEntity contEntity in source.Contacts)
                {
                    cont = new Contact();
                    contMapper.Map(contEntity, cont);
                    target.PersonalContacts.Add(cont);
                }
            }

            if (source.IdentificationDocumentFiles != null && source.IdentificationDocumentFiles.Count > 0)
            {
                IdentificationDocumentFile doc;
                IdentificationDocumentFileEfMap docMapper = new IdentificationDocumentFileEfMap();
                foreach (IdentificationDocumentFileEntity docEntity in source.IdentificationDocumentFiles)
                {
                    doc = new IdentificationDocumentFile();
                    docMapper.Map(docEntity, doc);
                    target.IdentificationDocumentFiles.Add(doc);
                }
            }

            if (source.ProcuratorLanguages != null && source.ProcuratorLanguages.Count > 0)
            {
                Language language;
                LanguageEfMap langMapper = new LanguageEfMap();
                foreach (ProcuratorLanguageEntity plEntity in source.ProcuratorLanguages)
                {
                    language = new Language();
                    langMapper.Map(plEntity.Language, language);
                    target.ProcuratorLanguages.Add(language);
                }
            }

            if (source.AssociationsProcurators != null && source.AssociationsProcurators.Count > 0)
            {
                AssociationProcurator ap;
                AssociationProcuratorEfMap apMapper = new AssociationProcuratorEfMap();
                foreach (AssociationProcuratorEntity apEntity in source.AssociationsProcurators)
                {
                    if (!onlyAcceptedAssociationsProcurators || onlyAcceptedAssociationsProcurators/* && apEntity.CreationStateId == CreationStateEnum.Accepted*/)
                    {
                        ap = new AssociationProcurator();
                        apMapper.Map(apEntity, ap, false);
                        target.AssociationsProcurators.Add(ap);
                    }
                }
            }

            if (source.Positions != null && source.Positions.Count > 0)
            {
                Position pos;
                PositionEfMap posMapper = new PositionEfMap();
                foreach (PositionEntity posEntity in source.Positions)
                {
                    pos = new Position();
                    posMapper.Map(posEntity, pos);
                    target.Positions.Add(pos);
                }
            }

            if (source.Suspensions != null && source.Suspensions.Count > 0)
            {
                Suspension susp;
                SuspensionEfMap suspMapper = new SuspensionEfMap();
                foreach (SuspensionEntity suspEntity in source.Suspensions)
                {
                    susp = new Suspension();
                    suspMapper.Map(suspEntity, susp);
                    target.Suspensions.Add(susp);
                }
            }

            if (source.ProcuratorAdherences != null && source.ProcuratorAdherences.Count > 0)
            {
                ProcuratorAdherence adh;
                ProcuratorAdherenceEfMap adhMapper = new ProcuratorAdherenceEfMap();
                foreach (ProcuratorAdherenceEntity adhEntity in source.ProcuratorAdherences)
                {
                    adh = new ProcuratorAdherence();
                    adhMapper.Map(adhEntity, adh);
                    target.ProcuratorAdherences.Add(adh);
                }
            }

            if (source.Honours != null && source.Honours.Count > 0)
            {
                Honour hon;
                HonourEfMap honMapper = new HonourEfMap();
                foreach (HonourEntity honEntity in source.Honours)
                {
                    hon = new Honour();
                    honMapper.Map(honEntity, hon);
                    target.Honours.Add(hon);
                }
            }

            //target.CreationStateId = source.CreationStateId;
            target.CreatorUserId = source.CreatorUserId;
            target.CreatorAssociationId = source.CreatorAssociationId;
            target.CreationStateReason = source.CreationStateReason;
            target.CreationRequestDate = source.CreationRequestDate;
            target.LatestCreationStateDate = source.LatestCreationStateDate;

            

            if(source.CreatorAssociation != null)
            {
                AssociationEfMap mapper = new AssociationEfMap();
                //target.CreatorAssociation = new Association();
                //mapper.Map(source.CreatorAssociation, target.CreatorAssociation);
            }
        }

        public void ShallowMap(ProcuratorEntity source, Procurator target)
        {
            if (target.PersonalContacts == null)
            {
                target.PersonalContacts = new List<Contact>();
            }
            if (target.IdentificationDocumentFiles == null)
            {
                target.IdentificationDocumentFiles = new List<IdentificationDocumentFile>();
            }

            if (target.AssociationsProcurators == null)
                target.AssociationsProcurators = new List<AssociationProcurator>();
            if (target.ProcuratorAdherences == null)
                target.ProcuratorAdherences = new List<ProcuratorAdherence>();
            if (target.ProcuratorLanguages == null)
            {
                target.ProcuratorLanguages = new List<Language>();
            }
            if (target.Suspensions == null)
            {
                target.Suspensions = new List<Suspension>();
            }
            if (target.Positions == null)
            {
                target.Positions = new List<Position>();
            }
            if (target.Honours == null)
            {
                target.Honours = new List<Honour>();
            }


            target.BirthCity = source.BirthCity;
            target.BirthDate = source.BirthDate;
            target.FirstName = source.FirstName;
            target.LicencedLawDegreeDate = source.LicencedLawDegreeDate;

           /* target.LicencedLawDegreeAvailableCertificateYear = source.LicencedLawDegreeAvailableCertificateYear;
            target.LicencedLawDegreeAvailableCertificateMonth = source.LicencedLawDegreeAvailableCertificateMonth;
            target.LicencedLawDegreeAvailableCertificateDayOfMonth = source.LicencedLawDegreeAvailableCertificateDayOfMonth;*/

            target.MasterDate = source.MasterDate;
            target.Nif = source.Nif;
            target.LastNifSentToMinistry = source.LastNifSentToMinistry;
            target.ProcuratorDegreeDate = source.ProcuratorDegreeDate;
            target.ProcuratorId = source.ProcuratorId;
            target.SecondName1 = source.SecondName1;
            target.SecondName2 = source.SecondName2;

     
            target.GraduadedLawDegreeDate = source.GraduadedLawDegreeDate;
            target.UniqueNumber = source.UniqueNumber;
            target.StateInMinistry = source.StateInMinistry;
            target.Observations = source.Observations;
            target.OtherStudies = source.OtherStudies;
            target.DontSendNotifications = source.DontSendNotifications;

           // target.CreationStateId = source.CreationStateId;
            target.CreatorUserId = source.CreatorUserId;
            target.CreatorAssociationId = source.CreatorAssociationId;
            target.CreationStateReason = source.CreationStateReason;
            target.CreationRequestDate = source.CreationRequestDate;
            target.LatestCreationStateDate = source.LatestCreationStateDate;

            

        }

        public void Map(Procurator source, ProcuratorEntity target, bool mapStateInMinistry)
        {
            if (target.Contacts == null)
            {
                target.Contacts = new List<ContactEntity>();
            }
            if (target.IdentificationDocumentFiles == null)
            {
                target.IdentificationDocumentFiles = new List<IdentificationDocumentFileEntity>();
            }
            if (target.AssociationsProcurators == null)
            {
                target.AssociationsProcurators = new List<AssociationProcuratorEntity>();
            }
            if (target.ProcuratorAdherences == null)
                target.ProcuratorAdherences = new List<ProcuratorAdherenceEntity>();
            if (target.ProcuratorLanguages == null)
            {
                target.ProcuratorLanguages = new List<ProcuratorLanguageEntity>();
            }
            if (target.Suspensions == null)
            {
                target.Suspensions = new List<SuspensionEntity>();
            }
            if (target.Positions == null)
            {
                target.Positions = new List<PositionEntity>();
            }
            if (target.Honours == null)
            {
                target.Honours = new List<HonourEntity>();
            }
            if (target.IdentificationDocumentFiles == null)
            {
                target.IdentificationDocumentFiles = new List<IdentificationDocumentFileEntity>();
            }


            target.BirthCity = source.BirthCity;
            target.BirthDate = source.BirthDate;
            target.FirstName = source.FirstName;
            target.LicencedLawDegreeDate = source.LicencedLawDegreeDate;

           /* target.LicencedLawDegreeAvailableCertificateYear = source.LicencedLawDegreeAvailableCertificateYear;
            target.LicencedLawDegreeAvailableCertificateMonth = source.LicencedLawDegreeAvailableCertificateMonth;
            target.LicencedLawDegreeAvailableCertificateDayOfMonth = source.LicencedLawDegreeAvailableCertificateDayOfMonth;*/

            target.MasterDate = source.MasterDate;
            target.Nif = source.Nif;
    
            target.ProcuratorDegreeDate = source.ProcuratorDegreeDate;
            target.SecondName1 = source.SecondName1;
            target.SecondName2 = source.SecondName2;

            target.SexId = source.Sex.SexId;
            target.GraduadedLawDegreeDate = source.GraduadedLawDegreeDate;
            target.UniqueNumber = source.UniqueNumber;
            if (mapStateInMinistry)
            {
                target.StateInMinistry = source.StateInMinistry;
            }
            target.Observations = source.Observations;
            target.OtherStudies = source.OtherStudies;
            target.DontSendNotifications = source.DontSendNotifications;

            
            target.IsPractisingInAnyAssociation = source.CheckIfPractisingInAnyAssociation();
            target.HasSuspendedMemberships = source.CheckIfAnySuspendedMemberships();
            target.HasThirdPartySuspensions = source.CheckIfAnyThirdPartySuspensions();
            target.HasCancelledMemberships = source.CheckIfAnyCancelledMemberships();
            target.CanUsePrivateWebsite = source.CheckIfCanUsePrivateWebsite();
            target.CanUseSoftwarePlatforms = source.CheckIfCanUseSoftwarePlatforms();

            target.CurrentThirdPartySuspensionDate = source.GetCurrentThirdPartySuspensionDate();


            //target.CreationStateId = source.CreationStateId;
            target.CreatorUserId = source.CreatorUserId;
            target.CreatorAssociationId = source.CreatorAssociationId;
            target.CreationStateReason = source.CreationStateReason;
            target.CreationRequestDate = source.CreationRequestDate;
            target.LatestCreationStateDate = source.LatestCreationStateDate;

        }

    }

}
