using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class SuspensionEntity
    {
        /// <summary>
        /// Identificador de la suspensión global
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SuspensionId { get; set; }
        /// <summary>
        /// Fecha de inicio de la suspensión. En esta fecha el procurador ya no estará activo.
        /// </summary>
        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Fecha de fin de la suspensión. Es el último día en que la suspensión tiene efecto, es decir,
        /// este día el procurador seguirá sin estar activo.
        /// </summary>
        [Column(TypeName="date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Razón por la que el procurador ha sido suspendido.
        /// </summary>
        [Required, MaxLength(250)]
        public string Reason { get; set; }


        /// <summary>
        /// Id del procurador que ha sido suspendido
        /// </summary>
        [Required, ForeignKey("ProcuratorSuspended")]
        public string ProcuratorId { get; set; }
        /// <summary>
        /// Procurador que ha sido suspendido
        /// </summary>
        public virtual ProcuratorEntity ProcuratorSuspended { get; set; }

        /// <summary>
        /// Id del colegio que ha solicitado la suspensión global
        /// </summary>
        [Required, ForeignKey("AssociationAgreeingOnSuspension")]
        public string AssociationId { get; set; }
        /// <summary>
        /// Colegio que ha solicitado la suspensión global
        /// </summary>
        public virtual AssociationEntity AssociationAgreeingOnSuspension { get; set; }

    }

}
