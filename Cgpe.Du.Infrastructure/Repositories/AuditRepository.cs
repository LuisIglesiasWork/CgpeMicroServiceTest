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

    public class AuditRepository : IAuditRepository
    {

        private DuUnitOfWork uow;

        public AuditRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }

        public void Create(Audit audit)
        {
            AuditEntity entity = new AuditEntity()
            {
                AuditId = Guid.NewGuid()
            };
            new AuditEfMap().Map(audit, entity);
            audit.AuditId = entity.AuditId;
            uow.DbContext.Audits.Add(entity);
        }

        private AuditEntity ReadAuditEntity(Guid auditId)
        {
            AuditEntity auditEntity = uow.DbContext.Audits
                .Where(a => a.AuditId == auditId).FirstOrDefault();

            return auditEntity;
        }

        public Audit Read(Guid auditId)
        {
            AuditEntity entity = this.ReadAuditEntity(auditId);

            if (entity == null)
                return null;
            Audit audit = new Audit();
            new AuditEfMap().Map(entity, audit);
            return audit;
        }

        public List<Audit> ReadObjectHistory(Guid objectId)
        {
            List<Audit> audits = new List<Audit>();
            List<AuditEntity> entities = this.uow.DbContext.Audits
                .Include(a => a.User)
                .Where(a => a.RelatedTreeId == objectId)
                .OrderByDescending(a => a.CreationDate)
                .ToList();

            if (entities != null)
            {
                AuditEfMap mapper = new AuditEfMap();
                Audit audit = null;
                foreach (AuditEntity ent in entities)
                {
                    audit = new Audit();
                    mapper.Map(ent, audit);
                    audits.Add(audit);
                }
            }

            return audits;
        }
    }
}
