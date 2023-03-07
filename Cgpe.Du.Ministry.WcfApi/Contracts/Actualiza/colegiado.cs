using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    public partial class colegiado
    {

        public tipoIdentificacion tipoIdentificacion { get; set; }

        public string numeroIdentificacion { get; set; }

        public tipoIdentificacion tipoIdentificacionOld { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool tipoIdentificacionOldSpecified
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.numeroIdentificacionOld);
            }
        }

        public string numeroIdentificacionOld { get; set; }

        public string nombre { get; set; }

        public string primerApellido { get; set; }

        public string segundoApellido { get; set; }

        public sexo sexo { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sexoSpecified
        {
            get
            {
                return true;
            }
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("telefono", IsNullable = false)]
        public string[] telefonos { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute("correoElectronico", IsNullable = false)]
        public string[] correosElectronicos { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute("fax", IsNullable = false)]
        public string[] faxes { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public direccion[] direcciones { get; set; }

        public situacionProfesional situacionProfesional { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public colegio[] colegios { get; set; }
    }

}