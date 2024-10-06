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
        cadeteriaNueva.ListaCadetes = cadetes;
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
                Console.WriteLine("-----ALTA DE PEDIDO-----");

                string nombre = validarEntrada.leerEntradaConValidacion("Ingrese el nombre del cliente: ", "El nombre no puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

                string telefono = validarEntrada.leerEntradaConValidacion("Ingrese el telefono del cliente: ", "El telefono no puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

                string direccion = validarEntrada.leerEntradaConValidacion("Ingrese la direccion del cliente: ", "La direccion no puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

                string referenciaDireccion = validarEntrada.leerEntradaConValidacion("Ingrese referencia de la direccion del cliente: ", "No puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

                string observacion = validarEntrada.leerEntradaConValidacion("Observacion: ", "No puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

                cadeteriaNueva.darAltaPedido(observacion,nombre,direccion,telefono,referenciaDireccion);
                break;

            case "2":

                if (cadeteriaNueva.ListaPedidos.Any(p => p.Estado == Estado.Preparacion))
                {
                    Console.WriteLine(cadeteriaNueva.listarPedidos(Estado.Preparacion));
                    
                    Console.WriteLine(cadeteriaNueva.listarCadetes());
                    Console.Write("Ingrese el numero de pedido: ");
                    int nroPedido = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Ingrese el ID del cadete: ");
                    int cadeteId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(cadeteriaNueva.asignarCadeteAPedido(cadeteId, nroPedido));
                }
                else
                {
                    Console.WriteLine("No hay pedidos nuevos.");
                }

                break;

            case "3":
                Console.Write("Ingrese el numero del pedido: ");
                int nroPedido2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Cambiar estado del pedido a: ");
                Console.WriteLine("1.Entregado");
                Console.WriteLine("2.Cancelado");
                Console.Write("Respuesta: ");
                string estado = Console.ReadLine();
                Console.WriteLine(cadeteriaNueva.cambiarEstadoPedido(nroPedido2, estado));
                break;

            case "4":
                Console.WriteLine(cadeteriaNueva.listarPedidos(Estado.Preparacion));
                Console.WriteLine(cadeteriaNueva.listarCadetes());
                Console.Write("Ingrese el numero del pedido: ");
                int nroPedido3 = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese el ID del cadete: ");
                int cadeteId2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(cadeteriaNueva.reAsignarPedido(nroPedido3, cadeteId2));
                break;

            case "5":
                string informe = cadeteriaNueva.informeDelDia();
                Console.WriteLine(informe);
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







