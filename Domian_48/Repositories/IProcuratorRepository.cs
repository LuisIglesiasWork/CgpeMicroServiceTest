using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{

    public interface IProcuratorRepository
    {

        //void DeleteStateHistory(string stateHistoryId);

        //AssociationProcurator GetAssociationProcuratorById(string associationProcuratorId);

        //List<StateHistory> GetStateHistroy(ISpecification<StateHistory> specification);

        //List<Procurator> GetProcuratorsCgpeSyncList(string associationCode, DateTime? fromDate, DateTime? toDate);

        //List<Procurator> GetProcuratorsListForNotary();

        //List<Procurator> GetProcuratorsListForBoe();

        Procurator GetProcuratorByNif(string nif);

        string Create(Procurator procurator);

        Procurator Read(string procuratorId);

        string Update(Procurator procurator, bool mapStateInMinistry);

        List<Procurator> GetPendingProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords);
        List<Procurator> GetRejectedProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords);
        List<Procurator> GetAcceptedProcuratorsPage(int pageIndex, int pageSize, ref int totalRecords);

        List<Procurator> GetProcuratorCreationsByAssociationPage(string associationId, int pageIndex, int pageSize, ref int totalRecords);

        List<Procurator> GetProcuratorsPage(string fullName, string nif, string associationId, string memberNumber,
             int? procuratorSituationId, string uniqueNumber, string procuratorId, bool? onlyAccepted, string agreementId,
             int pageIndex, int pageSize, ref int totalRecords);

        List<Procurator> GetProcurators(string fullName, string nif, string associationId, string memberNumber,
             int? procuratorSituationId, string uniqueNumber, string procuratorId, string agreementId);


        #region Ministry Integration

        List<Procurator> GetProcuratorsListByNifs(List<string> procuratorsNifs);
    
        int SyncInitTransaction(string SyncId, DateTime DateFrom, DateTime DateTo, string AssociationCode);

        int SyncInitLoadTransaction(string syncId);

        List<Procurator> SyncGetPageOfRecords(string SyncId, int pageSize);

        void SyncMarkProcessedPage(string SyncId, bool proccesed, List<string> ids);

        void SyncMarkAcceptedPage(string SyncId, bool accepted);

        void SyncRegisterSentNifs(string syncId, List<string> processedWithErrorNifs);

        List<Procurator> GetProcuratorsListForNotary();
        void UpdateProcuratorsSentState(List<Procurator> updateSet);
        
        #endregion

        List<Procurator> GetProcuratorsListForBoe();
        List<Procurator> GetProcuratorsListForCgpj();
        List<Procurator> GetProcuratorsListForRegistrators();


        List<SituationChange> GetUseReportPage(string associationId, DateTime? startDate, DateTime? endDate,
           bool includeRegistrations, bool includeUpdates, bool includeDeregistrations,
        int pageIndex, int pageSize, ref int totalRecords);

        List<SituationChange> GetUseReportItems(string associationId, DateTime? startDate, DateTime? endDate,
            bool includeRegistrations, bool includeUpdates, bool includeDeregistrations);

    }
}
