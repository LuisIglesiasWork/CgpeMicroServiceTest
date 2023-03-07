using System;
using System.Collections.Generic;

namespace Cgpe.Du.Domain.Entities
{

    public class ProcuratorAdherence
    {

        public Guid ProcuratorAdherenceId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string BankAccount { get; set; }

        public string OtherData { get; set; }

        public Agreement Agreement { get; set; }

        public Association Association { get; set; }

        public Procurator Procurator { get; set; }

        public Address Address { get; set; }

        public Contact Phone { get; set; }

        public Contact Mobile { get; set; }

        public Contact Email { get; set; }

    }

}
