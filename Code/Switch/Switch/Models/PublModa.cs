using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Switch.Models
{
    public class PublModa
    {
        [Key]
        public int Id { get; set; }

        public int CopiaIdPubl { get; set; }

        public int CopiaIdModa { get; set; }

        [ForeignKey("CopiaIdPubl")]
        public Publicaciones Publicaciones { get; set; }

        [ForeignKey("CopiaIdModa")]
        public Modalidades Modalidades { get; set; }
    }
}
