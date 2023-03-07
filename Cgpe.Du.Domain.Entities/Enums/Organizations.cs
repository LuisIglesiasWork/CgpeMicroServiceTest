using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class Organizations
    {
        // Consejo General

        public static readonly KeyValuePair<Guid, string> CGPE = new KeyValuePair<Guid, string>(new Guid("a4e77599-3672-491e-a933-6233bf50f8e3"), "1");


        // Colegios

        public static readonly KeyValuePair<Guid, string> ColegioAlava = new KeyValuePair<Guid, string>(new Guid("9170f65a-7245-4278-99c8-0eaaf9128132"), "21");
        public static readonly KeyValuePair<Guid, string> ColegioGranada = new KeyValuePair<Guid, string>(new Guid("3aea28be-32a0-4c1a-bd62-f5f58a1397be"), "46");
        public static readonly KeyValuePair<Guid, string> ColegioGuadalajara = new KeyValuePair<Guid, string>(new Guid("0016d46c-81c2-496d-9427-c01f3d11eac1"), "47");
        public static readonly KeyValuePair<Guid, string> ColegioGuipuzcoa = new KeyValuePair<Guid, string>(new Guid("75682bb7-b2f7-45d4-aa35-579932965a06"), "48");

        public static readonly KeyValuePair<Guid, string> ColegioHuelva = new KeyValuePair<Guid, string>(new Guid("61730748-8536-481b-a4fb-71c5dd73374c"), "49");
        public static readonly KeyValuePair<Guid, string> ColegioHuesca = new KeyValuePair<Guid, string>(new Guid("79e48a9e-0fd4-4831-b5b1-b3c25c3484ea"), "50");
        public static readonly KeyValuePair<Guid, string> ColegioJaen = new KeyValuePair<Guid, string>(new Guid("ceb3adc9-d5ae-4952-b619-c00b77b5424a"), "51");
        public static readonly KeyValuePair<Guid, string> ColegioLeon = new KeyValuePair<Guid, string>(new Guid("ad44f11e-c115-4c62-8919-61b0d5069c7f"), "52");

        public static readonly KeyValuePair<Guid, string> ColegioLerida = new KeyValuePair<Guid, string>(new Guid("8cdf1d71-6230-4f4a-b642-58e0e5b47010"), "53");
        public static readonly KeyValuePair<Guid, string> ColegioLaRioja = new KeyValuePair<Guid, string>(new Guid("c954773a-997d-49b1-924a-3159c86badca"), "54");
        public static readonly KeyValuePair<Guid, string> ColegioLugo = new KeyValuePair<Guid, string>(new Guid("9403d675-655f-48bb-ae79-37dfe4c58514"), "55");
        public static readonly KeyValuePair<Guid, string> ColegioMadrid = new KeyValuePair<Guid, string>(new Guid("0b5e364f-0c24-4672-b34c-5d70295665c1"), "56");

        public static readonly KeyValuePair<Guid, string> ColegioMalaga = new KeyValuePair<Guid, string>(new Guid("8cbf95ba-c9f5-4b33-b125-6905fe475cf0"), "57");
        public static readonly KeyValuePair<Guid, string> ColegioAntequera = new KeyValuePair<Guid, string>(new Guid("3dfcef71-d781-4fcd-8685-3a92e5670648"), "58");
        public static readonly KeyValuePair<Guid, string> ColegioMelilla = new KeyValuePair<Guid, string>(new Guid("03f9f763-7cd3-405b-981a-c54282a75f45"), "59");
        public static readonly KeyValuePair<Guid, string> ColegioMurcia = new KeyValuePair<Guid, string>(new Guid("40249e4c-7d3c-476f-a807-1700cbf8df35"), "60");

        public static readonly KeyValuePair<Guid, string> ColegioCartagena = new KeyValuePair<Guid, string>(new Guid("ead24ed4-add4-4f7b-ad45-01b6921eed81"), "61");
        public static readonly KeyValuePair<Guid, string> ColegioYecla = new KeyValuePair<Guid, string>(new Guid("852cf79f-5085-44ca-a734-dfd586ac0a77"), "62");
        public static readonly KeyValuePair<Guid, string> ColegioLorca = new KeyValuePair<Guid, string>(new Guid("111361c2-c468-4825-82a3-9370128548a4"), "63");
        public static readonly KeyValuePair<Guid, string> ColegioNavarra = new KeyValuePair<Guid, string>(new Guid("5818b33a-eed6-474e-b010-ea3501237b37"), "64");

        public static readonly KeyValuePair<Guid, string> ColegioOrense = new KeyValuePair<Guid, string>(new Guid("baa4af1a-47f4-4484-ba44-806b41f69c75"), "65");
        public static readonly KeyValuePair<Guid, string> ColegioOviedo = new KeyValuePair<Guid, string>(new Guid("f5178ce2-1055-4b3b-a72a-fdb21003a711"), "66");
        public static readonly KeyValuePair<Guid, string> ColegioGijon = new KeyValuePair<Guid, string>(new Guid("328be330-ac27-45eb-a83e-83412ed4581c"), "67");
        public static readonly KeyValuePair<Guid, string> ColegioPalencia = new KeyValuePair<Guid, string>(new Guid("1523c1ca-6b62-470f-a21c-3d31a506f4a8"), "68");

        public static readonly KeyValuePair<Guid, string> ColegioLasPalmas = new KeyValuePair<Guid, string>(new Guid("214b9ce4-d78f-4577-9c07-839a3eb5f468"), "69");
        public static readonly KeyValuePair<Guid, string> ColegioPontevedra = new KeyValuePair<Guid, string>(new Guid("e7780c0f-ea62-4aa0-98d0-2382d5e4906b"), "70");
        public static readonly KeyValuePair<Guid, string> ColegioVigo = new KeyValuePair<Guid, string>(new Guid("bd7ed436-1668-453e-a3d1-4951da3b747f"), "71");
        public static readonly KeyValuePair<Guid, string> ColegioSalamanca = new KeyValuePair<Guid, string>(new Guid("920b06b9-0517-4f55-bc35-c01cf33f2b8f"), "72");

        public static readonly KeyValuePair<Guid, string> ColegioTenerife = new KeyValuePair<Guid, string>(new Guid("2a478231-5d20-4761-9e0a-6436cf19a0c9"), "73");
        public static readonly KeyValuePair<Guid, string> ColegioCantabria = new KeyValuePair<Guid, string>(new Guid("15d8b2e5-018e-4441-8e92-3f6b29fc9cc5"), "74");
        public static readonly KeyValuePair<Guid, string> ColegioSegovia = new KeyValuePair<Guid, string>(new Guid("9461248a-1782-4f1e-a6f0-01f1dcfe1062"), "75");
        public static readonly KeyValuePair<Guid, string> ColegioSevilla = new KeyValuePair<Guid, string>(new Guid("57a3a4eb-23a1-4e8e-81fb-b0b7e87fd6bd"), "76");

        public static readonly KeyValuePair<Guid, string> ColegioSoria = new KeyValuePair<Guid, string>(new Guid("536e367f-6017-46bc-bda9-801064436bf3"), "77");
        public static readonly KeyValuePair<Guid, string> ColegioTarragona = new KeyValuePair<Guid, string>(new Guid("5304de28-72be-4dc2-b8c8-2050531da2d4"), "78");
        public static readonly KeyValuePair<Guid, string> ColegioReus = new KeyValuePair<Guid, string>(new Guid("9e2b58c1-17a1-47e9-bb66-db294ac7dd25"), "79");
        public static readonly KeyValuePair<Guid, string> ColegioTortosa = new KeyValuePair<Guid, string>(new Guid("6460b32d-641a-4f6a-83b2-f24b6740d5f7"), "80");

        public static readonly KeyValuePair<Guid, string> ColegioTeruel = new KeyValuePair<Guid, string>(new Guid("4c1319c2-0c8e-406a-b02f-a52ee1fe5433"), "81");
        public static readonly KeyValuePair<Guid, string> ColegioToledo = new KeyValuePair<Guid, string>(new Guid("1a852914-5f53-40aa-bf71-77c437f4831c"), "82");
        public static readonly KeyValuePair<Guid, string> ColegioValencia = new KeyValuePair<Guid, string>(new Guid("a534a05b-a00f-41b4-a631-1fcae3bd1ea6"), "83");
        public static readonly KeyValuePair<Guid, string> ColegioValladolid = new KeyValuePair<Guid, string>(new Guid("3fe72bba-60fc-4054-a557-09e3b5ff6f0d"), "84");

        public static readonly KeyValuePair<Guid, string> ColegioVizcaya = new KeyValuePair<Guid, string>(new Guid("3225d272-a809-4438-a5a5-03f2085e8a39"), "85");
        public static readonly KeyValuePair<Guid, string> ColegioZamora = new KeyValuePair<Guid, string>(new Guid("ab4f5748-6b85-4688-9aff-a2f8c844fad6"), "86");
        public static readonly KeyValuePair<Guid, string> ColegioZaragoza = new KeyValuePair<Guid, string>(new Guid("3ba91a1b-58a9-4a7e-a9a4-a917fb9bcd20"), "87");
        public static readonly KeyValuePair<Guid, string> ColegioAlbacete = new KeyValuePair<Guid, string>(new Guid("7f819474-99c7-435e-b6ce-2c0d652c3393"), "22");

        public static readonly KeyValuePair<Guid, string> ColegioAlicante = new KeyValuePair<Guid, string>(new Guid("453f6352-8cf2-4589-9a22-abfe851eb996"), "23");
        public static readonly KeyValuePair<Guid, string> ColegioElche = new KeyValuePair<Guid, string>(new Guid("80376cff-ce6e-412a-9ca2-01bf6c3a2a61"), "24");
        public static readonly KeyValuePair<Guid, string> ColegioAlmeria = new KeyValuePair<Guid, string>(new Guid("7dc15ca0-541e-4ab4-8883-87d04c95903c"), "25");
        public static readonly KeyValuePair<Guid, string> ColegioAvila = new KeyValuePair<Guid, string>(new Guid("4e96305b-8b9f-4edf-95e9-7cf48cf30f1e"), "26");

        public static readonly KeyValuePair<Guid, string> ColegioBadajoz = new KeyValuePair<Guid, string>(new Guid("6cf65475-f2c8-4a37-8cc4-416bfba3fa17"), "27");
        public static readonly KeyValuePair<Guid, string> ColegioBaleares = new KeyValuePair<Guid, string>(new Guid("6a6ca6a8-9bd7-4444-9493-d0ce890ff175"), "28");
        public static readonly KeyValuePair<Guid, string> ColegioBarcelona = new KeyValuePair<Guid, string>(new Guid("589f59e5-a3b9-4a3a-8d34-9a21c4501974"), "29");
        public static readonly KeyValuePair<Guid, string> ColegioManresa = new KeyValuePair<Guid, string>(new Guid("5b653786-30d1-4be4-902a-d80f66f8a23a"), "30");

        public static readonly KeyValuePair<Guid, string> ColegioMataro = new KeyValuePair<Guid, string>(new Guid("ed51bc5a-c32a-40cc-b302-905aaef8d37d"), "31");
        public static readonly KeyValuePair<Guid, string> ColegioTerrassa = new KeyValuePair<Guid, string>(new Guid("a5c7b957-21e0-45ce-86ee-d51e686bfecc"), "32");
        public static readonly KeyValuePair<Guid, string> ColegioBurgos = new KeyValuePair<Guid, string>(new Guid("4579bb19-c894-488a-9e86-3260635edf7a"), "33");
        public static readonly KeyValuePair<Guid, string> ColegioCaceres = new KeyValuePair<Guid, string>(new Guid("791163e7-2835-4935-92e7-299cde495822"), "34");

        public static readonly KeyValuePair<Guid, string> ColegioCadiz = new KeyValuePair<Guid, string>(new Guid("8bacf5ee-5bd9-461a-be22-14eb2e634a16"), "35");
        public static readonly KeyValuePair<Guid, string> ColegioJerez = new KeyValuePair<Guid, string>(new Guid("818b24f2-d436-4faa-8bea-030c5d2fd215"), "36");
        public static readonly KeyValuePair<Guid, string> ColegioCeuta = new KeyValuePair<Guid, string>(new Guid("5cf68fb3-bebc-4bea-9220-d75daa658f31"), "37");
        public static readonly KeyValuePair<Guid, string> ColegioCastellon = new KeyValuePair<Guid, string>(new Guid("4d8c5e7b-d4f1-4722-92c7-f365ddad6cb2"), "38");

        public static readonly KeyValuePair<Guid, string> ColegioCiudadReal = new KeyValuePair<Guid, string>(new Guid("8b240235-0556-4909-9ba9-50ff551a6444"), "39");
        public static readonly KeyValuePair<Guid, string> ColegioValdepenas = new KeyValuePair<Guid, string>(new Guid("06ec1b07-80fd-41fe-bab7-74bd02a6659b"), "40");
        public static readonly KeyValuePair<Guid, string> ColegioCordoba = new KeyValuePair<Guid, string>(new Guid("02e52907-0b24-437b-8300-43b11747569a"), "41");
        public static readonly KeyValuePair<Guid, string> ColegioCoruna = new KeyValuePair<Guid, string>(new Guid("8b60a04b-fa15-4096-863c-3538dd2d5ab4"), "42");

        public static readonly KeyValuePair<Guid, string> ColegioSantiago = new KeyValuePair<Guid, string>(new Guid("d2c3dcb2-e360-4896-b2a6-510533fc759a"), "43");
        public static readonly KeyValuePair<Guid, string> ColegioCuenca = new KeyValuePair<Guid, string>(new Guid("1ac31ffe-a7c4-4890-8aa8-c55e67fd3f8b"), "44");
        public static readonly KeyValuePair<Guid, string> ColegioGerona = new KeyValuePair<Guid, string>(new Guid("bdc3eda4-dbfc-44f1-bf91-5c98dad01471"), "45");


        // Consejos regionales

        public static readonly KeyValuePair<Guid, string> ConsellCatalunya = new KeyValuePair<Guid, string>(new Guid("f7ce7396-f6d5-412f-b9b7-4e9a94f47683"), "24328");
        public static readonly KeyValuePair<Guid, string> ConsejoValenciano = new KeyValuePair<Guid, string>(new Guid("7eb68bf6-fa5d-4bfb-9312-c4b5bdb4cda5"), "24329");
        public static readonly KeyValuePair<Guid, string> ConsejoAndaluz = new KeyValuePair<Guid, string>(new Guid("3ad291b5-8b19-4949-b9c9-8a8fb015c65e"), "24330");
        public static readonly KeyValuePair<Guid, string> ConselloGalego = new KeyValuePair<Guid, string>(new Guid("1913ba41-cb42-4eaf-8b6b-243a15e5af52"), "24331");
        public static readonly KeyValuePair<Guid, string> ConsejoCanario = new KeyValuePair<Guid, string>(new Guid("6cc674ff-3600-44a2-98ae-55fa445dd0b6"), "33182");
        public static readonly KeyValuePair<Guid, string> ConsejoCastillaLeon = new KeyValuePair<Guid, string>(new Guid("ba01e072-f9b9-4b51-9782-6f8fa7eabe3b"), "33649");
        public static readonly KeyValuePair<Guid, string> ConsejoCastillaLaMancha = new KeyValuePair<Guid, string>(new Guid("a41d790d-0e17-433f-b920-41c0fef73c75"), "33650");
        public static readonly KeyValuePair<Guid, string> ConsejoPaisVasco = new KeyValuePair<Guid, string>(new Guid("10b0caab-fa6a-43ad-826c-e85ed486767a"), "34037");
        public static readonly KeyValuePair<Guid, string> ConsejoAragon = new KeyValuePair<Guid, string>(new Guid("34fb85ea-2aa7-47bd-bc7d-7a7fd9768486"), "34403");

    }

}
