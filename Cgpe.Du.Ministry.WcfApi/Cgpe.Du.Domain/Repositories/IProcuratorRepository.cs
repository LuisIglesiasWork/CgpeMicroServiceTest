using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{

    public interface IProcuratorRepository
    {

        //void DeleteStateHistory(Guid stateHistoryId);

        //AssociationProcurator GetAssociationProcuratorById(Guid associationProcuratorId);

        //List<StateHistory> GetStateHistroy(ISpecification<StateHistory> specification);

        //List<Procurator> GetProcuratorsCgpeSyncList(string associationCode, DateTime? fromDate, DateTime? toDate);

        //List<Procurator> GetProcuratorsListForNotary();

        //List<Procurator> GetProcuratorsListForBoe();

        Procurator GetProcuratorByNif(string nif);

        Guid Create(Procurator procurator);

        Procurator Read(Guid procuratorId);

        Guid Update(Procurator procurator, bool mapStateInMinistry);

        List<Procurator> GetPendingProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords);
        List<Procurator> GetRejectedProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords);
        List<Procurator> GetAcceptedProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords);

        List<Procurator> GetProcuratorCreationsByAssociationPage(Guid associationId, int pageIndex, int pageSize, ref int totalRecords);

        List<Procurator> GetProcuratorsPage(string fullName, string nif, Guid? associationId, string memberNumber,
             int? procuratorSituationId, string uniqueNumber, Guid? procuratorId, bool? onlyAccepted, Guid? agreementId,
             int pageIndex, int pageSize, ref int totalRecords);

        List<Procurator> GetProcurators(string fullName, string nif, Guid? associationId, string memberNumber,
             int? procuratorSituationId, string uniqueNumber, Guid? procuratorId, Guid? agreementId);


        #region Ministry Integration

        List<Procurator> GetProcuratorsListByNifs(List<string> procuratorsNifs);
    
        int SyncInitTransaction(Guid SyncId, DateTime DateFrom, DateTime DateTo, string AssociationCode);

        int SyncInitLoadTransaction(Guid syncId);

        List<Procurator> SyncGetPageOfRecords(Guid SyncId, int pageSize);

        void SyncMarkProcessedPage(Guid SyncId, bool proccesed, List<Guid> ids);

        void SyncMarkAcceptedPage(Guid SyncId, bool accepted);

        void SyncRegisterSentNifs(Guid syncId, List<string> processedWithErrorNifs);

        List<Procurator> GetProcuratorsListForNotary();
        void UpdateProcuratorsSentState(List<Procurator> updateSet);
        
        #endregion

        List<Procurator> GetProcuratorsListForBoe();
        List<Procurator> GetProcuratorsListForCgpj();
        List<Procurator> GetProcuratorsListForRegistrators();


        List<SituationChange> GetUseReportPage(Guid? associationId, DateTime? startDate, DateTime? endDate,
           bool includeRegistrations, bool includeUpdates, bool includeDeregistrations,
        int pageIndex, int pageSize, ref int totalRecords);

        List<SituationChange> GetUseReportItems(Guid? associationId, DateTime? startDate, DateTime? endDate,
            bool includeRegistrations, bool includeUpdates, bool includeDeregistrations);

    }
}
