using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public class Organ
    {

        public Guid OrganId { get; set; }
        public string OrganCode { get; set; }
        public string OrganName { get; set; }
        public Guid OrganizationId { get; set; }

    }

}
