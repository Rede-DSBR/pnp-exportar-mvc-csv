namespace pnp_exportar_mvc_csv.Src;

public class DaoModel
{
    public IndexViewModel setModel(string columns, string lines, string[] years)
    {
        var model = new IndexViewModel
        {
            Tabela = new Tabela
            {
                colunas = "",
                linhas = ""
            }
        };
        model.Tabela.colunas = columns;
        model.Tabela.linhas = lines;
        model.Tabela.Ano = years;
        model.Tabela.Dimnesao = null;
        model.Tabela.Dimnesao = null;

        return model;
    }
}