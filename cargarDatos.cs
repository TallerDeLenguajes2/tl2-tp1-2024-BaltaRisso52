namespace CargaDatos
{
    public interface ICargarDatos
    {
        Cadeteria cargarCadeteria(string archivo);

        List<Cadete> cargarCadetes(string archivo);
    }
}