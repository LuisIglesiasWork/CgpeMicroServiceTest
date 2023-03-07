using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    [DataContract(Name = "profesionalesFault", Namespace="http://mju.ntj.ecd.profesionales")]
    public class ProfesionalesFaultContract
    {

        [XmlElement(ElementName = "codigoError", IsNullable=false)]
        [DataMember(Name = "codigoError", IsRequired = true, Order = 1)]
        public string CodigoError { get; set; }

        [XmlElement(ElementName = "descripcionError", IsNullable = false)]
        [DataMember(Name = "descripcionError", IsRequired = false, EmitDefaultValue = false, Order = 2)]
        public string DescripcionError { get; set; }

        [XmlElement(ElementName = "TipoError", IsNullable = false)]
        [DataMember(Name = "TipoError", IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public string TipoError { get; set; }

        [XmlElement(ElementName = "causaError", IsNullable = false)]
        [DataMember(Name = "causaError", IsRequired = false, EmitDefaultValue = false, Order = 4)]
        public string CausaError { get; set; }

        [XmlElement(ElementName = "accion", IsNullable = false)]
        [DataMember(Name = "accion", IsRequired = false, EmitDefaultValue = false, Order = 5)]
        public string Accion { get; set; }

    }

}