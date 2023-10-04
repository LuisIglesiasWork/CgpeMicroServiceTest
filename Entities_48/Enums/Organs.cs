using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class Organs
    {
        // CGPE

        public static readonly KeyValuePair<string, string> ComiteEjecutivo = new KeyValuePair<string, string>(new Guid("a96aa2c5-96f8-4fd8-b9c2-f959686f9567").ToString(), "24183");
        public static readonly KeyValuePair<string, string> ComisionPermanente = new KeyValuePair<string, string>(new Guid("39e41180-a3ed-4d9c-9e5b-b3b06f7322ac").ToString(), "24184");
        public static readonly KeyValuePair<string, string> Pleno = new KeyValuePair<string, string>(new Guid("5ecc040e-c294-4d37-8dba-8480ac24c356").ToString(), "24185");
        public static readonly KeyValuePair<string, string> ComisionRelacionesInstitucionales = new KeyValuePair<string, string>(new Guid("ea842196-f7d8-4c36-a4b5-0d5008f69721").ToString(), "24186");
        public static readonly KeyValuePair<string, string> ComisionRelacionesInternacionales = new KeyValuePair<string, string>(new Guid("943e1e79-07a0-44d6-a4a3-733b8211c515").ToString(), "24187");
        public static readonly KeyValuePair<string, string> ComisionFinanciacion = new KeyValuePair<string, string>(new Guid("ae08655c-85d5-4584-b048-72e2a5d33fba").ToString(), "24188");
        public static readonly KeyValuePair<string, string> ComisionFormacionEstatutos = new KeyValuePair<string, string>(new Guid("6da48f42-c562-413c-a658-291e3947dca9").ToString(), "24189");
        public static readonly KeyValuePair<string, string> ComisionDeontologia = new KeyValuePair<string, string>(new Guid("d5e93271-b665-43da-8230-738d8920c7d1").ToString(), "24190");
        public static readonly KeyValuePair<string, string> ComisionAranceles = new KeyValuePair<string, string>(new Guid("ef308b52-e425-4537-a14a-e1c3e08acfd4").ToString(), "24191");
        public static readonly KeyValuePair<string, string> ComisionAplicaciones = new KeyValuePair<string, string>(new Guid("7de59814-911b-4843-b3ac-b01cd6a77da2").ToString(), "24192");
        public static readonly KeyValuePair<string, string> ComisionImagen = new KeyValuePair<string, string>(new Guid("e51d39c4-4a51-4f81-981f-717aee3f9b98").ToString(), "24193");
        public static readonly KeyValuePair<string, string> ComisionCultura = new KeyValuePair<string, string>(new Guid("4c15cd3f-02c3-4bcf-8b02-d37a894cc886").ToString(), "34901");
        public static readonly KeyValuePair<string, string> ComisionEstudios = new KeyValuePair<string, string>(new Guid("c1e6fc7b-3cd9-4cd4-af79-cc3162731460").ToString(), "34902");
        public static readonly KeyValuePair<string, string> ComisionFormacion = new KeyValuePair<string, string>(new Guid("f0cca815-5652-4b17-9587-f9550ad477d0").ToString(), "34903");
        public static readonly KeyValuePair<string, string> ComisionModernizacion = new KeyValuePair<string, string>(new Guid("de55ebdb-d0bb-48b3-899f-f9e3b6816e2d").ToString(), "34904");
        public static readonly KeyValuePair<string, string> GrupoTrabajoModernizacion = new KeyValuePair<string, string>(new Guid("bce6ff46-dfdc-48aa-ab7e-655d26ef6c71").ToString(), "36292");
        public static readonly KeyValuePair<string, string> GrupoTrabajoFormacionCultura = new KeyValuePair<string, string>(new Guid("140ab8fa-a6c5-48e0-a7f6-1370292221b9").ToString(), "36293");
        public static readonly KeyValuePair<string, string> SubgrupoTrabajoFormacion = new KeyValuePair<string, string>(new Guid("7c005f6a-1fa4-4d58-b3fe-004b656dbda5").ToString(), "36294");
        public static readonly KeyValuePair<string, string> SubgrupoTrabajoCultura = new KeyValuePair<string, string>(new Guid("09bb7273-48cb-4a0e-9c92-060499adf2c6").ToString(), "36295");
        public static readonly KeyValuePair<string, string> GrupoTrabajoAreaInternacional = new KeyValuePair<string, string>(new Guid("346463c9-0d9e-47aa-b6ee-25e5f9051028").ToString(), "36296");


        // Colegios

        public static readonly KeyValuePair<string, string> JuntaGobiernoAlava = new KeyValuePair<string, string>(new Guid("b1e8ed86-5686-46f8-9dbb-cbfe89c5bfb7").ToString(), "24261");
        public static readonly KeyValuePair<string, string> JuntaGobiernoAlbacete = new KeyValuePair<string, string>(new Guid("38dd2408-d18f-4426-a1a5-56eccd5d2af2").ToString(), "24262");
        public static readonly KeyValuePair<string, string> JuntaGobiernoAlicante = new KeyValuePair<string, string>(new Guid("477be48a-bd9f-431d-b213-3335ca05b6df").ToString(), "24263");
        public static readonly KeyValuePair<string, string> JuntaGobiernoElche = new KeyValuePair<string, string>(new Guid("799e58ec-6d81-4602-bcc0-a42d2cc3dbd4").ToString(), "24264");
        public static readonly KeyValuePair<string, string> JuntaGobiernoAlmeria = new KeyValuePair<string, string>(new Guid("ec381880-24bd-4a3b-b3ef-3e826327030d").ToString(), "24265");
        public static readonly KeyValuePair<string, string> JuntaGobiernoAvila = new KeyValuePair<string, string>(new Guid("2ae8fce5-a02c-4d07-845d-28792a187635").ToString(), "24266");
        public static readonly KeyValuePair<string, string> JuntaGobiernoBadajoz = new KeyValuePair<string, string>(new Guid("af158140-7724-4be3-a350-cac8656c9886").ToString(), "24267");
        public static readonly KeyValuePair<string, string> JuntaGobiernoBaleares = new KeyValuePair<string, string>(new Guid("f7d49147-ab92-4c10-b0ac-37bac05068a1").ToString(), "24268");
        public static readonly KeyValuePair<string, string> JuntaGobiernoBarcelona = new KeyValuePair<string, string>(new Guid("29ebc98b-eb86-49b2-b0fa-ee96f7ba5eea").ToString(), "24269");
        public static readonly KeyValuePair<string, string> JuntaGobiernoManresa = new KeyValuePair<string, string>(new Guid("07526630-d66d-4fa3-9083-634c19820f4b").ToString(), "24270");
        public static readonly KeyValuePair<string, string> JuntaGobiernoMataro = new KeyValuePair<string, string>(new Guid("5457d490-b6de-4dc5-831d-19bd1c77ec9e").ToString(), "24271");
        public static readonly KeyValuePair<string, string> JuntaGobiernoTerrassa = new KeyValuePair<string, string>(new Guid("8e08b968-f71b-4038-9ba9-0868bc5162f8").ToString(), "24272");
        public static readonly KeyValuePair<string, string> JuntaGobiernoBurgos = new KeyValuePair<string, string>(new Guid("b1303024-bed4-4cf7-914c-d21e7029cc79").ToString(), "24273");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCaceres = new KeyValuePair<string, string>(new Guid("4e94dc4f-3ec9-4070-829d-6d921303c40f").ToString(), "24274");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCadiz = new KeyValuePair<string, string>(new Guid("36e14bba-2dcc-49b7-a06f-e7a409ccff7b").ToString(), "24275");
        public static readonly KeyValuePair<string, string> JuntaGobiernoJerez = new KeyValuePair<string, string>(new Guid("b12b6ad1-73bf-459b-bf58-bfeb15da715c").ToString(), "24276");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCeuta = new KeyValuePair<string, string>(new Guid("401a963c-545c-4278-baff-4ee8bc3ec8d3").ToString(), "24277");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCastellon = new KeyValuePair<string, string>(new Guid("f3bc463e-1dd7-444a-9a8f-6cb384feeb8b").ToString(), "24278");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCiudadReal = new KeyValuePair<string, string>(new Guid("0243e301-54ab-48cd-b384-e922a1d71d6b").ToString(), "24279");
        public static readonly KeyValuePair<string, string> JuntaGobiernoValdepenas = new KeyValuePair<string, string>(new Guid("e76365ec-d0ec-4508-a66f-456efc1ea0c0").ToString(), "24280");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCordoba = new KeyValuePair<string, string>(new Guid("80d63ca2-de1b-45e1-85f3-0c7d990044f6").ToString(), "24281");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCoruna = new KeyValuePair<string, string>(new Guid("867de19f-1e65-4d73-9c43-523841c65d93").ToString(), "24282");
        public static readonly KeyValuePair<string, string> JuntaGobiernoSantiago = new KeyValuePair<string, string>(new Guid("f29db68b-0616-4bff-8b36-8015631eaaf1").ToString(), "24283");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCuenca = new KeyValuePair<string, string>(new Guid("80f1ba30-2bfe-4a2b-8cf6-4035c2461326").ToString(), "24284");
        public static readonly KeyValuePair<string, string> JuntaGobiernoGerona = new KeyValuePair<string, string>(new Guid("615fd79e-bc0b-4c9b-bebf-221e345a9d0c").ToString(), "24285");
        public static readonly KeyValuePair<string, string> JuntaGobiernoGranada = new KeyValuePair<string, string>(new Guid("e03a2b71-35e4-4075-9bc3-dd5b07809746").ToString(), "24286");
        public static readonly KeyValuePair<string, string> JuntaGobiernoGuadalajara = new KeyValuePair<string, string>(new Guid("2595ebb6-bd5e-432f-af03-3980bec23122").ToString(), "24287");
        public static readonly KeyValuePair<string, string> JuntaGobiernoGuipuzcoa = new KeyValuePair<string, string>(new Guid("3d84e33e-938a-4088-8e99-05de4149421c").ToString(), "24288");
        public static readonly KeyValuePair<string, string> JuntaGobiernoHuelva = new KeyValuePair<string, string>(new Guid("76ea584a-9379-4cbf-9e9f-b49d827720da").ToString(), "24289");
        public static readonly KeyValuePair<string, string> JuntaGobiernoHuesca = new KeyValuePair<string, string>(new Guid("3431bbd1-466f-4829-afa2-264f2ce0e4a5").ToString(), "24290");
        public static readonly KeyValuePair<string, string> JuntaGobiernoJaen = new KeyValuePair<string, string>(new Guid("5ad0bd09-b67b-463f-b65b-181176a7acd9").ToString(), "24291");
        public static readonly KeyValuePair<string, string> JuntaGobiernoLeon = new KeyValuePair<string, string>(new Guid("ce85cf19-64ec-4a91-964f-30873f9a8a9a").ToString(), "24292");
        public static readonly KeyValuePair<string, string> JuntaGobiernoLerida = new KeyValuePair<string, string>(new Guid("513f9fd2-f567-4705-92aa-a393443346eb").ToString(), "24293");
        public static readonly KeyValuePair<string, string> JuntaGobiernoLaRioja = new KeyValuePair<string, string>(new Guid("d8201545-9aae-49aa-b7d4-0c8d37ed4e23").ToString(), "24294");
        public static readonly KeyValuePair<string, string> JuntaGobiernoLugo = new KeyValuePair<string, string>(new Guid("7a238ec1-23aa-4634-816b-e02cf115d6cf").ToString(), "24295");
        public static readonly KeyValuePair<string, string> JuntaGobiernoMadrid = new KeyValuePair<string, string>(new Guid("dfb1d69b-c915-4162-8260-eccf87486195").ToString(), "24296");
        public static readonly KeyValuePair<string, string> JuntaGobiernoMalaga = new KeyValuePair<string, string>(new Guid("f3c58228-4f45-4dcf-b9c6-d32b0004c953").ToString(), "24297");
        public static readonly KeyValuePair<string, string> JuntaGobiernoAntequera = new KeyValuePair<string, string>(new Guid("a9c4d973-eeeb-4228-879b-6ecdb0880408").ToString(), "24298");
        public static readonly KeyValuePair<string, string> JuntaGobiernoMelilla = new KeyValuePair<string, string>(new Guid("2da6f95e-58b0-4685-9bb7-30ceb42961c3").ToString(), "24299");
        public static readonly KeyValuePair<string, string> JuntaGobiernoMurcia = new KeyValuePair<string, string>(new Guid("ba8c7f7e-0baf-467d-91ab-c0989dcc161a").ToString(), "24300");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCartagena = new KeyValuePair<string, string>(new Guid("26d6ac9e-c519-4b68-b762-2033b949c583").ToString(), "24301");
        public static readonly KeyValuePair<string, string> JuntaGobiernoYecla = new KeyValuePair<string, string>(new Guid("78f6be45-c740-42fc-ae95-6805fe01f550").ToString(), "24302");
        public static readonly KeyValuePair<string, string> JuntaGobiernoLorca = new KeyValuePair<string, string>(new Guid("a6ef1531-5be2-4ae9-a643-553038e707da").ToString(), "24303");
        public static readonly KeyValuePair<string, string> JuntaGobiernoNavarra = new KeyValuePair<string, string>(new Guid("b6dd91e5-0911-441a-aa94-b73cf841c02a").ToString(), "24304");
        public static readonly KeyValuePair<string, string> JuntaGobiernoOrense = new KeyValuePair<string, string>(new Guid("6c6147ee-ce49-48d1-866c-155310fecb2a").ToString(), "24305");
        public static readonly KeyValuePair<string, string> JuntaGobiernoOviedo = new KeyValuePair<string, string>(new Guid("398f8e6d-cfbb-462b-a265-15913e403394").ToString(), "24306");
        public static readonly KeyValuePair<string, string> JuntaGobiernoGijon = new KeyValuePair<string, string>(new Guid("79bed5d3-5615-43e7-be02-76fae55fb59f").ToString(), "24307");
        public static readonly KeyValuePair<string, string> JuntaGobiernoPalencia = new KeyValuePair<string, string>(new Guid("4a235d1b-c19c-4958-9bda-f57bc3a30ba0").ToString(), "24308");
        public static readonly KeyValuePair<string, string> JuntaGobiernoLasPalmas = new KeyValuePair<string, string>(new Guid("296a1978-d148-41d7-b539-d830bd477ec3").ToString(), "24309");
        public static readonly KeyValuePair<string, string> JuntaGobiernoPontevedra = new KeyValuePair<string, string>(new Guid("f244a0a7-1614-4d29-a331-3cdd6b8a1e25").ToString(), "24310");
        public static readonly KeyValuePair<string, string> JuntaGobiernoVigo = new KeyValuePair<string, string>(new Guid("3d341eb4-21e3-4fa8-9527-d8079fd42767").ToString(), "24311");
        public static readonly KeyValuePair<string, string> JuntaGobiernoSalamanca = new KeyValuePair<string, string>(new Guid("fbf69aef-aba5-45fb-a2b8-9e71784e017f").ToString(), "24312");
        public static readonly KeyValuePair<string, string> JuntaGobiernoTenerife = new KeyValuePair<string, string>(new Guid("978b23b1-cc56-4bf1-a89e-80fe6b41b499").ToString(), "24313");
        public static readonly KeyValuePair<string, string> JuntaGobiernoCantabria = new KeyValuePair<string, string>(new Guid("57de6b6b-ae5e-4aa8-89ae-225eceef29ce").ToString(), "24314");
        public static readonly KeyValuePair<string, string> JuntaGobiernoSegovia = new KeyValuePair<string, string>(new Guid("aa4290a1-b5a9-42ac-bb96-83d93405c971").ToString(), "24315");
        public static readonly KeyValuePair<string, string> JuntaGobiernoSevilla = new KeyValuePair<string, string>(new Guid("b47effe4-6a05-461c-b9db-c69d89fc96dc").ToString(), "24316");
        public static readonly KeyValuePair<string, string> JuntaGobiernoSoria = new KeyValuePair<string, string>(new Guid("a4575108-3483-435d-887b-d562267cef96").ToString(), "24317");
        public static readonly KeyValuePair<string, string> JuntaGobiernoTarragona = new KeyValuePair<string, string>(new Guid("d1ebd603-e5fe-47e3-a476-0e1798d2f963").ToString(), "24318");
        public static readonly KeyValuePair<string, string> JuntaGobiernoReus = new KeyValuePair<string, string>(new Guid("d5cb6e91-1f9e-4dfb-b74f-eed1b40c4a23").ToString(), "24319");
        public static readonly KeyValuePair<string, string> JuntaGobiernoTortosa = new KeyValuePair<string, string>(new Guid("8fd3dfa9-a4f5-475a-bb56-c0a91e8c6e1d").ToString(), "24320");
        public static readonly KeyValuePair<string, string> JuntaGobiernoTeruel = new KeyValuePair<string, string>(new Guid("cbbadc93-e92d-42d3-8463-974ca32ae261").ToString(), "24321");
        public static readonly KeyValuePair<string, string> JuntaGobiernoToledo = new KeyValuePair<string, string>(new Guid("1dc71fb9-f63e-47a6-aee1-4962466fd37c").ToString(), "24322");
        public static readonly KeyValuePair<string, string> JuntaGobiernoValencia = new KeyValuePair<string, string>(new Guid("901fa9c3-27aa-4331-8d83-3fe897343b5e").ToString(), "24323");
        public static readonly KeyValuePair<string, string> JuntaGobiernoValladolid = new KeyValuePair<string, string>(new Guid("12171abb-75de-49b7-b64a-0c494932b5d4").ToString(), "24324");
        public static readonly KeyValuePair<string, string> JuntaGobiernoVizcaya = new KeyValuePair<string, string>(new Guid("dc07f12b-448a-4ad6-a828-103be0f5fdda").ToString(), "24325");
        public static readonly KeyValuePair<string, string> JuntaGobiernoZamora = new KeyValuePair<string, string>(new Guid("c3f3b7ea-d853-4803-b910-0c845092e162").ToString(), "24326");
        public static readonly KeyValuePair<string, string> JuntaGobiernoZaragoza = new KeyValuePair<string, string>(new Guid("6e7ca2c3-09c9-4896-870d-858067f4bbfd").ToString(), "24327");


        // Consejos regionales

        public static readonly KeyValuePair<string, string> JuntaConsejoValenciano = new KeyValuePair<string, string>(new Guid("c33e0d43-a2fe-4788-9b7f-82e103309863").ToString(), "24340");
        public static readonly KeyValuePair<string, string> JuntaConsellCatalunya = new KeyValuePair<string, string>(new Guid("2c81d1be-d251-4213-999b-94058226ddf5").ToString(), "24341");
        public static readonly KeyValuePair<string, string> JuntaConselloGalego = new KeyValuePair<string, string>(new Guid("e058f35d-5832-49ce-8782-bcd3017ea120").ToString(), "24342");
        public static readonly KeyValuePair<string, string> JuntaConsejoCanario = new KeyValuePair<string, string>(new Guid("dbde8f28-6fdb-4452-a75f-98626ae4126b").ToString(), "33183");
        public static readonly KeyValuePair<string, string> JuntaConsejoCastillaLeon = new KeyValuePair<string, string>(new Guid("88a01fe6-bc3e-4a04-bc00-d4787d1806ef").ToString(), "33667");
        public static readonly KeyValuePair<string, string> JuntaConsejoCastillaLaMancha = new KeyValuePair<string, string>(new Guid("2d87d492-5803-4aa6-9dd1-9a765c557761").ToString(), "33668");
        public static readonly KeyValuePair<string, string> PlenoConsejoAndaluz = new KeyValuePair<string, string>(new Guid("4f3d15ce-dbef-42fa-ba34-694bcaa01ed7").ToString(), "33985");
        public static readonly KeyValuePair<string, string> ComisionPermanenteConsejoAndaluz = new KeyValuePair<string, string>(new Guid("6e9b62a2-1d0c-4732-9c2c-d72e413ff6e2").ToString(), "33986");
        public static readonly KeyValuePair<string, string> PlenoConsejoPaisVasco = new KeyValuePair<string, string>(new Guid("3c010b4c-03f0-4b6c-8ea3-87d2513270b2").ToString(), "34038");
        public static readonly KeyValuePair<string, string> JuntaGobiernoConsejoAragon = new KeyValuePair<string, string>(new Guid("5d877e2a-87d5-4388-a331-052e7e24e4be").ToString(), "34407");



    }

}
