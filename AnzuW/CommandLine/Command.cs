using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///
/// </summary>
public class Command
{
	public delegate void Execute();

	private event Execute MyExecute;

	private string[] Alias { get; }

	public Command(string[] Alias, Execute execute)
	{
		this.Alias = Alias ?? throw new ArgumentNullException("Алиасы команд не могут быть пустыми", nameof(Alias));
		MyExecute = execute ?? throw new ArgumentNullException("Делегат не может быть null", nameof(execute));
		for (int i = 0; i <= this.Alias.Length; i++)
			this.Alias[i].ToLower();
	}

	public void Check(string cmd)
	{
		//TODO: Парсер
	}
}