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
		this.Alias = Alias ?? throw new ArgumentNullException("������ ������ �� ����� ���� �������", nameof(Alias));
		MyExecute = execute ?? throw new ArgumentNullException("������� �� ����� ���� null", nameof(execute));
		for (int i = 0; i <= this.Alias.Length; i++)
			this.Alias[i].ToLower();
	}

	public void Check(string cmd)
	{
		//TODO: ������
	}
}