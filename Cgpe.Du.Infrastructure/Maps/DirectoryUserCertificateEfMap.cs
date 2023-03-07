using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class DirectoryUserCertificateEfMap
    {

        public void Map(DirectoryUserCertificateEntity source, DirectoryUserCertificate target)
        {
            target.CertificateId = source.CertificateId;
            target.PublicKey = source.PublicKey;
            target.SerialNumber = source.SerialNumber;
            target.CreationDate = source.CreationDate;
            target.ActiveFrom = source.ActiveFrom;
            target.ActiveTo = source.ActiveTo;
            target.Active = source.Active;
        }

        public void Map(DirectoryUserCertificate source, DirectoryUserCertificateEntity target, Guid userId)
        {
            target.CertificateId = source.CertificateId;
            target.PublicKey = source.PublicKey;
            target.SerialNumber = source.SerialNumber;
            target.CreationDate = source.CreationDate;
            target.ActiveFrom = source.ActiveFrom;
            target.ActiveTo = source.ActiveTo;
            target.Active = source.Active;
            target.UserId = userId;
        }
    }

}


