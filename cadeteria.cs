using System.Text;
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
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("telefono")]
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Pedido> ListaPedidos { get => listaPedidos; }
    public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }


    

    public int JornalACobrar(int id)
    {
        return listaPedidos.Count(p => p.Estado == Estado.Entregado && p.Cadete.Id == id) * 500;
    }

    public void darAltaPedido(string obs, string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {

        Pedido pedido = new Pedido(obs, nombre, direccion, telefono, datosReferenciaDireccion);

        listaPedidos.Add(pedido);

    }

    public string informeDelDia()
    {

        int pedidosRecibidos = listaPedidos.Count;
        int pedidosEntregados = listaPedidos.Count(p => p.Estado == Estado.Entregado);

        StringBuilder cadena = new StringBuilder();

        cadena.AppendLine($"Cantidad de pedidos recibidos: {pedidosRecibidos}");
        cadena.AppendLine($"Cantidad de pedidos entregados: {pedidosEntregados}");

        int contador = 1;
        int totalCobrarCadetes = 0;

        cadena.AppendLine("Monto ganado y la cantidad de envíos de cada cadete: ");
        foreach (var cadete in listaCadetes)
        {
            cadena.AppendLine($"---{contador}.{cadete.Nombre}---");
            int monto = JornalACobrar(cadete.Id);
            cadena.AppendLine($"Monto: {monto}");
            int envios = monto / 500;
            cadena.AppendLine($"Cantidad de envios: {envios}");

            contador++;
            totalCobrarCadetes += monto;

        }

        int promedioEnvios = listaCadetes.Count > 0 ? pedidosEntregados / listaCadetes.Count : 0;

        cadena.AppendLine("----------");
        cadena.AppendLine($"Monto total: {totalCobrarCadetes}");
        cadena.AppendLine($"Promedio de envios por cadete: {promedioEnvios}");

        return cadena.ToString();

    }

    public string asignarCadeteAPedido(int idCadete, int nroPedido)
    {
        Cadete cadete = listaCadetes.FirstOrDefault(c => c.Id == idCadete);
        Pedido pedido = listaPedidos.FirstOrDefault(p => p.Nro == nroPedido && p.Cadete is null && p.Estado == Estado.Preparacion);

        if (cadete is not null && pedido is not null)
        {
            pedido.agregarCadete(cadete);
            pedido.CambiarEstado(Estado.enCamino);
            return "Cadete asignado a Pedido correctamente";
        }
        else
        {
            return "Hubo un error en la entrada.";
        }


    }

    public string reAsignarPedido(int nroPedido, int idCadete)
    {
        Pedido pedido = listaPedidos.FirstOrDefault(p => p.Nro == nroPedido && p.Cadete is not null && p.Estado == Estado.enCamino);
        Cadete cadete = listaCadetes.FirstOrDefault(c => c.Id == idCadete);

        if (cadete is not null && pedido is not null)
        {
            pedido.agregarCadete(cadete);
            return "Cadete reasignado a Pedido correctamente";
        }
        else
        {
            return "Hubo un error en la entrada.";
        }

    }
    public string cambiarEstadoPedido(int nro, string estado)
    {
        Pedido pedido = listaPedidos.FirstOrDefault(p => p.Nro == nro);

        if (pedido is not null)
        {
            switch (estado)
            {
                case "1":
                    pedido.CambiarEstado(Estado.Entregado);
                    return "Estado cambiado correctamente.";

                case "2":
                    pedido.CambiarEstado(Estado.Cancelado);
                    if (listaPedidos.Remove(pedido))
                    {
                        return "El pedido fue eliminado de la lista de pedidos.";
                    }
                    else
                    {
                        return "Hubo un error al eliminar el pedido.";
                    }

                default:
                    return "Opcion no valida";
            }
        }
        else
        {
            return "Pedido no encontrado";
        }

    }

    public string listarCadetes()
    {
        StringBuilder cadena = new StringBuilder();
        cadena.AppendLine("Lista de cadetes: ");
        foreach (var cadete in listaCadetes)
        {
            cadena.AppendLine($"{cadete.Id}. {cadete.Nombre}");
        }

        return cadena.ToString();
    }

    public string listarPedidos(Estado estado)
    {
        StringBuilder cadena = new StringBuilder();
        cadena.AppendLine("Lista de pedidos: ");
        foreach (var pedido in listaPedidos)
        {
            if (pedido.Estado == estado)
            {
                cadena.AppendLine($"Pedido numero: {pedido.Nro}.");
            }

        }
        return cadena.ToString();
    }
}