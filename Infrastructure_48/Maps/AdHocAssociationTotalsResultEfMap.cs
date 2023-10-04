using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class AdHocAssociationTotalsResultEfMap
    {
        public void Map(AdHocAssociationTotalsResultEntity source, AdHocAssociationTotalsResult target)
        {
            target.AssociationId = source.AssociationId;
            target.AssociationName = source.AssociationName;
            target.NumberOfPractising = source.NumberOfPractising;
            target.NumberOfNonPractising = source.NumberOfNonPractising;
            target.NumberOfUihjAdherences = source.NumberOfUihjAdherences;
        }
    }
}
