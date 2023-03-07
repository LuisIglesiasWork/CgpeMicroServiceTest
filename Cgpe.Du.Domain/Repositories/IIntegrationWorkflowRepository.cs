using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{

    public interface IIntegrationWorkflowRepository
    {

        void Create(IntegrationWorkflow workflow);

        IntegrationWorkflow Read(Guid workflowId);

        void Update(IntegrationWorkflow workflow);

    }

}
