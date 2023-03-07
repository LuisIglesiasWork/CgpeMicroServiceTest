using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public enum ProcuratorSituationEnum : int
    {
        None = 0, // Esto es para cuando se dé de alta un procurador con situaciones futuras, pero no presentes. La presente será "Ninguna".
        Practising = 1, // Ejerciente
        NonPractising = 2, // No ejerciente
        UnregisteredTemporarily = 3, // Baja temporal
        RetiredClosing = 4, // No ejerciente jubilado con despacho en liquidación
        // Retired totally ("jubilado total") desaparece (era el 5)
        PassedAway = 6, // Fallecimiento
        UnregisteredForever = 7, // Baja definitiva
        Suspended = 8, // Sancionado con suspensión
        UnregisteredNotPaying = 9, // Baja por impago
        // A partir de aquí no existían en DU 1.
        Expelled = 10, // Expulsión
        NonPractisingAuthorisedPersonalAffairs = 11, // No ejerciente con habilitación para asuntos propios
        NonPractisingAuthorisedClaimWages = 12, // No ejerciente con habilitación para jura de cuentas
    }

}
