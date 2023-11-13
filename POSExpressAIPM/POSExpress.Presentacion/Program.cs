// See https://aka.ms/new-console-template for more information

using POSExpress.Presentacion;
using POSExpress.Presentacion.Procesos;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("******************************************************");
        Console.WriteLine("**TEST: DESARROLLO E IMPLEMENTACIÓN  - ANA PARRAGA.***");
        Console.WriteLine("******************************************************");

        bool continuar = true;
        while (continuar)
        {
            Menu.MostrarMenu();
            int opcion = Menu.ObtenerOpcion();
            switch (opcion)
            {
                case 1:
                    Producto producto = await ProductoErp.RealizarRegistroNuevoProductoERP();

                    //2. Registro Asignación de Códigos de Barras                    
                    await CodigoBarra.RegistrarAsignacionCodigoBarra(producto.IdProducto);
                    //3. Registro de Asignación de Categorías de Producto
                    await Categoria.RegistrarAsignacion(producto.IdProducto);
                    //venta
                    await Venta.RealizarRegistroVenta(producto);                   

                    break;


                case 2:
                    Console.WriteLine("===== ELIJA EL PRODUCTO PARA ASIGNAR CODIGO DE BARRA =====");
                    var producto2 = await ProductoErp.SeleccionarProductoPorIdAsync();
                    if(producto2!=null && producto2.IdProducto >0)
                        await CodigoBarra.RegistrarAsignacionCodigoBarra(producto2.IdProducto);
                    break;

                case 3:
                    Console.WriteLine("===== ELIJA EL PRODUCTO PARA ASIGNAR CATEGORIA =====");
                    var producto3 = await ProductoErp.SeleccionarProductoPorIdAsync();
                    if (producto3 != null && producto3.IdProducto > 0)
                        await Categoria.RegistrarAsignacion(producto3.IdProducto);
                    break;

                case 4:
                    Console.WriteLine("===== ELIJA EL PRODUCTO PARA LA VENTA =====");
                    var productoSelec =await ProductoErp.SeleccionarProductoPorIdAsync();
                    if (productoSelec != null && productoSelec.IdProducto > 0)
                        await Venta.RealizarRegistroVenta(productoSelec);
                    break;
                case 0:
                    Console.WriteLine("Saliendo del programa.");
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                    break;
            }
            if (continuar)
            {
                Console.WriteLine("\n¿Deseas continuar con otro proceso? (Sí/No)");
                string respuesta = Console.ReadLine().Trim().ToUpper();

                if (respuesta != "SI" && respuesta != "SÍ")
                {
                    continuar = false;
                }
            }

        }
    }

}