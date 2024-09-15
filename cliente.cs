public class Cliente{
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferenciaDireccion;

    public Cliente(string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }

    public void mostrarDireccion(){
        Console.Write($"Direccion del cliente: {direccion}");
        Console.WriteLine($"Referencia: {datosReferenciaDireccion}");
    }

    public void mostrarDatos(){
        Console.WriteLine($"Nombre: {nombre}");
        Console.WriteLine($"Telefono: {telefono}");
    }

    
}