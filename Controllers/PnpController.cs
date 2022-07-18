using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pnp_exportar_mvc_csv.Models;
using System.Collections;
using QueryTool;
using System.Web;

namespace pnp_exportar_mvc_csv.Controllers;

public class PnpController : Controller
{
    public string colunasTabela { get; set; } = "";

    public string linhasTabela { get; set; } = "";

    
    public string[] tempDimensao { get; set; } 

    public string[] tempFato { get; set; } 

    private readonly ILogger<HomeController> _logger;

    public PnpController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
   


     public string Welcome()
        {

             ViewBag.Nome = "teste";
      //  Console.WriteLine("teste");
        ViewBag.Nome = 10;


        return "View()";
        }

   
    [HttpGet]
    public IActionResult IndexPNP1(string[] Dimensao1)
    {  
           ViewBag.Nome = "teste";
           //  Console.WriteLine("xxx"+Dimensao1[0]);
          //    processsaDAX(this.tempDimensao, this.tempFato);
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
        return View(model);
 
    }
 [HttpGet]
  public IActionResult IndexPNP()
  {
     //Console.WriteLine("entrei");
      
        // this.tempDimensao.CopyTo( Dimensao);
        //this.tempDimensao = Fato;

        processsaDAX(null, null,null);

      
        ViewBag.Nome = "teste";
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

        model.Tabela.Dimnesao = null;
        model.Tabela.Dimnesao = null;

        return View(model);

     //  HttpUtility.HtmlEncode("Dimen " + name + ", NumTimes is: " + numTimes);



     
  }  

 [HttpPost]
  public IActionResult IndexPNP(string[] Dimensao, string[] Fato, string[] Ano, string acao)
  {



       

        if(acao.Equals("csv")){
            String dimensoesT = "";
            String fatosT = "";
            for (int i=0; i < Dimensao.Length;i++){

            dimensoesT = dimensoesT + Dimensao[i]+",";

        }
         for (int i=0; i < Fato.Length;i++){

            fatosT = fatosT + Fato[i]+",";

        }

         long ticks = DateTime.Now.Ticks;
            byte[] bytes = BitConverter.GetBytes(ticks);
            string idFile = Convert.ToBase64String(bytes)
                        .Replace('+', '_')
                        .Replace('/', '-')
                        .TrimEnd('=');

            String arquivo = "PNP" + idFile + ".txt";
            String colunas =   montaColunas(Dimensao,Fato)[1];
            Console.WriteLine(colunas);
            this.gravaCSV(arquivo,colunas,dimensoesT, fatosT,Ano);

            ViewBag.Arquivo = arquivo;

        }
     Console.WriteLine("entrei"+acao);
      
        // this.tempDimensao.CopyTo( Dimensao);
        //this.tempDimensao = Fato;
        if((Dimensao.Length)==0 || (Fato.Length)==0){
            processsaDAX(null, null,null);
        }else
        {

            processsaDAX(Dimensao, Fato,Ano);
        }


        ViewBag.Nome = "teste";
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

        model.Tabela.Dimnesao = Dimensao;
        model.Tabela.Fato = Fato;

        model.Tabela.Ano = Ano;

        return View(model);

     //  HttpUtility.HtmlEncode("Dimen " + name + ", NumTimes is: " + numTimes);



     
  }    

    public IActionResult IndexPNPSubmit(string[] Dimensao, string[] Fato,string[] Ano)
    {
        Console.WriteLine("entrei");
        processsaDAX(Dimensao, Fato,Ano);
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }



