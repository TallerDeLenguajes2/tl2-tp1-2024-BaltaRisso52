using System.Text.Json;
using System.Text.Json.Serialization;
using CargaDatos;

public class ArchivoJson : ICargarDatos
{
    public Cadeteria cargarCadeteria(string archivo)
    {
        if (!File.Exists(archivo))
        {
            Console.WriteLine("El archivo no existe");
            return null;
        }

        var Json = File.ReadAllText(archivo);

        if (!string.IsNullOrWhiteSpace(Json))
        {
            try
            {
                Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(Json);
                return cadeteria;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al deserializar el archivo JSON: {ex.Message}");
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public List<Cadete> cargarCadetes(string archivo)
    {
        if (!File.Exists(archivo))
        {
            Console.WriteLine("El archivo no existe");
            return null;
        }

        var Json = File.ReadAllText(archivo);

        if (!string.IsNullOrWhiteSpace(Json))
        {
            try
            {
                List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(Json);
                return cadetes;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al deserializar el archivo JSON: {ex.Message}");
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}