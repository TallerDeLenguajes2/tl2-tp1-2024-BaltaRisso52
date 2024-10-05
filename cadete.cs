using System.Text.Json.Serialization;

public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;

    [JsonConstructor]
    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

    [JsonPropertyName("id")]
    public int Id { get => id; set => id = value;}
    [JsonPropertyName("nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("direccion")]
    public string Direccion { get => direccion; set => direccion = value; }
    [JsonPropertyName("telefono")]
    public string Telefono { get => telefono; set => telefono = value; }


}