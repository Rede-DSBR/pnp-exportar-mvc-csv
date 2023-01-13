namespace pnp_exportar_mvc_csv.Src;

public class ConfigEnv
{
    public static string Path;
    public static string PathService;
    public static string Port;

    static ConfigEnv() {
        //string Path = "C:\\pnp\\pnp-exportar-mvc-csv\\wwwroot\\PnpQuery\\";
        Path = @"C:\pnp\pnp-exportar-csv\";
        PathService = @"C:\Users\Pesquisador\AppData\Local\Microsoft\Power BI Desktop\AnalysisServicesWorkspaces";
        Port = "58354";
    }
}