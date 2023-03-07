using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{
    
    public interface IDirectoryUserRepository
    {
        void Create(DirectoryUser user);
        DirectoryUser Read(Guid Id);
        void Update(DirectoryUser procurator, bool considerCertificates);
        DirectoryUser ReadByNif(string nif);
        bool CheckIfUserIdDocumentExists(string nif, Guid? userId);
        List<DirectoryUser> GetUsersPage(Guid? associationId, int pageIndex, int pageSize, ref int totalRecords);
    }

}
