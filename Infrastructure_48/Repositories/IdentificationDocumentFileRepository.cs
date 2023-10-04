using Cgpe.Du.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cgpe.Du.Infrastructure
{

    public class IdentificationDocumentFileRepository : IIdentificationDocumentFileRepository
    {

        private DuUnitOfWork uow;

        public IdentificationDocumentFileRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }

        public void Create(IdentificationDocumentFile idDocumentFile)
        {
            IdentificationDocumentFileEntity entity = new IdentificationDocumentFileEntity();
            new IdentificationDocumentFileEfMap().Map(idDocumentFile, entity, null);
            
            uow.DbContext.IdentificationDocumentFiles.Add(entity);
        }

        public void Delete(string fileId)
        {
            IdentificationDocumentFileEntity entity = uow.DbContext.IdentificationDocumentFiles.Where(c => c.IdentificationDocumentId == fileId).FirstOrDefault();
            if (entity == null)
                return;
            uow.DbContext.IdentificationDocumentFiles.Remove(entity);
        }
    }

}
