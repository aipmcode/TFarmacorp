using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;

namespace POSExpress.Presentacion.Procesos
{
    internal class Venta
    {
        internal static bool VerificarStock(int cantidad, int stock, bool flagGamaMax)
        {
            if (flagGamaMax)
                return stock - cantidad > 10;
            else
                return stock >= cantidad;
        }
        internal static double CalcularDescuento(double total, bool flagGamaMax)
        {
            if (flagGamaMax)
                return total * 0.1;
            else
                return total * 0.3;
        }
        internal static async Task<Respuesta<int>> RegistroVentaAsync(VentaExpress venta)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConstantesServicio.url + ConstantesServicio.RegistrarVenta;
                string jsonBody = JsonConvert.SerializeObject(venta);
                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Respuesta<int>>();
            }

        }
        public static async Task RealizarRegistroVenta(Producto producto)
        {
            Console.WriteLine("*** 4. Registro de Venta. ***");
            Console.WriteLine("Ingrese el Nombre del cliente: ");
            string NombreCliente = Console.ReadLine();

            Console.WriteLine("Datos del producto de la venta");
            Console.WriteLine($"Nombre: {producto.Nombre} - Stock: {producto.Stock} - Tipo de Producto: {producto.tipoProducto.Descripcion}");
            Console.WriteLine($"Precio: {producto.Precio}");

            Console.WriteLine("Ingresae la cantidad a comprar:");
            string cantidadStr = Console.ReadLine();
            int cantidad = int.Parse(cantidadStr);

            bool flagGanaMax = ProductoErp.RangoGanaMax(producto.FechaRegistro, DateTime.Parse("15-08-2023"), DateTime.Parse("15-11-2023"));
            bool flagContinuar = VerificarStock(cantidad, producto.Stock, flagGanaMax);

            if (flagContinuar)
            {
                VentaExpress venta = CrearVenta(NombreCliente, cantidad, producto);

                Respuesta<List<CategoriaProducto>> resultadoProdCat = await Categoria.ObtenerProdcutosCategoriasPorIdProductoAsync(producto.IdProducto);
                List<CategoriaProducto> listaCatProd = resultadoProdCat.datos;

                if (listaCatProd != null && listaCatProd.Count == 1)
                {
                    double montoDescuento = CalcularDescuento(venta.Total, flagGanaMax);
                    venta.Descuento = montoDescuento;
                    venta.Total = venta.Total - montoDescuento;
                }

                MostrarDetalleVenta(venta);

                Console.WriteLine("Registrando venta...");
                var resultadoVenta = await RegistroVentaAsync(venta);
                Console.WriteLine("************************************");
                Console.WriteLine($"VENTA COMPLETADA ");
                Console.WriteLine($"Registro de venta :{resultadoVenta.mensaje}");
                Console.WriteLine($"ID VENTA :{resultadoVenta.datos}");
                Console.WriteLine("************************************");
                
            }
        }
        public static VentaExpress CrearVenta(string nombreCliente, int cantidad, Producto producto)
        {
            int IdProductoNuevo = producto.IdProducto;

            return new VentaExpress()
            {
                Fecha = DateTime.Now,
                Cliente = nombreCliente,
                Producto = producto.Nombre,
                UniqueProducto = producto.UniqueCodigo,
                Cantidad = cantidad,
                Precio = producto.Precio,
                Descuento = 0,
                Total = producto.Precio * cantidad,
                IdProducto = IdProductoNuevo
            };
        }
        public static void MostrarDetalleVenta(VentaExpress venta)
        {
            Console.WriteLine("************************************");
            Console.WriteLine("DETALLE DE LA VENTA:");
            Console.WriteLine("************************************");            
            Console.WriteLine($"Fecha: {venta.Fecha}");
            Console.WriteLine($"Cliente: {venta.Cliente}");
            Console.WriteLine($"Producto: {venta.Producto}");
            Console.WriteLine($"UniqueProducto: {venta.UniqueProducto}");
            Console.WriteLine($"Cantidad: {venta.Cantidad}");
            Console.WriteLine($"Precio: {venta.Precio}");
            Console.WriteLine($"Descuento: {venta.Descuento}");
            Console.WriteLine("************************************");
            Console.WriteLine($"Total: {venta.Total}");
            Console.WriteLine("************************************");
        }
    }
    public class VentaExpress
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public string UniqueProducto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Descuento { get; set; }
        public double Total { get; set; }
        public int IdProducto { get; set; }

    }
}
