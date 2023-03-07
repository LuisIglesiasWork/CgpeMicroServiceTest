using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class Languages
    {
        // Los idiomas y sus códigos están sacados del estándar ISO 639-1. No están todos. El responsable funcional del CGPE ha hecho una selección de los 
        // que considera necesarios.
        public static readonly KeyValuePair<Guid, string> Aleman = new KeyValuePair<Guid, string>(new Guid("3847c02a-68c8-4778-a6c9-60711345798a"), "de");
        public static readonly KeyValuePair<Guid, string> Castellano = new KeyValuePair<Guid, string>(new Guid("9c326284-7ada-44b5-8084-ea96cd6cc101"), "es");
        public static readonly KeyValuePair<Guid, string> Chino = new KeyValuePair<Guid, string>(new Guid("70fb02bd-0fae-45d4-90f0-63f7b140b3aa"), "zh");
        public static readonly KeyValuePair<Guid, string> Euskera = new KeyValuePair<Guid, string>(new Guid("f82648d8-79ad-4b79-be65-ec67934a4329"), "eu");
        public static readonly KeyValuePair<Guid, string> Frances = new KeyValuePair<Guid, string>(new Guid("fb5d732b-7c8b-4c64-bb2b-5c8a53dd3b78"), "fr");
        public static readonly KeyValuePair<Guid, string> Gallego = new KeyValuePair<Guid, string>(new Guid("cc54dc34-5381-4a85-af10-7d6a35a83d5f"), "gl");
        public static readonly KeyValuePair<Guid, string> GriegoModerno = new KeyValuePair<Guid, string>(new Guid("3782672b-e1a6-433a-88d1-af46bf7fa020"), "el");
        public static readonly KeyValuePair<Guid, string> Holandes = new KeyValuePair<Guid, string>(new Guid("8626dc9c-bf6c-4aa1-bd91-07282311ed9f"), "nl");
        public static readonly KeyValuePair<Guid, string> Ingles = new KeyValuePair<Guid, string>(new Guid("c7036751-1b06-4d2f-b1d1-2ef114cd4a65"), "en");
        public static readonly KeyValuePair<Guid, string> Italiano = new KeyValuePair<Guid, string>(new Guid("9effec3f-5e4a-4397-9462-86d00f3e58d2"), "it");
        public static readonly KeyValuePair<Guid, string> Japones = new KeyValuePair<Guid, string>(new Guid("bc4c2ed3-1fae-470b-ab85-1df9653b222c"), "ja");
        public static readonly KeyValuePair<Guid, string> Latin = new KeyValuePair<Guid, string>(new Guid("f47c6a06-cb11-4cb0-8df3-3aa652150115"), "la");
        public static readonly KeyValuePair<Guid, string> Noruego = new KeyValuePair<Guid, string>(new Guid("335a3aaf-06de-4630-af0b-1a0a556a86c4"), "no");
        public static readonly KeyValuePair<Guid, string> Polaco = new KeyValuePair<Guid, string>(new Guid("c2d362bf-6043-41a0-b475-2481b5457e6e"), "pl");
        public static readonly KeyValuePair<Guid, string> Portugues = new KeyValuePair<Guid, string>(new Guid("6a191e80-f2e7-4176-bb6d-0113c29388a9"), "pt");
        public static readonly KeyValuePair<Guid, string> Rumano = new KeyValuePair<Guid, string>(new Guid("80c9bfa9-37c3-4617-9306-1d4b3027da20"), "ro");
        public static readonly KeyValuePair<Guid, string> Ruso = new KeyValuePair<Guid, string>(new Guid("04b6d10a-beab-49f0-857e-3048e8deaa7a"), "ru");
        public static readonly KeyValuePair<Guid, string> Sueco = new KeyValuePair<Guid, string>(new Guid("8d9da057-9cbb-4899-b3f1-fd9259ae9d85"), "sv");
        public static readonly KeyValuePair<Guid, string> Turco = new KeyValuePair<Guid, string>(new Guid("d5a00607-93dd-492b-af33-9605a3559b55"), "tr");
        // El valenciano no forma parte del estándar ISO 639-1 pues, desde un punto de vista meramente lingüístico, el catalán, el valenciano y el balear
        // son la misma lengua. Se incluye aquí, con un código inventado ("va"), por razones políticas. También por razones políticas se va a usar
        // "castellano" en lugar de "español".
        public static readonly KeyValuePair<Guid, string> Valenciano = new KeyValuePair<Guid, string>(new Guid("ffa4f532-8121-433c-994c-b3809085fbf0"), "va");

        // Se nos ha requerido que también añadamos balear como opción
        public static readonly KeyValuePair<Guid, string> Catalan = new KeyValuePair<Guid, string>(new Guid("ae0cb520-ba60-4cec-a359-b190f098c874"), "ca");
        public static readonly KeyValuePair<Guid, string> Balear = new KeyValuePair<Guid, string>(new Guid("12e4bdc1-dd38-4050-8369-9c92918efe78"), "ba");

    }

}
