using System;

namespace Cgpe.Du.Domain.Entities
{

    public class IntegrationWorkflow
    {

        public Guid WorkflowId { get; set; }

        public IntegrationWorkflowStatesEnum CurrentState { get; set; }

        public int CurrentPage { get; set; }

        public int TotalRecordsNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastChangeDate { get; set; }

    }

}
