using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class DirectoryRoleTypes
    {

        public static readonly KeyValuePair<Guid, string> Consejo = new KeyValuePair<Guid, string>(new Guid("{75464AD7-021F-4703-835E-079765D9DC87}"), "Consejo");
        public static readonly KeyValuePair<Guid, string> Colegio = new KeyValuePair<Guid, string>(new Guid("{75464AD7-021F-4703-835E-079765D9DC88}"), "Colegio");
        public static readonly KeyValuePair<Guid, string> Ministerio = new KeyValuePair<Guid, string>(new Guid("{75464AD7-021F-4703-835E-079765D9DC89}"), "Ministerio");
        public static readonly KeyValuePair<Guid, string> CopiadorDeLaBaseDeDatos = new KeyValuePair<Guid, string>(new Guid("{75464AD7-021F-4703-835E-079765D9DC90}"), "CopiadorDeLaBaseDeDatos");
        public static readonly KeyValuePair<Guid, string> Notario = new KeyValuePair<Guid, string>(new Guid("{27716c3a-5be4-4b41-b953-4a3e22b02620}"), "Notario");
        public static readonly KeyValuePair<Guid, string> CGPJ = new KeyValuePair<Guid, string>(new Guid("{b7aa5448-08ba-4a13-a164-932548f16a2e}"), "CGPJ");
        public static readonly KeyValuePair<Guid, string> Registradores = new KeyValuePair<Guid, string>(new Guid("{d9c4467b-a85b-4062-919a-246ac872fd06}"), "Registradores");
        public static readonly KeyValuePair<Guid, string> BOE = new KeyValuePair<Guid, string>(new Guid("{0b4222d1-fe68-4900-9028-fe0bc3116bab}"), "BOE");
    }

}
