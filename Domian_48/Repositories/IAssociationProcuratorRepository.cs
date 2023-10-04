using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;

namespace Cgpe.Du.Domain
{
    public interface IAssociationProcuratorRepository
    {
        CountResultByAssociation Count(string associationId);

        List<AssociationProcurator> GetPendingMembershipsPage(int pageIndex, int pageSize, ref int totalRecords);

        List<AssociationProcurator> GetRejectedMembershipsPage(int pageIndex, int pageSize, ref int totalRecords);
        List<AssociationProcurator> GetAcceptedMembershipsPage(int pageIndex, int pageSize, ref int totalRecords);

        List<AssociationProcurator> GetMembershipCreationsByAssociationPage(string associationId, int pageIndex, int pageSize, ref int totalRecords);

        string Create(AssociationProcurator associationProcurator);

        string Update(AssociationProcurator associationProcurator);

        AssociationProcurator Read(string associationProcuratorId);



    }
}
