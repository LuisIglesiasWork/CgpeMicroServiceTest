using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{
    
    public interface IDirectoryUserRepository
    {
        void Create(DirectoryUser user);
        DirectoryUser Read(string Id);
        void Update(DirectoryUser procurator, bool considerCertificates);
        DirectoryUser ReadByNif(string nif);
        bool CheckIfUserIdDocumentExists(string nif, string userId);
        List<DirectoryUser> GetUsersPage(string associationId, int pageIndex, int pageSize, ref int totalRecords);
    }

}
