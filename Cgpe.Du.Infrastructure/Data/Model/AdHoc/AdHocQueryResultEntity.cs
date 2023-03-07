using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class AdHocQueryResultEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PrimaryKey { get; set; }
        public string Text001 { get; set; }
        public string Text002 { get; set; }
        public bool? Bool001 { get; set; }
        public bool? Bool002 { get; set; }

        public int? Int001 { get; set; }
        public int? Int002 { get; set; }
        public int? Int003 { get; set; }

    }

}
