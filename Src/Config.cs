namespace pnp_exportar_mvc_csv.Src;

public class ConfigEnv
{
    public static string Path;
    public static string PathService;
    public static string Port;

    static ConfigEnv() {
        Path = @"C:\Users\ricar\Documents\work\programming\csharp\Extrator\pnp-exportar-csv\";
        PathService = @"C:\Users\ricar\Microsoft\Power BI Desktop Store App\AnalysisServicesWorkspaces";
        Port = "59936";
    }
}