using CargaDatos;
public class ArchivoCsv : ICargarDatos
{
    public Cadeteria cargarCadeteria(string archivo)
    {
        if (!File.Exists(archivo))
        {
            Console.WriteLine("El archivo no existe");
            return null;
        }

        var lineas = File.ReadAllLines(archivo);

        if (lineas.Length == 0 || lineas.Length == 1 || lineas.All(linea => string.IsNullOrWhiteSpace(linea)))
        {
            Console.WriteLine("El archivo está vacío, solo contiene líneas en blanco o solo contiene el encabezado");
            return null;
        }


        var linea = lineas.Skip(1).FirstOrDefault();
        var campo = linea.Split(',');
        if (campo.Length != 2)
        {
            Console.WriteLine($"La linea no tiene el formato esperado: {linea}");
            return null;
        }

        List<Cadete> cadetes = cargarCadetes("Datos/cadetes.csv");
        if (cadetes.Count == 0)
        {
            Console.WriteLine($"Error con la carga de los cadetes");
            return null;
        }

        Cadeteria cadeteriaNueva = new Cadeteria(campo[0], campo[1], cadetes);

        return cadeteriaNueva;
    }

    public List<Cadete> cargarCadetes(string archivo)
    {
        List<Cadete> cadetes = new List<Cadete>();

        if (!File.Exists(archivo))
        {
            Console.WriteLine("El archivo no existe");
            return cadetes;
        }

        var lineas = File.ReadAllLines(archivo);

        if (lineas.Length == 0 || lineas.Length == 1 || lineas.All(linea => string.IsNullOrWhiteSpace(linea)))
        {
            Console.WriteLine("El archivo está vacío, solo contiene líneas en blanco o solo contiene el encabezado");
            return cadetes;
        }

        foreach (var linea in lineas.Skip(1))
        {
            var campos = linea.Split(',');

            if (campos.Length != 4)
            {
                Console.WriteLine($"La linea no tiene el formato esperado: {linea}");
                continue;
            }

            Cadete cadeteNuevo = new Cadete(int.Parse(campos[0]), campos[1], campos[2], campos[3]);
            cadetes.Add(cadeteNuevo);

        }

        return cadetes;
    }
}