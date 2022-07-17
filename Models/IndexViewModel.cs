public record Tabela
{
	public string linhas { get; set; }
	public string colunas { get; set; }

	public string[] Dimnesao { get; set; }

	public string[] Fato { get; set; }
	
}
public record IndexViewModel
{
	public Tabela Tabela { get; set; }

	public List<Tabela> FriendList { get; set; }
}