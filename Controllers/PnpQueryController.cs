using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pnp_exportar_mvc_csv.Models;
using System.Collections;
using QueryTool;
using Microsoft.AnalysisServices.Tabular;

using System.Text;

using pnp_exportar_mvc_csv.Src;

namespace pnp_exportar_mvc_csv.Controllers;

public class PnpQueryController : Controller
{

    public string colunasTabela { get; set; } = "";
    public string linhasTabela { get; set; } = "";
    private readonly ILogger<HomeController> _logger;
    Assemble _assemble;
    Save _save;
    Ticks _ticks;
    Attributes _attribute;
    Year _year;
    DaoModel _daoModel;

    public PnpQueryController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _assemble = new Assemble();
        _save = new Save();
        _ticks = new Ticks();
        _attribute = new Attributes();
        _year = new Year();
        _daoModel = new DaoModel();

    }

    [HttpGet]
    public IActionResult Index(string[] ano, string query, string titulo)
    {
        String[] sTitulo = titulo.Split(";");
        ViewBag.Titulo = sTitulo[0];
        ViewBag.subTitulo = sTitulo[1];
        FacadeCargaDax facadeCargaDax = new FacadeCargaDax(ConfigEnv.Port, ConfigEnv.PathService);


        TableCollection tables = facadeCargaDax.daoPowerBI.getDataModel();
        TableHandler tableParser = new TableHandler(tables);
        string destId = facadeCargaDax.daoPowerBI.GenerateDataDictionary(query, @"wwwroot\PnpQuery\/", sTitulo[0]);

        HashSet<string> listaAnos = _year.mapYears(ano);

        ArrayList listaPNP = facadeCargaDax.daoPowerBI.executeDaxQueryPNP(query, listaAnos);
        Console.WriteLine("count=" + listaPNP.Count);

        string linhasTabela = _assemble.AssemblingBodyLines(listaPNP, 100);

        HashSet<string> atributos = facadeCargaDax.daoPowerBI.listaAtributosQueryDAX(query);

        string[] sAtributos = _attribute.setAttributes(atributos);

        string colunasTabela = _assemble.AssemblingHeaderColumns(sAtributos!)[0];

        ViewBag.Nome = "teste";
        var model = _daoModel.setModel(colunasTabela, linhasTabela, ano);
        
        String colunas = _assemble.AssemblingHeaderColumns(sAtributos!)[1];
   
        string idFile = _ticks.getId();

        String arquivo = "PNP" + idFile + ".txt";

        ViewBag.Arquivo = "/PnpQuery/" + arquivo;
        ViewBag.registros = listaPNP.Count;
        ViewBag.query = query;
        ViewBag.Dicionario = "/PnpQuery/" + destId;
        _save.writeCSV(arquivo, listaPNP, colunas);

        return View(model);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}