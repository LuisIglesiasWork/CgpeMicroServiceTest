using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{
    
    public interface IIdentificationDocumentFileRepository
    {
        void Create(IdentificationDocumentFile identificationDocumentFile);
        void Delete(string fileId);
    }

}
