using System;

namespace Cgpe.Du.Domain.Entities
{
    public class Suspension
    {
        public string SuspensionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
        public Association AssociationAgreeingOnSuspension { get; set; }
    }
}
