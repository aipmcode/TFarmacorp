using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSExpress.Presentacion
{
    public class Respuesta<T>
    {
        public bool esCorrecto { get; set; }
        public string mensaje { get; set; }
        public T? datos { get; set; }
        public Respuesta()
        {
            this.esCorrecto = false;
            this.mensaje = "Ha ocurrido un error.";            
        }
    }
}
