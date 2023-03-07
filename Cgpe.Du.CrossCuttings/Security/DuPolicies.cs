using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.CrossCuttings
{

    public static class DuPolicies
    {

        // Procurator
        public static readonly Guid CreateProcurator = new Guid("{E0F483EE-CB55-4909-9BEF-7E0170B8D44D}");
        public static readonly Guid ReadProcurator = new Guid("{47AC304D-3C29-4FA2-BC2A-8E92A54D3C42}");
        public static readonly Guid UpdateProcurator = new Guid("{378FE296-423D-4657-9C06-EBE72F7E1B3E}");

        // Association
        public static readonly Guid CreateAssociation = new Guid("{F51053A6-8A70-4EB9-825C-52D08436A0BC}");
        public static readonly Guid ReadAssociation = new Guid("{00AF720B-57EB-4BBA-AC3C-C14727169989}");
        public static readonly Guid UpdateAssociation = new Guid("{0A5F2F5F-A061-4D67-AF07-1E4AED558E9E}");

        // Master data
        public static readonly Guid ReadMasterData = new Guid("{88831FA3-3F87-43F3-86E4-55D1491188D9}");


        

    }

}
