using System.Net.Security;
using System.ServiceModel;

namespace Cgpe.Du.Ministry.WcfApi.Contracts
{

    [XmlSerializerFormat]
    [ServiceContract(Namespace = "http://mju.ntj.ecd.profesionales", Name = "profesionales", ProtectionLevel=ProtectionLevel.Sign)]
    public interface IProfesionales
    {

        [OperationContract(Action = "mju.ntj.ecd.profesionales/actualiza", ProtectionLevel=ProtectionLevel.Sign)]
        [FaultContractAttribute(typeof(ProfesionalesFaultContract))]
        ColegiadosResponse actualiza(ColegiadosRequest colegiadosRequest);

        [OperationContract(Action = "mju.ntj.ecd.profesionales/actualizaReport", ProtectionLevel = ProtectionLevel.Sign)]
        [FaultContractAttribute(typeof(ProfesionalesFaultContract))]
        ReportResponse actualizaReport(ColegiadosReport colegiadosReport);

    }

}
