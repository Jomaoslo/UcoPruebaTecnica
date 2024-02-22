namespace UcoPruebaTecnica.Models
{
    /// Entidad para almacenar los datos de los repositorios y su total de commits
    public class Artist
    {
        public long IdArtista { get; set; }
        public string Nombre { get; set; } 
        public string Pais { get; set; }      
        public string CasaDisquera { get; set; }
    }    
}