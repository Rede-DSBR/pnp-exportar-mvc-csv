namespace pnp_exportar_mvc_csv.Src;

public class Year
{
    public HashSet<string> mapYears(string[] years)
    {
        HashSet<string> listaAnos = new HashSet<string>();
        for (int i = 0; i < years.Length; i++)
        {

            listaAnos.Add(years[i]);
        }

        return listaAnos;
    }
}