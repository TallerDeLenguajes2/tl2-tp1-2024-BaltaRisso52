public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listaCadetes;
    private List<Pedido> listaPedidosNuevos;

    public Cadeteria(string nombre, string telefono, List<Cadete> lista)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listaCadetes = lista;
        this.listaPedidosNuevos = new List<Pedido>();
    }

    public string leerEntradaConValidacion(string msj, string error)
    {

        string entrada;
        do
        {

            Console.Write(msj);
            entrada = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(entrada))
            {
                Console.WriteLine(error);
            }
        } while (string.IsNullOrWhiteSpace(entrada));

        return entrada;

    }

    public void darAltaPedido()
    {
        Console.WriteLine("-----ALTA DE PEDIDO-----");

        string nombre = leerEntradaConValidacion("Ingrese el nombre del cliente: ", "El nombre no puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

        string telefono = leerEntradaConValidacion("Ingrese el telefono del cliente: ", "El telefono no puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

        string direccion = leerEntradaConValidacion("Ingrese la direccion del cliente: ", "La direccion no puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

        string referenciaDireccion = leerEntradaConValidacion("Ingrese referencia de la direccion del cliente: ", "No puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

        string observacion = leerEntradaConValidacion("Observacion: ", "No puede estar vacío o contener solo espacios. Inténtelo de nuevo.");

        Pedido pedido = new Pedido(observacion, nombre, direccion, telefono, referenciaDireccion);

        listaPedidosNuevos.Add(pedido);

    }

    public void informeDelDia()
    {

        int pedidosRecibidos = listaCadetes.Sum(cadete => cadete.ListaPedidos.Count) + listaPedidosNuevos.Count;
        int pedidosEntregados = listaCadetes.Sum(pedidos => pedidos.ListaPedidos.Count(pedido => pedido.Estado == Estado.Entregado));

        Console.WriteLine($"Cantidad de pedidos recibidos: {pedidosRecibidos}");
        Console.WriteLine($"Cantidad de pedidos entregados: {pedidosEntregados}");

        int contador = 1;
        int totalCobrarCadetes = 0;
        int cantTotalEnvios = 0;
        Console.WriteLine("Monto ganado y la cantidad de envíos de cada cadete: ");
        foreach (var cadete in listaCadetes)
        {
            Console.WriteLine($"---{contador}.{cadete.Nombre}---");
            Console.WriteLine($"Monto: {cadete.JornalACobrar()}");
            int envios = cadete.ListaPedidos.Count(p => p.Estado == Estado.Entregado);
            Console.WriteLine($"Cantidad de envios: {envios}");

            contador++;
            totalCobrarCadetes += cadete.JornalACobrar();
            cantTotalEnvios += envios;
        }

        int promedioEnvios = cantTotalEnvios / listaCadetes.Count;


        Console.WriteLine("----------");
        Console.WriteLine($"Monto total: {totalCobrarCadetes}");
        Console.WriteLine($"Promedio de envios por cadete: {promedioEnvios}");

    }

    public void asignarPedido()
    {

        Console.WriteLine("Lista de pedidos no asignados: ");
        if (listaPedidosNuevos.Count == 0)
        {
            Console.WriteLine("No hay pedidos");
        }
        else
        {
            int cantPedidos = 1;
            foreach (var pedido in listaPedidosNuevos)
            {
                Console.WriteLine($"{cantPedidos}. Pedido numero: {pedido.Nro}");
                cantPedidos++;
            }

            Console.WriteLine("Lista de cadetes: ");
            int cantCadetes = 1;
            foreach (var cadete in listaCadetes)
            {
                Console.WriteLine($"{cantCadetes}. {cadete.Nombre} ID: {cadete.Id}");
                cantCadetes++;
            }

            int asignarPedido;
            do
            {
                Console.Write("Ingrese el numero de pedido que desea asignar: ");
                asignarPedido = Convert.ToInt32(Console.ReadLine());
            } while (!listaPedidosNuevos.Any(pedido => pedido.Nro == asignarPedido));

            int asignarCadete;
            do
            {
                Console.Write("Ingrese el ID del cadete a asignar el pedido: ");
                asignarCadete = Convert.ToInt32(Console.ReadLine());
            } while (!listaCadetes.Any(cadete => cadete.Id == asignarCadete));

            Cadete cadete1 = listaCadetes.First(c => c.Id == asignarCadete);
            Pedido pedidoNuevo = listaPedidosNuevos.First(p => p.Nro == asignarPedido);

            cadete1.AgregarPedido(pedidoNuevo);
            listaPedidosNuevos.Remove(pedidoNuevo);

        }



    }

    public void reAsignarPedido(int nro)
    {
        Cadete cadeteAsignado = listaCadetes.FirstOrDefault(c => c.ListaPedidos.Any(p => p.Nro == nro));

        if (cadeteAsignado != null)
        {
            Console.WriteLine("Lista de cadetes: ");
            int cantCadetes = 1;
            foreach (var cadete in listaCadetes)
            {
                Console.WriteLine($"{cantCadetes}. {cadete.Nombre} ID: {cadete.Id}");
                cantCadetes++;
            }

            int asignarCadete;
            do
            {
                Console.Write("Ingrese el ID del cadete a asignar el pedido: ");
                asignarCadete = Convert.ToInt32(Console.ReadLine());
            } while (!listaCadetes.Any(cadete => cadete.Id == asignarCadete));

            Cadete cadeteNuevo = listaCadetes.FirstOrDefault(c => c.Id == asignarCadete);


            Pedido pedidoReAsignado = cadeteAsignado.ListaPedidos.FirstOrDefault(p => p.Nro == nro);

            cadeteNuevo.AgregarPedido(pedidoReAsignado);

            cadeteAsignado.EliminarPedido(pedidoReAsignado);

        }
        else
        {
            Console.WriteLine($@"El pedido nro ""{nro}"" no fue encontrado.");
        }
    }
    public void cambiarEstadoPedido(int nro)
    {
        Pedido pedido = (listaCadetes.SelectMany(c => c.ListaPedidos).FirstOrDefault(p => p.Nro == nro)) ?? listaPedidosNuevos.FirstOrDefault(p => p.Nro == nro);

        if (pedido is not null)
        {
            Console.WriteLine("Cambiar estado del pedido a: ");
            Console.WriteLine("1.Entregado");
            Console.WriteLine("2.Cancelado");
            Console.Write("Respuesta: ");

            string respuesta = Console.ReadLine();

            switch (respuesta)
            {
                case "1":
                    pedido.CambiarEstado(Estado.Entregado);
                    break;

                case "2":
                    pedido.CambiarEstado(Estado.Cancelado);
                    if (listaPedidosNuevos.Remove(pedido))
                    {
                        Console.WriteLine("El pedido fue eliminado de la lista de nuevos pedidos.");
                    }else
                    {
                        Cadete cadete = listaCadetes.FirstOrDefault(c => c.ListaPedidos.Contains(pedido));
                        cadete.ListaPedidos.Remove(pedido);
                        Console.WriteLine("El pedido fue eliminado de la lista de pedidos del cadete.");
                    }
                    break;

                default:
                    Console.WriteLine("Respuesta no valida.");
                    break;
            }
        }else
        {
            Console.WriteLine("Pedido no encontrado.");
        }




    }
}