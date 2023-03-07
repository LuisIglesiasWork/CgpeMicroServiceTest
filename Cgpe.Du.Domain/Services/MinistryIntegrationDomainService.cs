using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Cgpe.Du.Domain
{

    public class MinistryIntegrationDomainService
    {

        private IUnitOfWork uow;
        private IProcuratorRepository procuratorRepository;
        private IIntegrationWorkflowRepository integrationWorkflowRepository;
        private AuditDomainService auditService;

        public MinistryIntegrationDomainService(IUnitOfWork uow, IProcuratorRepository procuratorRepository, IIntegrationWorkflowRepository integrationWorkflowRepository, IAuditRepository auditRepository)
        {
            this.uow = uow;
            this.procuratorRepository = procuratorRepository;
            this.integrationWorkflowRepository = integrationWorkflowRepository;
            this.auditService = new AuditDomainService(uow, auditRepository);
        }

        #region Integración con Ministerio de Justicia

        public int SyncInitTransaction(Guid transactionId, DateTime dateFrom, DateTime dateTo, String associationCode, bool isInitialLoad)
        {
            int result = 0;
            try
            {
                if (isInitialLoad)
                {
                    result = this.procuratorRepository.SyncInitLoadTransaction(transactionId);
                }
                else
                {
                    result = this.procuratorRepository.SyncInitTransaction(transactionId, dateFrom, dateTo, associationCode);
                }
                this.uow.Commit();
            }
            catch
            {
                this.uow.Rollback();
                throw;
            }
            return result;
            // ESTE MÉTODO PARECE ESTAR BIEN MIGRADO (2018-11-15 14:39)
        }

        public List<Procurator> SyncGetPageOfRecords(Guid TransactionId, int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentException(Resources.warning_InvalidArgumentForFindRecords);
            return procuratorRepository.SyncGetPageOfRecords(TransactionId, pageSize);
        }

        public void SyncMarkProcessedPage(Guid SyncId, bool proccesed, List<Guid> changeIds)
        {
            this.procuratorRepository.SyncMarkProcessedPage(SyncId, proccesed, changeIds);
            this.uow.Commit();
        }

        public void SyncMarkAcceptedPage(Guid SyncId, bool accepted, List<string> procuratorsWithErrorsNifs)
        {
            this.procuratorRepository.SyncMarkAcceptedPage(SyncId, accepted);
            this.uow.Commit();
            this.procuratorRepository.SyncRegisterSentNifs(SyncId, procuratorsWithErrorsNifs);
            this.uow.Commit();
        }

        public void SyncMarkProcuratorsWithError(List<string> procuratorsNifs)
        {
            List<Procurator> procurators = this.procuratorRepository.GetProcuratorsListByNifs(procuratorsNifs);
            foreach (Procurator procurator in procurators)
            {
                if (procurator.StateInMinistry == MinistryIntegrationStatesEnum.RegisteredSent)
                    procurator.StateInMinistry = MinistryIntegrationStatesEnum.Unregistered;
                else if (procurator.StateInMinistry == MinistryIntegrationStatesEnum.UnregisteredSent)
                    procurator.StateInMinistry = MinistryIntegrationStatesEnum.Registered;
                this.auditService.AuditOperation(null, null, null, OperationTypes.MinistrySyncError, procurator.ProcuratorId, TreeTypes.Procurator);
            }
        }

        public void SyncRegisterSentNifs(Guid syncId, List<string> processedWithErrorNifs)
        {
            this.procuratorRepository.SyncRegisterSentNifs(syncId, processedWithErrorNifs);
            this.uow.Commit();
        }

        public void UpdateSentProcurators(List<Procurator> sentProcs)
        {
            if (sentProcs != null && sentProcs.Count > 0)
            {
                for (int i = 0; i < sentProcs.Count; i++)
                {
                    sentProcs[i].LastNifSentToMinistry = sentProcs[i].Nif;
                    this.procuratorRepository.Update(sentProcs[i], true);
                }
                this.uow.Commit();
            }
        }

        public void Rollback()
        {
            this.uow.Rollback();
        }

        #endregion

        #region IntegrationWorkflow

        public IntegrationWorkflow CreateIntegrationWorkflowInstance(bool commit)
        {
            IntegrationWorkflow instance = new IntegrationWorkflow()
            {
                WorkflowId = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                CurrentState = IntegrationWorkflowStatesEnum.INICIADO,
                LastChangeDate = DateTime.Now
            };
            this.integrationWorkflowRepository.Create(instance);
            if (commit)
                this.uow.Commit();
            return instance;
            // Hasta aquí parece que está ya bien.
        }

        public void CreateIntegrationWorkflowInstanceCommit()
        {
            this.uow.Commit();
        }

        public IntegrationWorkflow GetIntegrationWorkflowInstance(Guid instanceId)
        {
            return this.integrationWorkflowRepository.Read(instanceId);
        }

        public IntegrationWorkflow SaveIntegrationWorkflowInstance(IntegrationWorkflow instance)
        {
            instance.LastChangeDate = DateTime.Now;
            this.integrationWorkflowRepository.Update(instance);
            this.uow.Commit();
            return instance;
            // ESTÁ BIEN
        }

        public void UpdateProcuratorsSentState(List<Procurator> updateSet)
        {
            this.procuratorRepository.UpdateProcuratorsSentState(updateSet);
            this.uow.Commit();
        }

        #endregion

    }

}
