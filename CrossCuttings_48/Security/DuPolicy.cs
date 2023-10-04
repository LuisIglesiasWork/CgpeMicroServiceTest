using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.CrossCuttings
{

    public class DuPolicy
    {

        public Guid PolicyId { get; set; }

        public Func<Procurator, bool> Check;
        

    }

}
