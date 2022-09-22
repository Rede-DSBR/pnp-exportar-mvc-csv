namespace pnp_exportar_mvc_csv.Src;

public class Attributes
{
    public string[] setAttributes(HashSet<string> attributes)
    {
        string[] sAtributos = new string[attributes.Count];
        int count = 0;
        foreach (var item in attributes)
        {
            sAtributos[count] = item;
            count++;
        }

        return sAtributos;
    }

}