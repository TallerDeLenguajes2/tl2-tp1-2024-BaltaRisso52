public class validarEntrada
{
    public static string leerEntradaConValidacion(string msj, string error)
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
}