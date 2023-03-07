using Cgpe.Du.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class IntegrationWorkflowEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid WorkflowId { get; set; }

        public IntegrationWorkflowStatesEnum CurrentState { get; set; }

        public int CurrentPage { get; set; }

        public int TotalRecordsNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastChangeDate { get; set; }

    }

}
