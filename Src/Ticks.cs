namespace pnp_exportar_mvc_csv.Src;

public class Ticks
{
    public string getId(){
        long ticks = DateTime.Now.Ticks;
        byte[] bytes = BitConverter.GetBytes(ticks);
        string idFile = Convert.ToBase64String(bytes)
                  .Replace('+', '_')
                  .Replace('/', '-')
                  .TrimEnd('=');
        return idFile;
    }
}