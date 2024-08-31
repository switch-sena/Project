using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Switch.Models
{
    [Table("public_modal")]
    public class PublicModal
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_publ")]
        public int IdPubl { get; set; }

        [Column("id_moda")]
        public int IdModa { get; set; }

        // Navegación opcional si deseas acceder a las entidades relacionadas
        public virtual Publicacion Publicacion { get; set; }
        public virtual Modalidad Modalidad { get; set; }
    }
}
