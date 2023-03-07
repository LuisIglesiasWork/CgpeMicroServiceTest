using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{

    public interface IAuditRepository
    {

        void Create(Audit audit);
        Audit Read(Guid auditId);
        List<Audit> ReadObjectHistory(Guid auditId);

    }

}
