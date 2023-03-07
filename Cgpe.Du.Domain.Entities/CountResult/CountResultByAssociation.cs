using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{
    public class CountResultByAssociation
    {
        public int Total { get; set; }
        public int Exercising { get; set; }
        public int NonExercising { get; set; }
        public int AdheredToUIHJ { get; set; }
        public string AssociationName { get; set; }
    }
}
