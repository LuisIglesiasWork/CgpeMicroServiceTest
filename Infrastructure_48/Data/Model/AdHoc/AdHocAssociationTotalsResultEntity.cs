using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AdHocAssociationTotalsResultEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 RowNum { get; set; }
        public string AssociationId { get; set; }
        public string AssociationName { get; set; }
        public int NumberOfPractising { get; set; }
        public int NumberOfNonPractising { get; set; }
        public int NumberOfUihjAdherences { get; set; }

    }

}
