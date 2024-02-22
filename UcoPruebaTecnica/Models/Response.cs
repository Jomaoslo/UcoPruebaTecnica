namespace UcoPruebaTecnica.Models
{
    /// Entidad para almacenar los procesos para validar si existen o no errores
    public class Response
    {
        public bool State { get; set; }     
        public int Code { get; set; }     
        public string Messsage { get; set; }
    }
}