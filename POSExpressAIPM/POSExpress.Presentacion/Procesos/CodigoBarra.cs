using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSExpress.Presentacion.Procesos
{
    internal class CodigoBarra
    {
        public int IdCodigoBarra { get; set; }
        public string UniqueCodigo { get; set; }
        public string Activo { get; set; }
        public int IdProducto { get; set; }
        private static async Task<Respuesta<int>> RegistrarCodigoBarra(int idProducto, string codigoUnico)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConstantesServicio.url + ConstantesServicio.RegistrarCodigoBarrar;
                CodigoBarra codigoBarra = new CodigoBarra() { UniqueCodigo = codigoUnico, Activo = "1", IdProducto = idProducto };
                string jsonBody = JsonConvert.SerializeObject(codigoBarra);
                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Respuesta<int>>();
            }

        }
        public static async Task RegistrarAsignacionCodigoBarra(int idProducto)
        {
            string CodigoBarraUnico = Guid.NewGuid().ToString();

            Console.WriteLine("*** 2. Registro Asignación de Códigos de Barras. ***");
            Console.WriteLine($"ID Producto :{idProducto}");
            Console.WriteLine($"CodigoBarraUnico :{CodigoBarraUnico}");
            Console.WriteLine("Registrando Codigo de Barra....");

            var resultadoCB = await RegistrarCodigoBarra(idProducto, CodigoBarraUnico);
            Console.WriteLine("************************************");
            Console.WriteLine($"PROCESO COMPLETADO "); 
            Console.WriteLine($"Registro Codigo Barra :{resultadoCB.mensaje}");
            Console.WriteLine($"ID CodigoBarra :{resultadoCB.datos}");
            Console.WriteLine("************************************");
            
            
        }
    }
    //internal class CodigoBarra
    //{
    //    public int IdCodigoBarra { get; set; }
    //    public string UniqueCodigo { get; set; }
    //    public string Activo { get; set; }
    //    public int IdProducto { get; set; }

    //}

}
