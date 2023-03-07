using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class MinistryIntegrationStates
    {
        public static readonly KeyValuePair<Guid, int> Registered = new KeyValuePair<Guid, int>(new Guid("e184ff52-2e75-47a7-bfd7-4a37e1726474"), (int)MinistryIntegrationStatesEnum.Registered);
        public static readonly KeyValuePair<Guid, int> Unregistered = new KeyValuePair<Guid, int>(new Guid("8b4832b3-9025-46c5-b7db-6150834aeb4a"), (int)MinistryIntegrationStatesEnum.Unregistered);
        public static readonly KeyValuePair<Guid, int> RegisteredSent = new KeyValuePair<Guid, int>(new Guid("3343afc2-4e7d-4ad6-8f41-1ff64534fbe9"), (int)MinistryIntegrationStatesEnum.RegisteredSent);
        public static readonly KeyValuePair<Guid, int> UnregisteredSent = new KeyValuePair<Guid, int>(new Guid("764c1d9b-f5a6-4bb9-a9e0-d3c61acfb80c"), (int)MinistryIntegrationStatesEnum.UnregisteredSent);
    }

}
