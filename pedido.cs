using System.ComponentModel;

public enum Estado
{
    Preparacion,
    enCamino,
    Entregado,
    Cancelado
}

public class  Pedido{
    private static int contador = 0;
    private int nro;
    private string obs;
    private Cliente cliente;
    private Estado estado;

    public Pedido(string obs, string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        this.nro = contador++;
        this.obs = obs;
        this.cliente = new Cliente(nombre, direccion, telefono, datosReferenciaDireccion);
        this.estado = Estado.Preparacion;
    }

    public Estado Estado { get => estado;}
    public int Nro { get => nro;}

    
    public void VerDireccionCliente(){
        cliente.mostrarDireccion();
    }

    public void VerDatosCliente(){
        cliente.mostrarDatos();
    }

    public void CambiarEstado(Estado estado){
        this.estado = estado;
    }
}