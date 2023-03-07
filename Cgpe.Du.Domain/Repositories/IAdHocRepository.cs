using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Cgpe.Du.Domain
{

    public interface IAdHocRepository
    {
        List<AdHocProcuratorResult> GetProcuratorsPerSexAndPractisePage(int? sexId, bool? isPractising,
         int pageIndex, int pageSize, ref int totalRecords);
        List<AdHocProcuratorResult> GetProcuratorsPerSexAndPractise(int? sexId, bool? isPractising);


        List<AdHocProcuratorResult> GetProcuratorsWithEmailsPage(int pageIndex, int pageSize, ref int totalRecords);
        List<AdHocProcuratorResult> GetProcuratorsWithEmails();

        List<AdHocProcuratorResult> GetProcuratorsWithMagAddressPage(int pageIndex, int pageSize, ref int totalRecords);
        List<AdHocProcuratorResult> GetProcuratorsWithMagAddress();
        List<AdHocAssociationTotalsResult> GetAssociationsTotalsPage(int pageIndex, int pageSize, ref int totalRecords);
        List<AdHocAssociationTotalsResult> GetAssociationsTotals();
    }

}
