using CargaDatos;

string opcion;

ICargarDatos acceso;
Cadeteria cadeteriaNueva = null;
List<Cadete> cadetes = null;

Console.WriteLine("--- Seleccionar Tipo ---");
Console.WriteLine("1.CSV");
Console.WriteLine("2.JSON");
Console.Write("Respuesta: ");
string opcionDato = Console.ReadLine();

switch (opcionDato)
{
    case "1":
        acceso = new ArchivoCsv();
        cadeteriaNueva = acceso.cargarCadeteria("Datos/Cadeteria.csv");
        break;
    case "2":
        acceso = new ArchivoJson();
        cadeteriaNueva = acceso.cargarCadeteria("Datos/Cadeteria.json");
        cadetes = acceso.cargarCadetes("Datos/cadetes.json");
        cadeteriaNueva.agregarCadetes(cadetes);
        break;
    default:
        cadeteriaNueva = null;
        break;
}

if (cadeteriaNueva != null && cadetes != null)
{
    bool salir = true;
    while (salir)
    {
        Console.WriteLine("-----MENU-----");
        Console.WriteLine("1.Dar de alta pedidos");
        Console.WriteLine("2.Asignar cadete a pedido");
        Console.WriteLine("3.Cambiar de estado un pedido");
        Console.WriteLine("4.Reasignar pedido a otro cadete");
        Console.WriteLine("5.Informe");
        Console.WriteLine("6.salir");
        Console.Write("Ingrese su respuesta: ");
        opcion = Console.ReadLine();
        switch (opcion)
        {
            case "1":
                cadeteriaNueva.darAltaPedido();
                break;

            case "2":

                if (cadeteriaNueva.ListaPedidos.Any(p => p.Estado == Estado.Preparacion))
                {
                    cadeteriaNueva.listarPedidos(Estado.Preparacion);
                    cadeteriaNueva.listarCadetes();
                    Console.Write("Ingrese el numero de pedido: ");
                    int nroPedido = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Ingrese el ID del cadete: ");
                    int cadeteId = Convert.ToInt32(Console.ReadLine());
                    cadeteriaNueva.asignarCadeteAPedido(cadeteId, nroPedido);
                }
                else
                {
                    Console.WriteLine("No hay pedidos nuevos.");
                }

                break;

            case "3":
                Console.Write("Ingrese el numero del pedido: ");
                int nroPedido2 = Convert.ToInt32(Console.ReadLine());
                cadeteriaNueva.cambiarEstadoPedido(nroPedido2);
                break;

            case "4":
                cadeteriaNueva.listarPedidos(Estado.enCamino);
                cadeteriaNueva.listarCadetes();
                Console.Write("Ingrese el numero del pedido: ");
                int nroPedido3 = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese el ID del cadete: ");
                int cadeteId2 = Convert.ToInt32(Console.ReadLine());
                cadeteriaNueva.reAsignarPedido(nroPedido3, cadeteId2);
                break;

            case "5":
                cadeteriaNueva.informeDelDia();
                break;

            case "6":
                salir = false;
                Console.WriteLine("CHAU, GRACIAS.");
                break;


            default:
                Console.WriteLine("Ingrese una opcion correcta.");
                break;
        }
    }
}
else
{
    Console.WriteLine("Hubo un error con la carga de datos");
}







