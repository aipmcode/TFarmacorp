using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace POSExpress.Presentacion.Procesos
{
    internal static class ProductoErp
    {
        public static async Task<Producto> SeleccionarProductoPorIdAsync()
        {
            Console.WriteLine("Obteniendo lista de productos...");
            Producto productoSeleccionado = new();
            var resultado = await ProductoErp.ObtenerTodosProductosAsync();
            if(resultado.datos != null && resultado.datos.Count>0)
            {
                List<Producto> listaProd = resultado.datos;
                ProductoErp.MostrarListaProductos(listaProd);

                Console.WriteLine("Ingrese Id Producto:");
                string idProductoStr = Console.ReadLine();
                int idProducto = int.Parse(idProductoStr);

                // Buscar el producto por ID en la lista
                productoSeleccionado = listaProd.FirstOrDefault(p => p.IdProducto == idProducto);
                if (productoSeleccionado == null)
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }
            return productoSeleccionado;
        }
        internal static async Task<List<TiposProducto>> ObtenertipoProductoAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConstantesServicio.url + ConstantesServicio.ObtenerTiposProductos;
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<TiposProducto>>();
            }
        }

        internal static async Task<Respuesta<int>> RegistroProductoAsync(Producto producto)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConstantesServicio.url + ConstantesServicio.RegistrarProducto;
                string jsonBody = JsonConvert.SerializeObject(producto);
                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Respuesta<int>>();
            }

        }
        internal static async Task<Respuesta<List<Producto>>> ObtenerTodosProductosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConstantesServicio.url + ConstantesServicio.ObtenerTodosProductos;
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Respuesta<List<Producto>>>();
            }
        }
        internal static void MostrarListaProductos(List<Producto> productos)
        {
            Console.WriteLine("Lista de Productos:");
            foreach (var producto in productos)
            {
                Console.WriteLine($"ID: {producto.IdProducto}, Nombre: {producto.Nombre}, Precio: {producto.Precio}, stock: {producto.Stock}");
            }
            Console.WriteLine();
        }

        internal static void MostrarTipoProducto(List<TiposProducto> tipoProducto)
        {
            Console.WriteLine("Lista de TiposProducto:");
            foreach (var item in tipoProducto)
            {
                Console.WriteLine($"ID: {item.IdTipoProducto}, Nombre: {item.Descripcion}");
            }
            Console.WriteLine();
        }
        internal static bool RangoGanaMax(DateTime fecha, DateTime fechaInicio, DateTime fechaFin)
        {
            return fecha >= fechaInicio && fecha <= fechaFin;
        }

        internal static async Task<Producto> RealizarRegistroNuevoProductoERP()
        {
            Console.WriteLine("***1. Registro Nuevo Producto ERP.***");

            string nombreProductoStr = SolicitarInput("Ingrese el nombre del producto:");
            double costo = ObtenerCostoProducto();
            string stockStr = SolicitarInput("Ingrese el stock del producto:");
            string ObservacionStr = SolicitarInput("Ingrese una observacion del producto:");

            List<TiposProducto> lista = await ObtenertipoProductoAsync();
            MostrarTipoProducto(lista);

            string idTipoProductoStr = SolicitarInput("Ingrese el ID Tipo Producto.");

            Producto producto = CrearNuevoProducto(nombreProductoStr, costo, stockStr, ObservacionStr, lista, idTipoProductoStr);

            bool flagGanaMax = RangoGanaMax(producto.FechaRegistro, DateTime.Parse("15-08-2023"), DateTime.Parse("15-11-2023"));
            if (flagGanaMax)
            {
                producto.Precio = costo + costo * 0.8;
            }

            MostrarDatosProducto(producto);

            Console.WriteLine("Registrando Nuevo Producto ERP....");
            var resultado = await RegistroProductoAsync(producto);
            Console.WriteLine("************************************");
            Console.WriteLine($"PROCESO COMPLETADO ");
            Console.WriteLine($"Registro de producto:{resultado.mensaje}");
            Console.WriteLine($"ID Producto Nuevo :{resultado.datos}");
            Console.WriteLine("************************************");
            
            producto.IdProducto = resultado.datos;
            return producto;
        }
        private static string SolicitarInput(string mensaje)
        {
            Console.WriteLine(mensaje);
            return Console.ReadLine();
        }
        private static double ObtenerCostoProducto()
        {
            Console.WriteLine("Ingrese el costo del producto:");
            string costoProductoStr = Console.ReadLine();
            double costo = 0;
            if (!double.TryParse(costoProductoStr, out costo))
            {
                costo = 100;
                Console.WriteLine("El costo asignado al producto es: " + costo);
            }
           
            return costo;
        }
        private static Producto CrearNuevoProducto(string nombre, double costo, string stockStr, string observacion, List<TiposProducto> lista, string idTipoProductoStr)
        {
            Producto producto = new Producto()
            {
                Nombre = nombre,
                Costo = costo,
                Precio = costo + costo * 0.5,
                UniqueCodigo = Guid.NewGuid().ToString(),
                Stock = int.Parse(stockStr),
                Activo = "1",
                FechaVencimiento = DateTime.Now.AddYears(1),
                FechaRegistro = DateTime.Now,
                Observaciones = observacion,
                tipoProducto = lista.Where(x => x.IdTipoProducto == int.Parse(idTipoProductoStr)).FirstOrDefault()!
            };
            return producto;
        }
        private static void MostrarDatosProducto(Producto producto)
        {
            Console.WriteLine("Datos del nuevo producto Erp");
            Console.WriteLine($"Nombre: {producto.Nombre}");
            Console.WriteLine($"Costo: {producto.Costo}");
            Console.WriteLine($"Precio: {producto.Precio}");
            Console.WriteLine($"Código único: {producto.UniqueCodigo}");
            Console.WriteLine($"Stock: {producto.Stock}");
            Console.WriteLine($"Activo: {producto.Activo}");
            Console.WriteLine($"Fecha de vencimiento: {producto.FechaVencimiento}");
            Console.WriteLine($"Fecha de registro: {producto.FechaRegistro}");
            Console.WriteLine($"Observaciones: {producto.Observaciones}");
            Console.WriteLine($"Tipo de Producto: {producto.tipoProducto.Descripcion}");
        }
      
    }
    internal class Producto
    {
        public int IdProducto { get; set; }
        public double Costo { get; set; }
        public string UniqueCodigo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Stock { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Activo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string? Observaciones { get; set; }
        public TiposProducto tipoProducto { get; set; }

    }

    internal class TiposProducto
    {
        public int IdTipoProducto { get; set; }
        public string? Descripcion { get; set; }

    }
}
