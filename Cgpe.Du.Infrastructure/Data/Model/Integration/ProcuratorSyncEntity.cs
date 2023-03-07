using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ProcuratorSyncEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProcuratorSyncId { get; set; }

        public Guid TransacctionId { get; set; }

        public Guid ProcuradorId { get; set; }

        public bool Processed { get; set; }

        public bool Accepted { get; set; }

    }

}
