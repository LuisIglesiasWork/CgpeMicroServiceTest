using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public class AdHocAssociationTotalsResult
    {

        public string AssociationId { get; set; }
        public string AssociationName { get; set; }
        public int Total
        {
            get
            {
                return this.NumberOfNonPractising + this.NumberOfPractising;
            }
        }
        public int NumberOfPractising { get; set; }
        public int NumberOfNonPractising { get; set; }
        public int NumberOfUihjAdherences { get; set; }

    }

}
