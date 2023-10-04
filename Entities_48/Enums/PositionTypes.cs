using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class PositionTypes
    {
        public static readonly KeyValuePair<string, string> Presidente = new KeyValuePair<string, string>(new Guid("eae86921-aa70-4974-870c-b761479645e4").ToString(), "33");
        public static readonly KeyValuePair<string, string> Vicepresidente = new KeyValuePair<string, string>(new Guid("c959ada8-f707-45fe-85ad-0bd8d7a4e973").ToString(), "23");
        public static readonly KeyValuePair<string, string> Decano = new KeyValuePair<string, string>(new Guid("81556eb6-212d-401b-b735-b1f490fa2951").ToString(), "1");
        public static readonly KeyValuePair<string, string> DecanoEnFunciones = new KeyValuePair<string, string>(new Guid("2c7ab4ac-1867-4dd3-ae94-8a17d927520b").ToString(), "38");
        public static readonly KeyValuePair<string, string> Vicedecano = new KeyValuePair<string, string>(new Guid("28211bab-e104-4395-acd3-a37d709807e4").ToString(), "2");
        public static readonly KeyValuePair<string, string> Secretario = new KeyValuePair<string, string>(new Guid("9c9825ec-b79b-4414-bac4-e9960d4a65a1").ToString(), "3");
        public static readonly KeyValuePair<string, string> Vicesecretario = new KeyValuePair<string, string>(new Guid("89379e28-41b4-4c8a-8c93-c9e382ec295a").ToString(), "5");
        public static readonly KeyValuePair<string, string> DecanoHonorario = new KeyValuePair<string, string>(new Guid("78c47275-c129-428b-86be-a6083d97c72d").ToString(), "27");
        public static readonly KeyValuePair<string, string> InspectorGeneral = new KeyValuePair<string, string>(new Guid("c9d8fcc4-a7ce-495e-af1f-c945db9d3bc6").ToString(), "34");
        public static readonly KeyValuePair<string, string> SubinspectorGeneral = new KeyValuePair<string, string>(new Guid("55d5d236-d29d-4398-8231-b8b05471b2ff").ToString(), "35");
        public static readonly KeyValuePair<string, string> Tesorero = new KeyValuePair<string, string>(new Guid("20e53a64-962f-4faa-9b08-6904e33d2ed3").ToString(), "4");
        public static readonly KeyValuePair<string, string> Vicetesorero = new KeyValuePair<string, string>(new Guid("72228c34-9bb8-45a6-b01c-67fcd2feb65f").ToString(), "6");
        public static readonly KeyValuePair<string, string> Contador = new KeyValuePair<string, string>(new Guid("0ff2e486-1c24-4029-a3a3-d537152ec8aa").ToString(), "7");
        public static readonly KeyValuePair<string, string> ContadorBibliotecario = new KeyValuePair<string, string>(new Guid("027f8fa1-739f-4086-851d-0ff5084b9402").ToString(), "51");
        public static readonly KeyValuePair<string, string> Interventor = new KeyValuePair<string, string>(new Guid("c76199dd-3371-4fdc-a9aa-7a043e0fd532").ToString(), "26");
        public static readonly KeyValuePair<string, string> Consejero = new KeyValuePair<string, string>(new Guid("a58a0cad-4f18-4972-b925-3a951c273512").ToString(), "29");
        public static readonly KeyValuePair<string, string> Coordinador = new KeyValuePair<string, string>(new Guid("35243f1b-b159-4bd4-9c7b-c7dd0bd1b150").ToString(), "65");
        public static readonly KeyValuePair<string, string> Miembro = new KeyValuePair<string, string>(new Guid("f4dd820e-a571-469b-86b8-dfff94095282").ToString(), "32");
        public static readonly KeyValuePair<string, string> Viceinterventor = new KeyValuePair<string, string>(new Guid("e1f94c84-b312-4bde-b824-a128a14efbcc").ToString(), "24");
        public static readonly KeyValuePair<string, string> Censor = new KeyValuePair<string, string>(new Guid("2178e18c-8e8f-4862-9c55-edd1e3a238a4").ToString(), "30");
        public static readonly KeyValuePair<string, string> BibliotecarioArchivero = new KeyValuePair<string, string>(new Guid("f41d1a19-e8c4-4022-893a-fc95558c372f").ToString(), "31");
        public static readonly KeyValuePair<string, string> Vocal1o = new KeyValuePair<string, string>(new Guid("47bea514-d076-41c0-b0c0-9f46d84194a0").ToString(), "8");
        public static readonly KeyValuePair<string, string> Vocal2o = new KeyValuePair<string, string>(new Guid("94dbae0e-357a-4fca-95d2-9afba6603c4c").ToString(), "9");
        public static readonly KeyValuePair<string, string> Vocal3o = new KeyValuePair<string, string>(new Guid("949c3d9e-7fc0-4e5a-9d5c-0ad0cbc8446b").ToString(), "10");
        public static readonly KeyValuePair<string, string> Vocal4o = new KeyValuePair<string, string>(new Guid("d9efd623-1f46-4524-b93b-e27789ec36da").ToString(), "11");
        public static readonly KeyValuePair<string, string> Vocal5o = new KeyValuePair<string, string>(new Guid("a2f1f22a-6876-4d31-8645-c643c1354f2b").ToString(), "12");
        public static readonly KeyValuePair<string, string> Vocal6o = new KeyValuePair<string, string>(new Guid("3044915f-b2b2-4157-b1c0-03f5b6096bbd").ToString(), "13");
        public static readonly KeyValuePair<string, string> Vocal7o = new KeyValuePair<string, string>(new Guid("b3589f2f-f7ba-4585-be60-fc6dde4a0ffc").ToString(), "14");
        public static readonly KeyValuePair<string, string> Vocal8o = new KeyValuePair<string, string>(new Guid("2c5004aa-facd-4394-9966-40a129fd1a3c").ToString(), "15");
        public static readonly KeyValuePair<string, string> VocalHonorario = new KeyValuePair<string, string>(new Guid("ab415b11-496d-4c9b-84f9-6bda8fcb2266").ToString(), "17");
        public static readonly KeyValuePair<string, string> Vocal = new KeyValuePair<string, string>(new Guid("82a05831-bd28-4354-9f08-c217d036132d").ToString(), "16");
        public static readonly KeyValuePair<string, string> VocalNato = new KeyValuePair<string, string>(new Guid("d69d96a2-7162-4326-be48-5fa1ef008e4e").ToString(), "22");
        public static readonly KeyValuePair<string, string> ConsejeroHonorario = new KeyValuePair<string, string>(new Guid("92923146-70f2-4dab-90d5-2bd9ef0d6989").ToString(), "28");
        public static readonly KeyValuePair<string, string> ProcuradorHonorario = new KeyValuePair<string, string>(new Guid("199c0f7d-e28a-4d05-9f79-bd26ae1be72b").ToString(), "25");
        public static readonly KeyValuePair<string, string> RepresentanteSocioProtector = new KeyValuePair<string, string>(new Guid("b23dfee4-f92c-4ac4-a505-b3f52ef988ff").ToString(), "36");
        public static readonly KeyValuePair<string, string> DelegadoDeAsuntosExteriores = new KeyValuePair<string, string>(new Guid("ad16775f-0cca-4a85-ad03-25e1463b63cd").ToString(), "50");
        public static readonly KeyValuePair<string, string> Diputado1 = new KeyValuePair<string, string>(new Guid("cfdc135c-984a-48b8-a6cb-c44680c37564").ToString(), "52");
        public static readonly KeyValuePair<string, string> Diputado2 = new KeyValuePair<string, string>(new Guid("c700247a-7f81-4ef3-822a-1a07d2121f69").ToString(), "53");
        public static readonly KeyValuePair<string, string> Diputado3 = new KeyValuePair<string, string>(new Guid("a87ebd1d-92b5-4af5-a0dd-8002b80cdfa5").ToString(), "54");
        public static readonly KeyValuePair<string, string> Diputado4 = new KeyValuePair<string, string>(new Guid("7d91820f-6636-4f86-ae3e-e9ce2353c01e").ToString(), "55");
        public static readonly KeyValuePair<string, string> Diputado5 = new KeyValuePair<string, string>(new Guid("156c8e12-0de4-4538-bfb9-ea80de99b903").ToString(), "56");
        public static readonly KeyValuePair<string, string> Diputado6 = new KeyValuePair<string, string>(new Guid("e695a838-ef77-4e4b-a454-bfc859ec2d5b").ToString(), "57");
        public static readonly KeyValuePair<string, string> Diputado7 = new KeyValuePair<string, string>(new Guid("5b8448af-41c3-4607-82c5-009e777ef0ff").ToString(), "58");
        public static readonly KeyValuePair<string, string> Diputado8 = new KeyValuePair<string, string>(new Guid("c38f400d-31db-4b6c-a5d6-280f857c6b2d").ToString(), "59");
        public static readonly KeyValuePair<string, string> Diputado9 = new KeyValuePair<string, string>(new Guid("86d4adff-54f6-4d34-afc8-da54b016abca").ToString(), "60");
        public static readonly KeyValuePair<string, string> SupervisorVocalEntidadProtectora = new KeyValuePair<string, string>(new Guid("9bb14761-ad2c-473d-98c9-85ad9c716927").ToString(), "61");
        public static readonly KeyValuePair<string, string> VocalComisionEjecutiva = new KeyValuePair<string, string>(new Guid("1a0c82ef-644a-4d15-ab83-6525cf999c18").ToString(), "62");
        public static readonly KeyValuePair<string, string> VocalComisionInversiones = new KeyValuePair<string, string>(new Guid("629217b6-38d5-411a-b9a0-5ef0c1ee3779").ToString(), "63");
        public static readonly KeyValuePair<string, string> VocalEntProtectoraComisionInversiones = new KeyValuePair<string, string>(new Guid("86899096-494e-40ff-92d5-83b688bf5348").ToString(), "64");
        public static readonly KeyValuePair<string, string> DecanoAdjuntoALaPresidencia = new KeyValuePair<string, string>(new Guid("f06aee55-4ba5-41ec-ae85-6fda6af818f0").ToString(), "66");
        public static readonly KeyValuePair<string, string> SecretarioEnFunciones = new KeyValuePair<string, string>(new Guid("d7d4b6ee-00a3-4b0a-83bc-236ebe6f574c").ToString(), "67");
        public static readonly KeyValuePair<string, string> TesoreroEnFunciones = new KeyValuePair<string, string>(new Guid("1f3d0aec-1b00-4b53-998d-afc48450a1da").ToString(), "68");
        public static readonly KeyValuePair<string, string> VicesecretarioEnFunciones = new KeyValuePair<string, string>(new Guid("5bbf79e5-1ef1-4e53-9e2e-ee8b2b8f09e9").ToString(), "69");
        public static readonly KeyValuePair<string, string> VicetesoreroEnFunciones = new KeyValuePair<string, string>(new Guid("a1e5c0ad-aab9-481c-a9ba-8e12005bed42").ToString(), "70");
        public static readonly KeyValuePair<string, string> PresidenteDeHonor = new KeyValuePair<string, string>(new Guid("c6c299b1-6743-407a-9b11-171d3250017c").ToString(), "71");
        // En el original del censo hay un "decano en funciones" repetido, con código 72. Habrá que tenerlo en cuenta, si algún día se migran datos desde el censo: 
        // habrá que redirigir los 72 al 38.
        public static readonly KeyValuePair<string, string> ConsejeroEnFunciones = new KeyValuePair<string, string>(new Guid("454c00f7-8ef9-4515-838b-e89be379b8ea").ToString(), "73");

    }

}
