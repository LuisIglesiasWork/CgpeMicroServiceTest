using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class DuClaimTypes
    {
        public static readonly string QueryMasterData = new Guid("{0006df9d-eca8-4037-a453-7b41ca59bae2}").ToString();

        public static readonly string QueryProcurator = new Guid("{17C13060-33FA-473C-A485-EFE14D5BBEE8}").ToString();
        public static readonly string QueryAnyProcurator = new Guid("{65841521-90CC-42B0-AB42-F02970C8B961}").ToString();

        public static readonly string CreateAnyProcurator = new Guid("{28129268-A6A5-438C-96E7-F103E5DD010D}").ToString();
        public static readonly string UpdateAnyProcurator = new Guid("{D3BA23D6-136F-43D9-A092-F16DE7303D62}").ToString();

        public static readonly string SuperviseDataEntry = new Guid("{7f4e7eef-7cfe-4538-a606-abb28d1f7168}").ToString();
        public static readonly string PrecreateProcurator = new Guid("{f16e1517-6abd-4b79-a5a3-1417eb9d9834}").ToString();
        public static readonly string PrecreateAssociationProcurator = new Guid("{0391ac79-b19f-46c4-8c85-a629cd30abfe}").ToString();


        public static readonly string QueryAssociation = new Guid("{2922CFA5-E285-42DB-8323-F3B06E3FBF00}").ToString();
        public static readonly string QueryAnyAssociation = new Guid("{F96BBA3B-59CF-4F1A-8984-F72CD56F3A25}").ToString();

        public static readonly string CreateAssociation = new Guid("{1FC0B931-7676-40F0-A71D-F907E37748F7}").ToString();
        public static readonly string CreateAnyAssociation = new Guid("{58CE7B55-5CF2-4D8F-9246-F934F761E1A3}").ToString();

        public static readonly string UpdateAssociation = new Guid("{61E0C5C5-AB3F-4FC9-AE96-F9AD1EE47E27}").ToString();
        public static readonly string UpdateAnyAssociation = new Guid("{88F82945-7ED6-439F-B384-F9B1A9781413}").ToString();

        public static readonly string QueryUser = new Guid("{9A624E39-15CA-4019-992D-D6D4B0913AE6}").ToString();
        public static readonly string QueryAnyUser = new Guid("{D6A2A446-A355-4AAC-AF98-C3D34CA55540}").ToString();

        public static readonly string CreateUser = new Guid("{E35AD463-2DFE-4572-8CBE-B2253E703504}").ToString();
        public static readonly string CreateAnyUser = new Guid("{E1A40EC9-8905-4C70-AF39-B15808AA38FB}").ToString();

        public static readonly string UpdateUser = new Guid("{CCDA1C39-96C2-4F8C-8836-B099EDD4E835}").ToString();
        public static readonly string UpdateAnyUser = new Guid("{C4324FAA-379C-4082-BDAC-89F43FC065AB}").ToString();

        public static readonly string DeleteUser = new Guid("{D558FCCB-1336-44E4-9020-6274552C182A}").ToString();
        public static readonly string DeleteAnyUser = new Guid("{230C9C29-3E90-4739-8C9D-5A88CC29DFB0}").ToString();

        public static readonly string Reader = new Guid("{26FC6111-3D80-4FE8-A021-292117D45B5E}").ToString();
        public static readonly string Copyer = new Guid("{213B5916-8567-44C2-8865-2D74B5AC50B7}").ToString();
        public static readonly string Notary = new Guid("{e9ed3ccc-24be-4a23-8098-bacea3ab9396}").ToString();
        public static readonly string BOE = new Guid("{2f3ec5f5-0bb4-4dae-9b6b-065c875e6914}").ToString();
        public static readonly string Cgpj = new Guid("{544EA68A-9D83-4B40-ADB9-5F2CB15C440F}").ToString();
        public static readonly string Registrator = new Guid("{CD3A172E-C425-4454-8FFE-DE57A7B98DE0}").ToString();

        public static readonly string CgpeInternal = new Guid("{687c7646-c69c-4205-abdd-bd5cc3100521}").ToString();



        public static readonly string ShowLogProcurator = new Guid("{8b8d2b67-07eb-4a4c-8764-1dbd42e16e02}").ToString();

    }

}
