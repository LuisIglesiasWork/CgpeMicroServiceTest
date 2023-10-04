using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class SexEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public SexEnum SexId { get; set; }

        [Required, MaxLength(15)]
        public string SexName { get; set; }

    }

}
