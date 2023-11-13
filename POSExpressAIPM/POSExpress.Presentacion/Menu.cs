using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSExpress.Presentacion
{
    internal static  class Menu
    {

        internal static void MostrarMenu()
        {
            
            Console.WriteLine("===== MENÚ PRINCIPAL =====");
            Console.WriteLine("1. Registro Nuevo Producto ERP");
            Console.WriteLine("2. Registro Asignación de Códigos de Barras");
            Console.WriteLine("3. Registro de Asignación de Categorías de Productos");
            Console.WriteLine("4. Registro de Venta");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción:");
        }
        internal static int ObtenerOpcion()
        {
            int opcion;
            while (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Entrada no válida. Introduce un número.");
            }
            return opcion;
        }
        public static string SolicitarInput(string mensaje)
        {
            Console.WriteLine(mensaje);
            return Console.ReadLine();
        }
    }
}
