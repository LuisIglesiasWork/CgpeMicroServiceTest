using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;

namespace Cgpe.Du.Domain
{
    public interface IAssociationProcuratorRepository
    {
        CountResultByAssociation Count(Guid associationId);

        List<AssociationProcurator> GetPendingMembershipsPage(int pageIndex, int pageSize, ref int totalRecords);

        List<AssociationProcurator> GetRejectedMembershipsPage(int pageIndex, int pageSize, ref int totalRecords);
        List<AssociationProcurator> GetAcceptedMembershipsPage(int pageIndex, int pageSize, ref int totalRecords);

        List<AssociationProcurator> GetMembershipCreationsByAssociationPage(Guid associationId, int pageIndex, int pageSize, ref int totalRecords);

        Guid Create(AssociationProcurator associationProcurator);

        Guid Update(AssociationProcurator associationProcurator);

        AssociationProcurator Read(Guid associationProcuratorId);



    }
}
