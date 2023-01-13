using System.Collections;

namespace pnp_exportar_mvc_csv.Src;

public class Save
{
    public async void writeCSV(string arquivo, ArrayList listaPNP, String colunas)
    {
        string nameFile = "C:\\pnp\\pnp-exportar-mvc-csv\\wwwroot\\PnpQuery\\";
        colunas = colunas.ToString().Substring(0, colunas.Length - 1);
        using StreamWriter file = new(nameFile + arquivo);
        await file.WriteLineAsync(colunas);
        int c = 0;

        foreach (string line in listaPNP)
        {
            await file.WriteLineAsync(line);
            c = c + 1;
        }
    }
}