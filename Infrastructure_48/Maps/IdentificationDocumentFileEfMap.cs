using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class IdentificationDocumentFileEfMap
    {

        public void Map(IdentificationDocumentFileEntity source, IdentificationDocumentFile target)
        {
            target.IdentificationDocumentId = source.IdentificationDocumentId;
            target.OriginalFileName = source.OriginalFileName;
            target.OriginalFileExtension = source.OriginalFileExtension;
            target.ExternalFileFullName = source.ExternalFileFullName;
            target.FileSize = source.FileSize;
            target.Hash = source.Hash;
            target.ProcuratorId = source.ProcuratorId;
        }

        public void Map(IdentificationDocumentFile source, IdentificationDocumentFileEntity target, string procuratorId, bool isNew = false)
        {
            if (isNew)
            {
                source.IdentificationDocumentId = Guid.NewGuid().ToString();
            }

            target.IdentificationDocumentId = source.IdentificationDocumentId;
            target.OriginalFileName = source.OriginalFileName;
            target.OriginalFileExtension = source.OriginalFileExtension;
            target.ExternalFileFullName = source.ExternalFileFullName;
            target.FileSize = source.FileSize;
            target.Hash = source.Hash;
            if(procuratorId != null)
            {
                source.ProcuratorId = procuratorId;
            }
            target.ProcuratorId = source.ProcuratorId;
        }
    }
}