    public  void processsaDAX(string[] Dimensao_, string[] Fato_,string[] Ano_)
    {



        FacadeCargaDax facadeCargaDax = new FacadeCargaDax();
      
        string dimensoesT = "";
      

         string fatosT = "";


        if (Dimensao_ == null)
        {

            Dimensao_ = new string[7];

            Dimensao_[0] = "'dCalendário'[Ano]";
            Dimensao_[1] = "d_IES[Região]";
            Dimensao_[3] = "d_IES[UF]";
            Dimensao_[2] = "d_IES[Estado]";
            Dimensao_[3] = "d_IES[Organização Acadêmica]";
            Dimensao_[4] = "dimUnidade[Instituicao]";
            Dimensao_[5] = "d_IES[Instituição (Nome)]";
            Dimensao_[6] = "dimCurso[nomeCurso]";
           

            Fato_ = new string[6];
            Fato_[0] = "\"Número de cursos\", [Número de cursos]";
            Fato_[1] = "\"Número de concluintes\", [Número de concluintes]";
            Fato_[2] = "\"Número de ingressantes\", [Número de ingressantes]";
            Fato_[3] = "\"Número de inscritos\", [Número de inscritos]";
            Fato_[4] = "\"Número de Matrículas\", [Número de Matrículas]";
            Fato_[5] = "\"Número de vagas\", [Número de vagas]";
           

        }
        else
        {
            for (int i = 0; i < Dimensao_.Length; i++)
            {
                Dimensao_[i] = Dimensao_[i].Trim();

            }
        }



        string filter = "";

        if (Ano_ != null)
        {

            string anos = "";
            for (int i = 0; i < Ano_.Length; i++)
            {
                anos = anos + Ano_[i] + ",";


            }
            if (anos.Length != 0)
            {
                filter = "ALL( 'dCalendário'[Ano] ),'dCalendário'[Ano] IN {" + anos.Substring(0, anos.Length - 1) + "}";
            }

        }
        //filter = filter + "ALL( d_IES[UF] ),d_IES[UF] IN {\"BA\",\"PB\"}";

        

        for (int i=0; i < Dimensao_.Length;i++){

            dimensoesT = dimensoesT + Dimensao_[i]+",";

        }
         for (int i=0; i < Fato_.Length;i++){

            fatosT = fatosT + Fato_[i]+",";

        }

        String colunas = montaColunas(Dimensao_,Fato_)[0];
        Console.WriteLine(colunas);

        this.colunasTabela = colunas;


        try
        {
            fatosT = fatosT.ToString().Substring(0, fatosT.Length - 1);
            dimensoesT = dimensoesT.ToString().Substring(0, dimensoesT.Length - 1);
        }catch(Exception e){

        }




        ArrayList listaPNP =facadeCargaDax.executeDaxQueryDinamicPNP(dimensoesT, fatosT, filter, "", 1, 100);

        string linhas = montaLinhas(listaPNP);

        Console.WriteLine(linhas);

        this.linhasTabela = linhas;

        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Your Message');", true);
        //IsPost = true;

        //     montaLinhas();






    }

  public string   montaLinhas( ArrayList listaPNP)
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
            }


          //  Console.WriteLine("Linhas" + this.Linhas);
        }
        catch (Exception e)
        {

        }
        return Linhas;

    }

    public string[] montaColunas(string[] Dimensao_,string[] Fato_)
    {

        string[] Colunas = new string[2];
        int c = 0;
        for(int i=0;i<Dimensao_.Length;i++)
        {

            Colunas[0] = Colunas[0] + "{text:'" + Dimensao_[i].Trim().Replace("\'","") + "', align:'start',sortable: false, value: 'c" + c + "',},";
            Colunas[1] = Colunas[1] + Dimensao_[i].Trim().Replace("\'", "") + ";";
            c = c + 1;
        }
         for(int i=0;i<Fato_.Length;i++)
        {

            string[] temp = Fato_[i].Split(",");

            Colunas[0] = Colunas[0] + "{text:'" + temp[0].Replace("\"","") + "', align:'start',sortable: false, value: 'c" + c + "',},";
            Colunas[1] = Colunas[1] + temp[0].Replace("\"", "") + ";";
            c = c + 1;
        }
        return Colunas;

        //   Console.WriteLine("Colunas" + this.Colunas);


    }

    public async void gravaCSV(String arquivo,String colunas,String dimensoesT,String fatosT,String[] Ano){


        if(dimensoesT == null){
          
     

            dimensoesT =  "'dCalendário'[Ano],d_IES[Região, d_IES[UF],d_IES[Estado],d_IES[Organização Acadêmica],dimUnidade[Instituicao]"+
                         "d_IES[Instituição (Nome)],dimCurso[nomeCurso],";
           

            fatosT=
             "\"Número de cursos\", [Número de cursos],"+
             "\"Número de concluintes\", [Número de concluintes],"+
            "\"Número de ingressantes\", [Número de ingressantes],"+
            "\"Número de inscritos\", [Número de inscritos]"+
             "\"Número de Matrículas\", [Número de Matrículas],"+
            "\"Número de vagas\", [Número de vagas],";
           

        
        
        


        }

        string nameFile = "C:\\Pnp\\v1\\pnp-exportar-mvc-csv\\wwwroot\\Pnp\\";
            fatosT = fatosT.ToString().Substring(0,fatosT.Length-1);
             dimensoesT  = dimensoesT.ToString().Substring(0,dimensoesT.Length-1);

        string filter = "";



        if (Ano != null)
        {

            string anos = "";
            for (int i = 0; i < Ano.Length; i++)
            {
                anos = anos + Ano[i] + ",";


            }
            if (anos.Length != 0)
            {
                filter = "ALL( 'dCalendário'[Ano] ),'dCalendário'[Ano] IN {" + anos.Substring(0, anos.Length - 1) + "}";
            }

        }

           
                     using StreamWriter file = new(nameFile +arquivo);
      
                    await file.WriteLineAsync(colunas.Substring(0,colunas.Length-1));
                    int cont=0;

                    FacadeCargaDax facadeCargaDax = new FacadeCargaDax();
                    ArrayList listaPNP1 = facadeCargaDax.executeDaxQueryDinamicPNP(dimensoesT, fatosT, filter, "", 1,40000);
                    int c=0;


                        foreach (string line in listaPNP1)
                           {
                    //  Console.WriteLine("gravei");
                    

                            await file.WriteLineAsync(line);

                            c=c+1;
                            
                            }
 


    }



}