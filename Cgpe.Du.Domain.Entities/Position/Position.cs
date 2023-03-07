using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{

    public class Position
    {

        public Guid PositionId { get; set; }

        public Organ Organ { get; set; }

        public Organization Organization { get; set; }

        public OrganizationType OrganizationType { get; set; }

        public PositionType PositionType { get; set; }

        public DateTime ElectedDate { get; set; }

        public DateTime? FiredDate { get; set; }

        public void Validate()
        {
            // DATOS PERSONALES

            Guid defaultGuid = new Guid();

            // El organismo es obligatorio.
            if (this.Organization == null || this.Organization.OrganizationId == null || defaultGuid.Equals(this.Organization.OrganizationId))
            {
                throw new Exception(Resources.OrganizationRequiredValidation);
            }

            // El órgano es obligatorio.
            if (this.Organ == null || this.Organ.OrganId == null || defaultGuid.Equals(this.Organ.OrganId))
            {
                throw new Exception(Resources.OrganRequiredValidation);
            }

            // La fecha de nombramiento es obligatoria.
            if (this.ElectedDate == null)
            {
                throw new Exception(Resources.ElectedDateRequiredValidation);
            }
        }
    }
}
