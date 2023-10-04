using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace Cgpe.Du.Domain
{

    public class CoreDomainService
    {

        #region Fields & Properties

        private IUnitOfWork uow;
        private IProcuratorRepository procuratorRepository;
        private IAssociationRepository associationRepository;
        private IAssociationProcuratorRepository associationProcuratorRepository;
        private IAdHocRepository adHocRepository;

        #endregion

        #region Construction & Desctruction

        public CoreDomainService(IUnitOfWork uow, IProcuratorRepository procuratorRepository, IAssociationRepository associationRepository, IAdHocRepository adHocRepository, IAssociationProcuratorRepository associationProcuratorRepository)
        {
            this.uow = uow;
            this.procuratorRepository = procuratorRepository;
            this.associationRepository = associationRepository;
            this.adHocRepository = adHocRepository;
            this.associationProcuratorRepository = associationProcuratorRepository;
        }

        #endregion

        #region Procurator

        

       
        public string CreateProcurator(Procurator procurator)
        {
            procurator.SetCreationStatusInformation();

            procurator.Validate();
            return this.procuratorRepository.Create(procurator);
        }

        public string PrecreateProcurator(Procurator procurator)
        {
            DateTime now = DateTime.UtcNow;

            procurator.StateInMinistry = MinistryIntegrationStatesEnum.Unregistered;
            //procurator.CreationStateId = CreationStateEnum.Precreated;
            procurator.CreationRequestDate = now;
            procurator.LatestCreationStateDate = now;

            if (procurator.AssociationsProcurators != null)
            {
                foreach (AssociationProcurator assoProc in procurator.AssociationsProcurators)
                {
                    //assoProc.CreationStateId = CreationStateEnum.Precreated;
                    assoProc.CreationRequestDate = now;
                    assoProc.LatestCreationStateDate = now;
                    assoProc.OrderSituationChanges();
                }
            }

            procurator.Validate();
            return this.procuratorRepository.Create(procurator);
        }

        public string PrecreateAssociationProcurator(AssociationProcurator associationProcurator)
        {
            DateTime now = DateTime.UtcNow;

            //associationProcurator.CreationStateId = CreationStateEnum.Precreated;
            associationProcurator.CreationRequestDate = now;
            associationProcurator.LatestCreationStateDate = now;

            associationProcurator.Validate();
            return this.associationProcuratorRepository.Create(associationProcurator);
        }

        public string AcceptAssociationProcurator(AssociationProcurator associationProcurator)
        {
            DateTime now = DateTime.UtcNow;

            //associationProcurator.CreationStateId = CreationStateEnum.Accepted;
            associationProcurator.LatestCreationStateDate = now;

            associationProcurator.Validate();
            return this.associationProcuratorRepository.Update(associationProcurator);
        }

        public string RejectAssociationProcurator(AssociationProcurator associationProcurator)
        {
            DateTime now = DateTime.UtcNow;

            //associationProcurator.CreationStateId = CreationStateEnum.Rejected;
            associationProcurator.LatestCreationStateDate = now;

            associationProcurator.Validate();
            return this.associationProcuratorRepository.Update(associationProcurator);
        }

        public string FixAssociationProcurator(AssociationProcurator associationProcurator)
        {
            DateTime now = DateTime.UtcNow;

           // associationProcurator.CreationStateId = CreationStateEnum.Precreated;
            associationProcurator.LatestCreationStateDate = now;
            associationProcurator.CreationStateReason = string.Empty;

            associationProcurator.Validate();
            return this.associationProcuratorRepository.Update(associationProcurator);
        }


        public Procurator ReadProcurator(string procuratorId)
        {
            return this.procuratorRepository.Read(procuratorId);
        }

        public AssociationProcurator ReadAssociationProcurator(string associationProcuratorId)
        {
            return this.associationProcuratorRepository.Read(associationProcuratorId);
        }

        public string UpdateProcurator(Procurator procurator)
        {
            if (procurator.AssociationsProcurators != null)
            {
                foreach (AssociationProcurator assoProc in procurator.AssociationsProcurators)
                {
                    assoProc.OrderSituationChanges();
                }
            }
            procurator.Validate();
            return this.procuratorRepository.Update(procurator, false);
        }

        public string AcceptProcurator(Procurator procurator)
        {
            DateTime now = DateTime.UtcNow;

            //procurator.CreationStateId = CreationStateEnum.Accepted;
            procurator.LatestCreationStateDate = now;

            if (procurator.AssociationsProcurators != null)
            {
                foreach (AssociationProcurator assoProc in procurator.AssociationsProcurators)
                {
                    assoProc.OrderSituationChanges();

                    //assoProc.CreationStateId = CreationStateEnum.Accepted;
                    assoProc.LatestCreationStateDate = now;
                }
            }

            procurator.Validate();
            return this.procuratorRepository.Update(procurator, false);
        }

        public string RejectProcurator(Procurator procurator)
        {
            DateTime now = DateTime.UtcNow;

           // procurator.CreationStateId = CreationStateEnum.Rejected;
            procurator.LatestCreationStateDate = now;

            if (procurator.AssociationsProcurators != null)
            {
                foreach (AssociationProcurator assoProc in procurator.AssociationsProcurators)
                {
                    assoProc.OrderSituationChanges();

                    //assoProc.CreationStateId = CreationStateEnum.Rejected;
                    assoProc.LatestCreationStateDate = now;
                }
            }
            procurator.Validate();
            return this.procuratorRepository.Update(procurator, false);
        }

        public string FixProcurator(Procurator procurator)
        {
            DateTime now = DateTime.UtcNow;

           // procurator.CreationStateId = CreationStateEnum.Precreated;
            procurator.LatestCreationStateDate = now;
            procurator.CreationStateReason = string.Empty;

            if (procurator.AssociationsProcurators != null)
            {
                foreach (AssociationProcurator assoProc in procurator.AssociationsProcurators)
                {
                    assoProc.OrderSituationChanges();

                    //assoProc.CreationStateId = CreationStateEnum.Precreated;
                    assoProc.LatestCreationStateDate = now;
                    assoProc.CreationStateReason = string.Empty;
                }
            }
            procurator.Validate();
            return this.procuratorRepository.Update(procurator, false);
        }

        public List<Procurator> GetPendingProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.procuratorRepository.GetPendingProcuratorsPage(pageIndex, pageSize, ref totalRecords);
        }

        public List<Procurator> GetRejectedProcuratorsPage(string associationId,
             int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.procuratorRepository.GetRejectedProcuratorsPage(pageIndex, pageSize, ref totalRecords);
        }

        public List<Procurator> GetAcceptedProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.procuratorRepository.GetAcceptedProcuratorsPage(pageIndex, pageSize, ref totalRecords);
        }



        public List<AssociationProcurator> GetPendingMembershipsPage(int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.associationProcuratorRepository.GetPendingMembershipsPage(pageIndex, pageSize, ref totalRecords);
        }

        public List<AssociationProcurator> GetRejectedMembershipsPage(int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.associationProcuratorRepository.GetRejectedMembershipsPage(pageIndex, pageSize, ref totalRecords);
        }

        public List<AssociationProcurator> GetAcceptedMembershipsPage(int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.associationProcuratorRepository.GetAcceptedMembershipsPage(pageIndex, pageSize, ref totalRecords);
        }



        public List<Procurator> GetProcuratorCreationsByAssociationPage(string associationId, int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.procuratorRepository.GetProcuratorCreationsByAssociationPage(associationId, pageIndex, pageSize, ref totalRecords);
        }

        public List<AssociationProcurator> GetMembershipCreationsByAssociationPage(string associationId, int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.associationProcuratorRepository.GetMembershipCreationsByAssociationPage(associationId, pageIndex, pageSize, ref totalRecords);
        }


        public List<Procurator> GetProcuratorsPage(string fullName, string nif, string associationId, string memberNumber,
             int? procuratorSituationId, string uniqueNumber, string procuratorId, bool? onlyAccepted, string agreementId,
             int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.procuratorRepository.GetProcuratorsPage(fullName, nif, associationId, memberNumber,
             procuratorSituationId, uniqueNumber, procuratorId, onlyAccepted, agreementId, pageIndex, pageSize, ref totalRecords);
        }

        public List<AdHocProcuratorResult> GetProcuratorsPerSexAndPractisePage(int? sexId, bool? isPractising,
             int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.adHocRepository.GetProcuratorsPerSexAndPractisePage(sexId, isPractising, pageIndex, pageSize, ref totalRecords);
        }

        public List<AdHocProcuratorResult> GetProcuratorsPerSexAndPractise(int? sexId, bool? isPractising)
        {
            return this.adHocRepository.GetProcuratorsPerSexAndPractise(sexId, isPractising);
        }

        public List<AdHocProcuratorResult> GetProcuratorsWithEmailsPage(int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.adHocRepository.GetProcuratorsWithEmailsPage(pageIndex, pageSize, ref totalRecords);
        }

        public List<AdHocProcuratorResult> GetProcuratorsWithEmails()
        {
            return this.adHocRepository.GetProcuratorsWithEmails();
        }

        public List<AdHocProcuratorResult> GetProcuratorsWithMagAddressPage(int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.adHocRepository.GetProcuratorsWithMagAddressPage(pageIndex, pageSize, ref totalRecords);
        }

        public List<AdHocProcuratorResult> GetProcuratorsWithMagAddress()
        {
            return this.adHocRepository.GetProcuratorsWithMagAddress();
        }

        public List<AdHocAssociationTotalsResult> GetAssociationsTotalsPage(int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.adHocRepository.GetAssociationsTotalsPage(pageIndex, pageSize, ref totalRecords);
        }

        public List<AdHocAssociationTotalsResult> GetAssociationsTotals()
        {
            return this.adHocRepository.GetAssociationsTotals();
        }

        public List<SituationChange> GetUseReportPage(string associationId, DateTime? startDate, DateTime? endDate,
            bool includeRegistrations, bool includeUpdates, bool includeDeregistrations,
            int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.procuratorRepository.GetUseReportPage(associationId, startDate, endDate, includeRegistrations, includeUpdates, includeDeregistrations,
                pageIndex, pageSize, ref totalRecords);
        }

        public List<SituationChange> GetUseReportItems(string associationId, DateTime? startDate, DateTime? endDate,
            bool includeRegistrations, bool includeUpdates, bool includeDeregistrations)
        {
            return this.procuratorRepository.GetUseReportItems(associationId, startDate, endDate, includeRegistrations, includeUpdates, includeDeregistrations);
        }

        public List<Procurator> GetProcurators(string fullName, string nif, string associationId, string memberNumber,
             int? procuratorSituationId, string uniqueNumber, string procuratorId, string agreementId)
        {
            return this.procuratorRepository.GetProcurators(fullName, nif, associationId, memberNumber,
             procuratorSituationId, uniqueNumber, procuratorId, agreementId);
        }

        #endregion

        #region Association

        public void CreateAssociation(Association association)
        {
            association.Validate();
            this.associationRepository.Create(association);
        }

        public Association ReadAssociation(string associationId)
        {
            return this.associationRepository.Read(associationId);
        }

        public Association ReadAssociationByNif(string associationNif)
        {
            return this.associationRepository.ReadByCif(associationNif);
        }

        public void UpdateAssociation(Association association)
        {
            association.Validate();
            this.associationRepository.Update(association);
        }

        public List<Association> GetAssociations()
        {
            return this.associationRepository.GetAssociations();
        }

        public bool CheckIfAssociationIdDocumentExists(string cif, string associationId)
        {
            return this.associationRepository.CheckIfAssociationIdDocumentExists(cif, associationId);
        }

        #endregion

    }

}
