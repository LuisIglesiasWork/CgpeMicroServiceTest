using System.Xml.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    public class colegiadosResponseColegiados
    {

        [XmlElementAttribute("actualizacionColegiado")]
        public colegiadosResponseColegiadosActualizacionColegiado[] actualizacionColegiado { get; set; }

    }

}