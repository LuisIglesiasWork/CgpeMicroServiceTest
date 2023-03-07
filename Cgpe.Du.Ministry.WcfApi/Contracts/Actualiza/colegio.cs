using System.Runtime.Serialization;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    [DataContract(Namespace = "http://mju.ntj.ecd.profesionales")]
    public class colegio
    {

        [DataMember]
        public string codigoColegio { get; set; }
        [DataMember]
        public string numeroColegiado { get; set; }
        [DataMember]
        public bool principal { get; set; }
        [DataMember]
        public bool baja { get; set; }

    }

}