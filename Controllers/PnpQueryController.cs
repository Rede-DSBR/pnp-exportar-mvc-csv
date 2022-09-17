using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pnp_exportar_mvc_csv.Models;
using System.Collections;
using QueryTool;
using System.Web;
using Microsoft.AnalysisServices.Tabular;

using pnp_exportar_mvc_csv.Src;

namespace pnp_exportar_mvc_csv.Controllers;

public class PnpQueryController : Controller
{
    
    public string colunasTabela { get; set; } = "";

    public string linhasTabela { get; set; } = "";

    
    public string[] tempDimensao { get; set; } 

    public string[] tempFato { get; set; } 

    private readonly ILogger<HomeController> _logger;

    public PnpQueryController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
   


   
 
    [HttpGet]
    public IActionResult Index(string[] ano, string query,string titulo)
  {

       //Console.WriteLine("xxx"+ano.Length);
        String[] sTitulo = titulo.Split(";");
        ViewBag.Titulo = sTitulo[0];
        ViewBag.subTitulo = sTitulo[1];
        FacadeCargaDax facadeCargaDax = new FacadeCargaDax(ConfigEnv.Port, ConfigEnv.PathService);


            TableCollection tables = facadeCargaDax.daoPowerBI.getDataModel();
            TableHandler tableParser = new TableHandler(tables);
            //tableParser.DeleteFileIfExists();
            // // tableParser.GenerateDocument();
            // String destId = tableParser.GenerateCustomTable(@"wwwroot\PnpQuery\/", dic1);
            string destId = facadeCargaDax.daoPowerBI.GenerateDataDictionary(query, @"wwwroot\PnpQuery\/", sTitulo[0]);

        HashSet<string> listaAnos = new HashSet<string>();
        for (int i= 0; i < ano.Length;i++){
            
            listaAnos.Add(ano[i]);
        


        }
            ArrayList listaPNP = facadeCargaDax.daoPowerBI.executeDaxQueryPNP(query, listaAnos);
                Console.WriteLine("count="+listaPNP.Count);

        string linhasTabela = montaLinhas(listaPNP,100);

        HashSet<string> atributos = facadeCargaDax.daoPowerBI.listaAtributosQueryDAX(query);
        string[] sAtributos = new string[atributos.Count];
        int count = 0;
        foreach (var item in atributos)
            {


            sAtributos[count] = item;
            count++;
            //Console.WriteLine(item);
            }



        string colunasTabela = montaColunas(sAtributos!)[0];
        //Console.WriteLine("entrei");

        // this.tempDimensao.CopyTo( Dimensao);
        //this.tempDimensao = Fato;




       
        ViewBag.Nome = "teste";
            var model = new IndexViewModel
	        {
            Tabela = new Tabela
            {
                colunas ="",
                linhas = ""
               
            }
	    };


        model.Tabela.colunas = colunasTabela;
        model.Tabela.linhas = linhasTabela;
        model.Tabela.Ano = ano;
        model.Tabela.Dimnesao = null;
        model.Tabela.Dimnesao = null;

       

       
         

        
            String colunas =    montaColunas(sAtributos!)[1];
              long ticks = DateTime.Now.Ticks;
              byte[] bytes = BitConverter.GetBytes(ticks);
              string idFile = Convert.ToBase64String(bytes)
                        .Replace('+', '_')
                        .Replace('/', '-')
                        .TrimEnd('=');

              String arquivo = "PNP" + idFile + ".txt";


        ViewBag.Arquivo = "/PnpQuery/"+arquivo;
        ViewBag.registros = listaPNP.Count;
        ViewBag.query = query;
        ViewBag.Dicionario = "/PnpQuery/"+destId;
        this.gravaCSV(arquivo,listaPNP,colunas);

       

        return View(model);

     //  HttpUtility.HtmlEncode("Dimen " + name + ", NumTimes is: " + numTimes);



     
  }  

 
   



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }






  public string   montaLinhas( ArrayList listaPNP,int numlinhas)
    {
       
       
       string Linhas="";
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
                if (numlinhas==cont){
                    return Linhas;
                }
            }

 
          //  Console.WriteLine("Linhas" + this.Linhas);
        }
        catch (Exception e)
        {

        }
        return Linhas;

    }


    
    public string[] montaColunas(string[] Atributos)
    {

        string[] Colunas = new string[2];
        int c = 0;
        for(int i=0;i<Atributos.Length;i++)
        {

            Colunas[0] = Colunas[0] + "{text:'" + Atributos[i].Trim().Replace("\'","") + "', align:'start',sortable: false, value: 'c" + c + "',width:'1%'},";
            Colunas[1] = Colunas[1] + Atributos[i].Trim().Replace("\'", "") + ";";
            c = c + 1;
        }
        Console.WriteLine(Colunas);
        
        return Colunas;

        //   Console.WriteLine("Colunas" + this.Colunas);


    }



        public async void gravaCSV(string arquivo, ArrayList listaPNP,String colunas){
            string nameFile = ConfigEnv.Path;
            //string nameFile = "C:\\pnp\\pnp-exportar-mvc-csv\\wwwroot\\PnpQuery\\";
            
            
            
             colunas  = colunas.ToString().Substring(0,colunas.Length-1);

             string filter = "";


           
                     using StreamWriter file = new(nameFile +arquivo);
      
                    await file.WriteLineAsync(colunas);
                    int cont=0;

               
                                       
                    int c=0;


                        foreach (string line in listaPNP)
                           {
                    //  Console.WriteLine("gravei");
                    

                            await file.WriteLineAsync(line);

                            c=c+1;
                            
                            }

   



}



}