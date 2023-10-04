using System;

namespace Cgpe.Du.Domain.Entities
{
    public class Cancellation
    {
        public string CancellationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
    }
}
