using System.Text.Json.Serialization;
public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> listaCadetes;
    private List<Pedido> listaPedidos;


    [JsonConstructor]
    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listaCadetes = new List<Cadete>();
        this.listaPedidos = new List<Pedido>();
    }

    

    public Cadeteria(string nombre, string telefono, List<Cadete> lista)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listaCadetes = lista;
        this.listaPedidos = new List<Pedido>();
    }

    [JsonPropertyName("nombre")]
    public string Nombre {get=> nombre; set=> nombre = value;}
    [JsonPropertyName("telefono")]
    public string Telefono {get=> telefono; set=> telefono = value;}



    public List<Pedido> ListaPedidos { get => listaPedidos; }

    public void agregarCadetes(List<Cadete> cadetes){
        this.listaCadetes = cadetes;
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

    public int JornalACobrar(int id)
    {
        return listaPedidos.Count(p => p.Estado == Estado.Entregado && p.Cadete.Id == id) * 500;
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

        listaPedidos.Add(pedido);

    }

    public void informeDelDia()
    {

        int pedidosRecibidos = listaPedidos.Count;
        int pedidosEntregados = listaPedidos.Count(p => p.Estado == Estado.Entregado);

        Console.WriteLine($"Cantidad de pedidos recibidos: {pedidosRecibidos}");
        Console.WriteLine($"Cantidad de pedidos entregados: {pedidosEntregados}");

        int contador = 1;
        int totalCobrarCadetes = 0;

        Console.WriteLine("Monto ganado y la cantidad de envíos de cada cadete: ");
        foreach (var cadete in listaCadetes)
        {
            Console.WriteLine($"---{contador}.{cadete.Nombre}---");
            int monto = JornalACobrar(cadete.Id);
            Console.WriteLine($"Monto: {monto}");
            int envios = monto / 500;
            Console.WriteLine($"Cantidad de envios: {envios}");

            contador++;
            totalCobrarCadetes += monto;

        }

        int promedioEnvios = pedidosEntregados / listaCadetes.Count;

        Console.WriteLine("----------");
        Console.WriteLine($"Monto total: {totalCobrarCadetes}");
        Console.WriteLine($"Promedio de envios por cadete: {promedioEnvios}");

    }

    public void asignarCadeteAPedido(int idCadete, int nroPedido)
    {
        Cadete cadete = listaCadetes.FirstOrDefault(c => c.Id == idCadete);
        Pedido pedido = listaPedidos.FirstOrDefault(p => p.Nro == nroPedido && p.Cadete is null && p.Estado == Estado.Preparacion);

        if (cadete is not null && pedido is not null)
        {
            pedido.agregarCadete(cadete);
            pedido.CambiarEstado(Estado.enCamino);
            Console.WriteLine("Cadete asignado a Pedido correctamente");
        }
        else
        {
            Console.WriteLine("Hubo un error en la entrada.");
        }


    }

    public void reAsignarPedido(int nroPedido, int idCadete)
    {
        Pedido pedido = listaPedidos.FirstOrDefault(p => p.Nro == nroPedido && p.Cadete is not null && p.Estado == Estado.enCamino);
        Cadete cadete = listaCadetes.FirstOrDefault(c => c.Id == idCadete);

        if (cadete is not null && pedido is not null)
        {
            pedido.agregarCadete(cadete);
            Console.WriteLine("Cadete reasignado a Pedido correctamente");
        }
        else
        {
            Console.WriteLine("Hubo un error en la entrada.");
        }

    }
    public void cambiarEstadoPedido(int nro)
    {
        Pedido pedido = listaPedidos.FirstOrDefault(p => p.Nro == nro);

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
                    Console.WriteLine("Estado cambiado correctamente.");
                    break;

                case "2":
                    pedido.CambiarEstado(Estado.Cancelado);
                    if (listaPedidos.Remove(pedido))
                    {
                        Console.WriteLine("El pedido fue eliminado de la lista de pedidos.");
                    }
                    else
                    {
                        Console.WriteLine("Hubo un error");
                    }
                    break;

                default:
                    Console.WriteLine("Respuesta no valida.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Pedido no encontrado.");
        }

    }

    public void listarCadetes()
    {
        Console.WriteLine("Lista de cadetes: ");
        foreach (var cadete in listaCadetes)
        {
            Console.WriteLine($"{cadete.Id}. {cadete.Nombre}");
        }
    }

    public void listarPedidos(Estado estado)
    {
        Console.WriteLine("Lista de pedidos: ");
        foreach (var pedido in listaPedidos)
        {
            if (pedido.Estado == estado)
            {
                Console.WriteLine($"Pedido numero: {pedido.Nro}.");
            }

        }
    }
}