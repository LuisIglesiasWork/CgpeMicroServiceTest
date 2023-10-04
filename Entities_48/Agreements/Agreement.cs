using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public class Agreement
    {

        public string AgreementId { get; set; }

        public string AgreementName { get; set; }

        public string AgreementCode { get; set; }

        public string AgreementDescription { get; set; }

        public DateTime ForceDate { get; set; }

        public bool RequiresAssociation { get; set; }
        public bool RequiresBankAccount { get; set; }
        public bool RequiresContactData { get; set; }

        [IgnoreDataMember]
        public List<ProcuratorAdherence> ProcuratorAdherences { get; set; }

        public Agreement()
        {
            this.ProcuratorAdherences = new List<ProcuratorAdherence>();
        }

    }

}
