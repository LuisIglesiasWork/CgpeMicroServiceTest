using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{
    internal class AuditEfMap
    {

        public void Map(AuditEntity source, Audit target)
        {
            target.AuditId = source.AuditId;
            target.CertificateSn = source.CertificateSn;
            target.CreationDate = source.CreationDate;
            target.DataAfter = source.DataAfter;
            target.DataBefore = source.DataBefore;
            target.OperationType = source.OperationType;
            target.RelatedTreeId = source.RelatedTreeId;
            target.RelatedTreeType = source.RelatedTreeType;
            target.SignedData = source.SignedData;
            target.UserId = source.UserId;
            if (source.User != null)
            {
                target.User = new DirectoryUser();
                new DirectoryUserEfMap().Map(source.User, target.User);
            }
            target.AppVersion = source.AppVersion;
        }

        public void Map(Audit source, AuditEntity target)
        {
            target.CertificateSn = source.CertificateSn;
            target.CreationDate = source.CreationDate;
            target.DataAfter = source.DataAfter;
            target.DataBefore = source.DataBefore;
            target.OperationType = source.OperationType;
            target.RelatedTreeId = source.RelatedTreeId;
            target.RelatedTreeType = source.RelatedTreeType;
            target.SignedData = source.SignedData;
            target.UserId = source.UserId;
            target.AppVersion = source.AppVersion;
        }

    }
}
