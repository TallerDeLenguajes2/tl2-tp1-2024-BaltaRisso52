string opcion;

Cadeteria cadeteriaNueva = cargarDatos.cargarCadeteria("Datos/Cadeteria.csv");

if (cadeteriaNueva != null)
{
    bool salir = true;
    while (salir)
    {
        //Console.Clear();
        Console.WriteLine("-----MENU-----");
        Console.WriteLine("1.Dar de alta pedidos");
        Console.WriteLine("2.Asignar pedido a cadete");
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
                cadeteriaNueva.asignarPedido();
                break;

            case "3":
                Console.Write("Ingrese el numero del pedido: ");
                int nroPedido = Convert.ToInt32(Console.ReadLine());
                cadeteriaNueva.cambiarEstadoPedido(nroPedido);
                break;

            case "4":
                Console.Write("Ingrese el numero del pedido: ");
                int nro = Convert.ToInt32(Console.ReadLine());
                cadeteriaNueva.reAsignarPedido(nro);
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







