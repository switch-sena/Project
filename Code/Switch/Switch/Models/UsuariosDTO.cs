namespace SwitchBack.Models
{
    public class UsuariosDTO
    {
        public int IdUsua { get; set; }
        public required string NombreUsua { get; set; }
        public required string ApellidoUsua { get; set; }
        public required string GeneroUsua { get; set; }
        public required DateTime FechaNacimientoUsua { get; set; }
        public required string CelularUsua { get; set; }
        public required string CorreoUsua { get; set; }
        public required string ClaveUsua { get; set; }
        public required string CorreoElectronicoUsua { get; set; }
        public string? LinksRsUsua { get; set; }
        public string? NombreBarr { get; set; }
        public int IdBarr { get; set; }
    }
}
