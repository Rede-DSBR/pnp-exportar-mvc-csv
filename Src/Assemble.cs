using System.Collections;

namespace pnp_exportar_mvc_csv.Src;

public class Assemble
{
    public string AssemblingBodyLines(ArrayList listaPNP, int numlinhas)
    {
        string Linhas = "";
        try
        {
            int cont = 0;
            foreach (var item in listaPNP)
            {
                string[] s = item.ToString().Split(";");
                Linhas = Linhas + "{";

                for (int i = 0; i < s.Length; i++)
                {
                    Linhas = Linhas + "c" + i + ":'" + s[i].Replace("\'", "") + "',";
                }
                Linhas = Linhas + "},";
                cont = cont + 1;
                if (numlinhas == cont)
                {
                    return Linhas;
                }
            }
        }
        catch (Exception e)
        {

        }
        return Linhas;

    }
    public string[] AssemblingHeaderColumns(string[] Atributos)
    {

        string[] Colunas = new string[2];
        int c = 0;
        for (int i = 0; i < Atributos.Length; i++)
        {

            Colunas[0] = Colunas[0] + "{text:'" + Atributos[i].Trim().Replace("\'", "") + "', align:'start', color: '#fff', class: 'backheader', sortable: false, value: 'c" + c + "',width:'1%'},";
            Colunas[1] = Colunas[1] + Atributos[i].Trim().Replace("\'", "") + ";";
            c = c + 1;
        }
        Console.WriteLine(Colunas);

        return Colunas;
    }
}