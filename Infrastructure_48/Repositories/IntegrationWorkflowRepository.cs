using Cgpe.Du.Domain;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;

namespace Cgpe.Du.Infrastructure
{

    public class IntegrationWorkflowRepository : IIntegrationWorkflowRepository
    {

        private DuUnitOfWork uow;

        public IntegrationWorkflowRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }

        public void Create(IntegrationWorkflow workflow)
        {
            workflow.WorkflowId = Guid.NewGuid().ToString();
            IntegrationWorkflowEntity entity = new IntegrationWorkflowEntity()
            {
                WorkflowId = workflow.WorkflowId,
                CreationDate = workflow.CreationDate,
                CurrentPage = workflow.CurrentPage,
                CurrentState = workflow.CurrentState,
                LastChangeDate = workflow.LastChangeDate,
                TotalRecordsNumber = workflow.TotalRecordsNumber
            };
            uow.DbContext.IntegrationWorkflows.Add(entity);
        }

        public IntegrationWorkflow Read(string workflowId)
        {
            IntegrationWorkflowEntity entity = (from iwf in uow.DbContext.IntegrationWorkflows where iwf.WorkflowId == workflowId select iwf).FirstOrDefault();
            if (entity == null)
                return null;
            IntegrationWorkflow item = new IntegrationWorkflow()
            {
                WorkflowId = entity.WorkflowId,
                CreationDate = entity.CreationDate,
                CurrentPage = entity.CurrentPage,
                CurrentState = entity.CurrentState,
                LastChangeDate = entity.LastChangeDate,
                TotalRecordsNumber = entity.TotalRecordsNumber
            };
            return item;
        }

        public void Update(IntegrationWorkflow workflow)
        {
            IntegrationWorkflowEntity entity = (from iwf in uow.DbContext.IntegrationWorkflows where iwf.WorkflowId == workflow.WorkflowId select iwf).FirstOrDefault();
            if (entity == null)
                throw new NullReferenceException($"Workflow with id {workflow.WorkflowId} was not found in the Data Base.");
            entity.CurrentPage = workflow.CurrentPage;
            entity.CurrentState = workflow.CurrentState;
            entity.LastChangeDate = workflow.LastChangeDate;
            entity.TotalRecordsNumber = workflow.TotalRecordsNumber;
        }

    }

}
