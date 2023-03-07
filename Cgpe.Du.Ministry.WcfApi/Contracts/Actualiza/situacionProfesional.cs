using System;
using System.Xml.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    public class situacionProfesional
    {

        public situacionEjercicio situacion { get; set; }
        [XmlElement(DataType = "date")]
        public DateTime fechaIncioNoEjercicio { get; set; }
        [XmlElement(DataType = "date")]
        public DateTime fechaFinNoEjercicio { get; set; }

    }

}