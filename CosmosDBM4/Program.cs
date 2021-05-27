using System;
using CosmosDBM4.entities;
using Microsoft.Azure.Cosmos;
using CosmosDBM4.cosmos;
using System.Collections.Generic;
using System.Linq;

namespace CosmosDBM4
{
    class Program
    {
        static void Main(string[] args)
        {
            var cliente = DatabaseClient.Conex;

            var opc=Menu();
            while (opc != 6) {
                switch (opc) {
                    case 1:
                        Console.WriteLine("-.-.-.Todas las ventas-.-.-.");
                        GetAllVentas(cliente);
                        opc = Menu();
                        break;
                    case 2:
                        Console.WriteLine("-.-.-.Productos x email-.-.-.");
                        Console.WriteLine("email: ");
                        var email = Console.ReadLine();
                        GetProductsByEmail(email, cliente);
                        opc = Menu();
                        break;
                    case 3:
                        Console.WriteLine("-.-.-.Ventas x email-.-.-.-");
                        GetVentaByEmail(cliente);
                        opc = Menu();
                        break;
                    case 4:
                        Console.WriteLine("-.-.-.Añadir Venta-.-.-.-.");
                        addVenta(cliente);
                        opc = Menu();
                        break;
                    case 5:
                        Console.WriteLine("-.-.-.Borrar venta x correo-.-.-.");
                        DeleteVentaById(cliente);
                        opc = Menu();
                        break;
                    default:
                        Console.WriteLine("OPCIÓN INVÁLIDA!!!!!!");
                        opc = Menu();
                        break;


                
                }
            
            }


            
        }

        private static int Menu() {
            Console.WriteLine();
            Console.WriteLine("-.-.-.-.-.Menú.-.-.-.-.");
            Console.WriteLine(".-.-1. Todas las Ventas ");
            Console.WriteLine(".-.-2. Productos x email");
            Console.WriteLine(".-.-3. Venta x email");
            Console.WriteLine(".-.-4. Añadir Venta");
            Console.WriteLine(".-.-5. Borrar venta x email");
            Console.WriteLine(".-.-6. Salir ");
            Console.WriteLine("Selecciona opción: ");
            var opc = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return opc;
        
        }

        public  static void GetAllVentas(Database database) {
            var contenedor = database.GetContainer("ventas");
            var query = new QueryDefinition("select * from ventas p");
            var ventas = contenedor.GetItemQueryIterator<Venta>(query);
            while (ventas.HasMoreResults) {
                foreach (var item in ventas.ReadNextAsync().Result) {
                    Console.WriteLine(item);
                }
            }
        }

        public static void GetProductsByEmail(string email, Database database)
        {
            var contenedor = database.GetContainer("ventas");
            var productos = contenedor.GetItemLinqQueryable<Venta>(true)
                .AsEnumerable().Where(t => t.Email == email).Select(t => t.Productos);
        
            Console.WriteLine($"productos de {email}");
            foreach (var pr in productos) {
                foreach (var p in pr) {
                    Console.WriteLine(p);
                }
            }
            
        }

        public async static void addVenta(Database database) {
            Console.WriteLine("-.-.-.-.Añadiendo Venta Nueva-.-.-.-.-.");
            Console.Write("Email cliente: ");
            var email = Console.ReadLine();
            Console.WriteLine("Nombre cliente: ");
            var nombreC = Console.ReadLine();
            Console.WriteLine("Cuantos productos tiene la venta? ");
            var cantidad = int.Parse(Console.ReadLine());
            var lista = new List<Producto>();
            for (int i=0;i< cantidad; i++) {
                Console.WriteLine($"Nombre producto {i+1}: ");
                var nombre = Console.ReadLine();
                Console.WriteLine($"Precio producto {i+1}: ");
                var precio = double.Parse(Console.ReadLine());
                lista.Add(new Producto { 
                   Nombre=nombre, Precio=precio
                });
            }

            var contenedor = database.GetContainer("ventas");
            var resultado = await contenedor.CreateItemAsync(new Venta { 
                Id=Guid.NewGuid().ToString(),
                Email=email,
                Nombre=nombreC,
                Productos=lista
            });

            Console.WriteLine(resultado.StatusCode);
        }

        public static Venta GetVentaByEmail(Database database) {
            Console.WriteLine("-.-.-.-.Buscando venta x email-.-.-.-.");
            Console.WriteLine("Email usuario: ");
            var email = Console.ReadLine();
            var contenedor = database.GetContainer("ventas");
            var venta = contenedor.GetItemLinqQueryable<Venta>(true)
                .AsEnumerable().Where(t=>t.Email.Equals(email)).FirstOrDefault();
            Console.WriteLine(venta!=null?venta.ToString():"no existe");
            return venta;
        }

        public async static void DeleteVentaById (Database database) {
            Console.WriteLine("-.-.-.eliminando venta por correo-.-.-.");

            var contenedor = database.GetContainer("ventas");
            var venta = GetVentaByEmail(database);

            if (venta != null)
            {
                var resp = await contenedor.DeleteItemAsync<Venta>(venta.Id, new PartitionKey(venta.Email));
                Console.WriteLine("eliminado");
            }
            else {
                Console.WriteLine("no existe");
            }
        }
    }
}
