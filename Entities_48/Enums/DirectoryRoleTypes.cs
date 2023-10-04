using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class DirectoryRoleTypes
    {

        public static readonly KeyValuePair<string, string> Consejo = new KeyValuePair<string, string>(new Guid("{75464AD7-021F-4703-835E-079765D9DC87}").ToString(), "Consejo");
        public static readonly KeyValuePair<string, string> Colegio = new KeyValuePair<string, string>(new Guid("{75464AD7-021F-4703-835E-079765D9DC88}").ToString(), "Colegio");
        public static readonly KeyValuePair<string, string> Ministerio = new KeyValuePair<string, string>(new Guid("{75464AD7-021F-4703-835E-079765D9DC89}").ToString(), "Ministerio");
        public static readonly KeyValuePair<string, string> CopiadorDeLaBaseDeDatos = new KeyValuePair<string, string>(new Guid("{75464AD7-021F-4703-835E-079765D9DC90}").ToString(), "CopiadorDeLaBaseDeDatos");
        public static readonly KeyValuePair<string, string> Notario = new KeyValuePair<string, string>(new Guid("{27716c3a-5be4-4b41-b953-4a3e22b02620}").ToString(), "Notario");
        public static readonly KeyValuePair<string, string> CGPJ = new KeyValuePair<string, string>(new Guid("{b7aa5448-08ba-4a13-a164-932548f16a2e}").ToString(), "CGPJ");
        public static readonly KeyValuePair<string, string> Registradores = new KeyValuePair<string, string>(new Guid("{d9c4467b-a85b-4062-919a-246ac872fd06}").ToString(), "Registradores");
        public static readonly KeyValuePair<string, string> BOE = new KeyValuePair<string, string>(new Guid("{0b4222d1-fe68-4900-9028-fe0bc3116bab}").ToString(), "BOE");
    }

}
