using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{

    public interface IAuditRepository
    {

        void Create(Audit audit);
        Audit Read(string auditId);
        List<Audit> ReadObjectHistory(string auditId);

    }

}
