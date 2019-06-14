#region copyright

// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

#endregion copyright

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