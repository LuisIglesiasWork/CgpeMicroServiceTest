using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class AddressTypes
    {
        public static readonly Guid Despacho = new Guid("{C6B9D9AA-2A51-4F89-AA4A-0FC107D617DB}");
        public static readonly Guid DespachoPrincipal = new Guid("{BB61D34F-C4BB-4D6D-9B33-ABE379E4EF12}");
        public static readonly Guid Particular = new Guid("{7DC3F62D-15F8-41AA-91D8-F457F5B3D819}");
        public static readonly Guid DeAvisos = new Guid("{C80637C1-1160-4FCE-8D0E-03B87789EF51}");
        public static readonly Guid ApartadoCorreos = new Guid("{CA9D7814-DC7C-4033-B9C8-7304DB0F35FA}");
        public static readonly Guid Sede = new Guid("{222A3FF4-3C86-4331-AD9F-1812B73A91ED}");
        public static readonly Guid Otros = new Guid("{9A819A7A-F257-492F-9D37-A4BB8216DDE6}");

    }

}
