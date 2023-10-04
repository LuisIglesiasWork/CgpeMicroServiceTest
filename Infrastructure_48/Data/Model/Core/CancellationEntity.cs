using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cgpe.Du.Infrastructure.Data
{

    public class CancellationEntity
    {
        /// <summary>
        /// Identificador de la cancelación de la colegiación
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CancellationId { get; set; }
        /// <summary>
        /// Fecha de inicio de la cancelación. En esta fecha la colegiación ya no estará activa.
        /// </summary>
        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Fecha de fin de la cancelación. Es el último día en que la cancelación tiene efecto, es decir,
        /// este día la colegiación seguirá sin estar activa.
        /// </summary>
        [Column(TypeName="date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Razón por la que la colegiación ha sido cancelada.
        /// </summary>
        [Required, MaxLength(250)]
        public string Reason { get; set; }


        /// <summary>
        /// Id de la colegiación que ha sido cancelada
        /// </summary>
        [Required, ForeignKey("AssociationProcuratorCancelled")]
        public string AssociationProcuratorId { get; set; }
        /// <summary>
        /// Procurador que ha sido suspendido
        /// </summary>
        public virtual AssociationProcuratorEntity AssociationProcuratorCancelled { get; set; }


    }

}
