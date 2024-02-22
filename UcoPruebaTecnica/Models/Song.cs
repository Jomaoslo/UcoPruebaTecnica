namespace UcoPruebaTecnica.Models
{
    /// Entidad para almacenar los datos de los repositorios y su total de commits
    public class Song
    {
        public long IdCancion { get; set; }
        public long IdArtista { get; set; }
        public string Nombre { get; set; } 
        public string Duracion { get; set; }
    }    
}