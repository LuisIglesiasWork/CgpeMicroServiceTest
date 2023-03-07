using System.Net.Security;
using System.ServiceModel;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    [MessageContract(WrapperNamespace = "http://mju.ntj.ecd.profesionales", WrapperName = "reportResponse", ProtectionLevel = ProtectionLevel.Sign)]
    public class ReportResponse
    {

        [MessageBodyMember]
        public string numeroPeticion { get; set; }

    }

}