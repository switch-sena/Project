namespace SwitchBack.Models
{
    public class PublicacionesDTO
    {
        public int IdPubl { get; set; }
        public string TituloPubl { get; set; }
        public string DescripcionPubl { get; set; }
        public string NombreUsuario { get; set; }
        public List<string> Habilidades { get; set; }
        public List<string> Modalidades { get; set; }
    }
}
