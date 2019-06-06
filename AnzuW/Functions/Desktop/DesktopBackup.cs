using AnzuW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
///
/// </summary>
internal class DesktopBackup
{
	public void BA(MainWindow MainWindow)
	{
		MainWindow.PoolThread.Add(new Thread(() =>
		{
			var Progress = new ProgressController(MainWindow);
			Progress.ShowProgressBar();

			for (int i = 0; i < 101; i += 1)
			{
				Progress.SetProgress(i);
				Thread.Sleep(300);
			}

			Progress.HideProgressBar();
		}));
		MainWindow.PoolThread.Last().Start();

		//	Transport.FileTransport(@"D:\TEST\WWWWWWWWWW", @"D:\TEST\END");
	}
}