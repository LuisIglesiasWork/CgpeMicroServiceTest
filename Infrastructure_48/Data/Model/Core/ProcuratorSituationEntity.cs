using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ProcuratorSituationEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ProcuratorSituationEnum ProcuratorSituationId { get; set; }

        [Required, MaxLength(150)]
        public string ProcuratorSituationName { get; set; }
    }

}
