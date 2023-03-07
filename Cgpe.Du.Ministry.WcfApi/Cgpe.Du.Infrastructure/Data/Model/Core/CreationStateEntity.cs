using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class CreationStateEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public CreationStateEnum StateId { get; set; }

        [Required, MaxLength(15)]
        public string StateName { get; set; }

    }

}
