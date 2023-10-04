using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{

    public interface IIntegrationWorkflowRepository
    {

        void Create(IntegrationWorkflow workflow);

        IntegrationWorkflow Read(string workflowId);

        void Update(IntegrationWorkflow workflow);

    }

}
