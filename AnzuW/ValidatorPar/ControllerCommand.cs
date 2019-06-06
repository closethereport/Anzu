using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///Controller command args
/// </summary>
internal class ControllerCommand
{
	public Command[] Commands { get; }

	public ControllerCommand(string[] args)
	{
		//Commands = new Command[] {
		//	new Command(new string[]{"--NoGUI", "-nogui"}, Execute)
		//};
	}

	public void Parse()
	{
	}
}