using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public class Honour
    {

        public string HonourId { get; set; }

        public HonourType HonourType { get; set; }
        public string HonourLiteral { get; set; }

        public DateTime HonourDate { get; set; }

        public void Validate()
        {
            // DATOS PERSONALES

            string defaultstring = new Guid().ToString();

            // El tipo de distinción es obligatorio.
            if (this.HonourType == null || this.HonourType.TypeId == null || defaultstring.Equals(this.HonourType.TypeId))
            {
                throw new Exception(Resources.HonourTypeRequiredValidation);
            }

            // La descripción de la distinción es obligatoria si el tipo de distinción es "Otra".
            if (this.HonourType.TypeId == HonourTypes.Otra && string.IsNullOrWhiteSpace(this.HonourLiteral))
            {
                throw new Exception(Resources.HonourLiteralRequiredValidation);
            }

            // La fecha de concesión de la distinción es obligatoria.
            if (this.HonourDate == null)
            {
                throw new Exception(Resources.HonourDateRequiredValidation);
            }
        }

    }
}
