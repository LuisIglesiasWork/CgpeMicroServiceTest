using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AdHocCountResultEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountResult { get; set; }
        
    }

}
