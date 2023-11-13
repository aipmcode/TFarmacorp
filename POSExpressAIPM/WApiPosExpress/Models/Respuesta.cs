namespace WApiPosExpress.Models
{
    public class Respuesta
    {
        public bool esCorrecto { get; set; }
        public string mensaje { get; set; }
        public dynamic? datos { get; set; }
        public Respuesta()
        {
            this.esCorrecto = false;
            this.mensaje = "Ha ocurrido un error.";
            this.datos = null;
        }
    }
}
