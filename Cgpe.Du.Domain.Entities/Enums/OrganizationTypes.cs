using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class OrganizationTypes
    {
        public static readonly KeyValuePair<Guid, string> ConsejoGeneral = new KeyValuePair<Guid, string>(new Guid("4da59fba-1bd6-4d84-a720-b8baa1dd2b21"), "1");
        public static readonly KeyValuePair<Guid, string> ConsejoAutonomico = new KeyValuePair<Guid, string>(new Guid("f884e400-d3cf-4a3b-90bf-c4d1c36f2c50"), "3");
        public static readonly KeyValuePair<Guid, string> ColegioDeProcuradores = new KeyValuePair<Guid, string>(new Guid("5d6d4885-faa2-4975-9df9-04a3561e5c93"), "5");
    }

}
