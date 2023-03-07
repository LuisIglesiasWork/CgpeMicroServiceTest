using System;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    [MessageContract(WrapperNamespace = "http://mju.ntj.ecd.profesionales", WrapperName = "colegiadosReport")]
    public class ColegiadosReport
    {

        [MessageBodyMember(Order = 1)]
        public string codigoRetorno { get; set; }
        [MessageBodyMember(Order = 2)]
        public string descripcionRetorno { get; set; }
        [MessageBodyMember(Order = 3)]
        public colegiadosReportDestinoPeticion destinoPeticion { get; set; }
        [MessageBodyMember(Order = 4)]
        public string numeroPeticion { get; set; }
        [MessageBodyMember(Order = 5)]
        [XmlElement(DataType = "date")]
        public DateTime fechaDesde { get; set; }
        [MessageBodyMember(Order = 6)]
        public string codigoColegio { get; set; }
        [MessageBodyMember(Order = 7)]
        public int pagina { get; set; }
        [MessageBodyMember(Order = 8)]
        public int totalPaginas { get; set; }
        [MessageBodyMember(Order = 9)]
        public colegiadosReportColegiadosConError colegiadosConError { get; set; }

    }

}