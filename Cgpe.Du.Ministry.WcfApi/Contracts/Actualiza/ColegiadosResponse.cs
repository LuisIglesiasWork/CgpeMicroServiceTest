using System;
using System.Net.Security;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    [MessageContract(WrapperNamespace = "http://mju.ntj.ecd.profesionales", WrapperName = "colegiadosResponse", ProtectionLevel=ProtectionLevel.Sign)]
    public class ColegiadosResponse
    {

        [MessageBodyMember(Order = 1, ProtectionLevel = ProtectionLevel.Sign)]
        public string codigoRetorno { get; set; }
        [MessageBodyMember(Order = 2, ProtectionLevel = ProtectionLevel.Sign)]
        public string descripcionRetorno { get; set; }
        [MessageBodyMember(Order = 3)]
        public string origenRespuesta { get; set; }
        [MessageBodyMember(Order = 4)]
        public string numeroPeticion { get; set; }
        [MessageBodyMember(Order = 5)]
        [XmlElement(DataType = "date")]
        public DateTime fechaDesde { get; set; }
        [MessageBodyMember(Order = 6)]
        public string codigoColegio { get; set; }
        [MessageBodyMember(Order = 7)]
        public string pagina { get; set; }
        [MessageBodyMember(Order = 8)]
        public string totalPaginas { get; set; }
        [MessageBodyMember(Order = 9)]
        public colegiadosResponseColegiados colegiados { get; set; }

    }

}