using Cgpe.Du.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace Cgpe.Du.Domain
{

    public class AuditDomainService
    {

        private IUnitOfWork uow;
        private IAuditRepository auditRepository;

        public AuditDomainService(IUnitOfWork uow, IAuditRepository auditRepository)
        {
            this.uow = uow;
            this.auditRepository = auditRepository;
        }

        public Audit GetAudit(Guid auditId)
        {
            return this.auditRepository.Read(auditId);
        }

        public List<Audit> GetObjectAuditHistory(Guid objectId)
        {
            return this.auditRepository.ReadObjectHistory(objectId);
        }

        public void AuditOperation(string before, string after, string signedData, Guid operation, Guid relatedTreeId, Guid relatedTreeType)
        {
            DirectoryUser loggedUser = Thread.CurrentPrincipal as DirectoryUser;
            if (loggedUser == null)
                throw new Exception("Logged user is not valid.");
            Audit newAudit = new Audit()
            {
                CertificateSn = loggedUser.ClientCertificate.GetSerialNumberString(),
                DataAfter = after,
                DataBefore = before,
                OperationType = operation,
                RelatedTreeId = relatedTreeId,
                RelatedTreeType = relatedTreeType,
                SignedData = signedData,
                UserId = loggedUser.UserId,
                CreationDate = DateTime.Now,
                AppVersion = 2
            };
            this.auditRepository.Create(newAudit);
        }

        public string GetJson<T>(T instance)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    ProcessDictionaryKeys = false
                }
            };
            JObject json = JObject.FromObject(instance, new JsonSerializer
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            });
            return json.ToString();
        }

    }

}
