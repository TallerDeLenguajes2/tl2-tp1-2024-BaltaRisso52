public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> listaPedidos;

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.listaPedidos = new List<Pedido>();
    }

    public List<Pedido> ListaPedidos { get => listaPedidos; }
    public int Id { get => id; }
    public string Nombre { get => nombre; }


    public int JornalACobrar()
    {
        return listaPedidos.Count(p => p.Estado == Estado.Entregado) * 500;
    }

    public void AgregarPedido(Pedido pedido)
    {
        listaPedidos.Add(pedido);
        pedido.CambiarEstado(Estado.enCamino);
        Console.WriteLine("Pedido agregado exitosamente.");
    }

    public void EliminarPedido(Pedido pedido)
    {
        if (listaPedidos.Remove(pedido))
        {
            Console.WriteLine("Pedido eliminado exitosamente.");
        }
        else
        {
            Console.WriteLine("El pedido no se encuentra en la lista.");
        }
    }


}