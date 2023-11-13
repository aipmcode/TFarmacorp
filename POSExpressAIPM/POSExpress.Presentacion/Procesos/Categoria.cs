using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSExpress.Presentacion.Procesos
{
    internal class Categoria
    {
        public int idCategoria { get; set; }
        public string descripcion { get; set; }
        internal static async Task<Respuesta<List<Categoria>>> ObtenerCategoriasAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConstantesServicio.url + ConstantesServicio.ObtenerCategorias;
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Respuesta<List<Categoria>>>();
            }
        }
        internal static void MostrarCategorias(List<Categoria> categorias)
        {
            Console.WriteLine("Lista de Categorías:");
            foreach (var categoria in categorias)
            {
                Console.WriteLine($"ID: {categoria.idCategoria}, Nombre: {categoria.descripcion}");
            }
            Console.WriteLine();
        }
        internal static async Task<Respuesta<int>> RegistrarAsignacionCategoria(int idCategoria, int idProducto)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConstantesServicio.url + ConstantesServicio.AsignarCategoria;
                CategoriaProducto categoriaProducto = new CategoriaProducto() { idCategoria = idCategoria, idProducto = idProducto };
                string jsonBody = JsonConvert.SerializeObject(categoriaProducto);
                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Respuesta<int>>();
            }
        }
        internal static async Task<Respuesta<List<CategoriaProducto>>> ObtenerProdcutosCategoriasPorIdProductoAsync(int idProducto)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = ConstantesServicio.url +ConstantesServicio.ObtenerProductosCategorias+ $"?idProducto={idProducto}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Respuesta<List<CategoriaProducto>>>();
            }
        }
        public static async Task RegistrarAsignacion(int idProducto)
        {
            Console.WriteLine("*** 3. Registro de Asignación de Categorías de Producto. ***");

            Console.WriteLine("Elija una opción de Lista de CATEGORIAS:");
            Respuesta<List<Categoria>> resultadoObtCat = await ObtenerCategoriasAsync();
            List<Categoria> listaCategorias = resultadoObtCat.datos;
            MostrarCategorias(listaCategorias);

            Console.WriteLine("Ingrese el ID Categoria.");
            string idCategoriaStr = Console.ReadLine();
            int idCategoria = int.Parse(idCategoriaStr);

            Console.WriteLine("Registrando Asignacion de categoria....");
            var resultadoCat = await RegistrarAsignacionCategoria(idCategoria, idProducto);
            Console.WriteLine("************************************");
            Console.WriteLine($"PROCESO COMPLETADO ");
            Console.WriteLine($"Registro signacion de categoria :{resultadoCat.mensaje}");
            Console.WriteLine($"ID Asignación de Categorías :{resultadoCat.datos}");
            Console.WriteLine("************************************");
        }
    }

    //internal class Categoria
    //{
    //    public int idCategoria { get; set; }
    //    public string descripcion { get; set; }
    //}
    internal class CategoriaProducto
    {
        public int idCategoria { get; set; }
        public int idProducto { get; set; }
    }
}
