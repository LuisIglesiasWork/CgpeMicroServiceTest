using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ProcuratorSyncEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProcuratorSyncId { get; set; }

        public string TransacctionId { get; set; }

        public string ProcuradorId { get; set; }

        public bool Processed { get; set; }

        public bool Accepted { get; set; }

    }

}
