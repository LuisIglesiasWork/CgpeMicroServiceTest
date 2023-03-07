using System;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    [MessageContract(WrapperNamespace = "http://mju.ntj.ecd.profesionales", WrapperName = "colegiadosRequest")]
    public class ColegiadosRequest
    {

        [MessageBodyMember(Order=1)]
        public string numeroPeticion { get; set; }
        [MessageBodyMember(Order = 2)]
        public colegiadosRequestDestinoPeticion destinoPeticion { get; set; }
        [MessageBodyMember(Order = 3)]
        [XmlElement(DataType = "date")]
        public DateTime fechaDesde { get; set; }
        [MessageBodyMember(Order = 4)]
        public string codigoColegio { get; set; }
        [MessageBodyMember(Order = 5)]
        public int pagina { get; set; }

    }

}