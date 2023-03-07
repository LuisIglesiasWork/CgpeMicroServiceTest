using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cgpe.Du.Infrastructure.Data
{

    public class ContactTypeEntity
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid TypeId { get; set; }

        [MaxLength(30)]
        public string TypeName { get; set; }

    }

}
