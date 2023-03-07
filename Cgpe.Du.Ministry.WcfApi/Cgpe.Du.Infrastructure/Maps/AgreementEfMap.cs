using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class AgreementEfMap
    {
        public void Map(AgreementEntity source, Agreement target)
        {
            target.AgreementId = source.AgreementId;
            target.AgreementCode = source.AgreementCode;
            target.AgreementName = source.AgreementName;
            target.ForceDate = source.ForceDate;
            target.AgreementDescription= source.AgreementDescription;
            target.RequiresAssociation = source.RequiresAssociation;
            target.RequiresBankAccount = source.RequiresBankAccount;
            target.RequiresContactData = source.RequiresContactData;
        }

        public void Map(Agreement source, AgreementEntity target)
        {
            target.AgreementId = source.AgreementId;
            target.AgreementCode = source.AgreementCode;
            target.AgreementName = source.AgreementName;
            target.ForceDate = source.ForceDate;
            target.AgreementDescription = source.AgreementDescription;
            target.RequiresAssociation = source.RequiresAssociation;
            target.RequiresBankAccount = source.RequiresBankAccount;
            target.RequiresContactData = source.RequiresContactData;
        }
    }

}
