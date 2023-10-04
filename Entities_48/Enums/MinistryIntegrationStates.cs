using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public static class MinistryIntegrationStates
    {
        public static readonly KeyValuePair<string, int> Registered = new KeyValuePair<string, int>(new Guid("e184ff52-2e75-47a7-bfd7-4a37e1726474").ToString(), (int)MinistryIntegrationStatesEnum.Registered);
        public static readonly KeyValuePair<string, int> Unregistered = new KeyValuePair<string, int>(new Guid("8b4832b3-9025-46c5-b7db-6150834aeb4a").ToString(), (int)MinistryIntegrationStatesEnum.Unregistered);
        public static readonly KeyValuePair<string, int> RegisteredSent = new KeyValuePair<string, int>(new Guid("3343afc2-4e7d-4ad6-8f41-1ff64534fbe9").ToString(), (int)MinistryIntegrationStatesEnum.RegisteredSent);
        public static readonly KeyValuePair<string, int> UnregisteredSent = new KeyValuePair<string, int>(new Guid("764c1d9b-f5a6-4bb9-a9e0-d3c61acfb80c").ToString(), (int)MinistryIntegrationStatesEnum.UnregisteredSent);
    }

}
