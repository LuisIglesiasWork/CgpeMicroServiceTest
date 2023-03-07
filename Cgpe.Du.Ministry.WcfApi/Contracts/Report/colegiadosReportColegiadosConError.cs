using System.Xml.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    public class colegiadosReportColegiadosConError
    {

        [XmlElementAttribute("colegiadoConError")]
        public colegiadosReportColegiadosConErrorColegiadoConError[] colegiadoConError { get; set; }

    }

}