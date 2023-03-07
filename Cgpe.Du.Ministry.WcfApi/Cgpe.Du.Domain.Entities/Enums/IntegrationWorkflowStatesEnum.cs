using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public enum IntegrationWorkflowStatesEnum : int
    {
        INICIADO = 0,
        PENDIENTE = 1,
        PROCESANDO = 2,
        FINALIZADO = 3,
        INFORME_RECIBIDO = 4,
        CARGA_INICIAL = 5
    }

}
