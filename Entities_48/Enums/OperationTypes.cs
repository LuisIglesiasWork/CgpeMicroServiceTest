using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class OperationTypes
    {
        public static readonly string Add = new Guid("{5355A05C-5900-43B4-896C-00512D4A0BE9}").ToString();
        public static readonly string Update = new Guid("{10AB8BEC-3668-4F1D-B824-00A42D1D87FE}").ToString();
        public static readonly string Delete = new Guid("{A0277D42-891C-4133-9115-01271E62F501}").ToString();
        public static readonly string Query = new Guid("{2588CBAF-3309-49E2-AA70-0637724C9918}").ToString();
        public static readonly string MinistrySyncError = new Guid("{107426ee-df00-4f2a-a704-32e6ac130933}").ToString();

        public static readonly string PrecreateProcurator = new Guid("{E6357847-3478-4E2B-8BE3-2A305D708E36}").ToString();
        public static readonly string PrecreateAssociationProcurator = new Guid("{5AF1C949-5CF2-44E6-AE5C-9F0424D4D1FF}").ToString();

        public static readonly string RejectProcurator = new Guid("{7D45823A-05CD-4057-B6A9-E663CB72CBB8}").ToString();
        public static readonly string RejectAssociationProcurator = new Guid("{F92E2414-5285-4509-9BA0-55B74028FD65}").ToString();

        public static readonly string FixProcurator = new Guid("{F22FC169-40CA-4446-8480-6E4CCA7AED54}").ToString();
        public static readonly string FixAssociationProcurator = new Guid("{8AABF7D8-4309-43F0-A38A-73D69D2A2505}").ToString();
        public static readonly string FixSituationHistory = new Guid("{E311811B-F413-415F-837B-A5D0D12E34E5}").ToString();
		public static readonly string FixContactHistory = new Guid("{96E04E29-9E23-41EA-8EA6-8928DDBC5639}").ToString();

        public static readonly string AcceptProcurator = new Guid("{D2C07015-8EDE-46FD-8670-F94799A19B15}").ToString();
        public static readonly string AcceptAssociationProcurator = new Guid("{7A4FE985-9DAE-4C51-B9F3-B4167F4B2BE7}").ToString();





        //public static readonly string CensoSyncError = new Guid("{dd9457d1-e218-4ffc-9c1b-8f60ba4b6c96}").ToString();
    }

}
