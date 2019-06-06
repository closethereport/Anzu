using AnzuW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// онрнл сдюкхрэ мюуси вхярн дкъ реярнб
/// </summary>
public class TESTED
{
	public void BA(MainWindow MainWindow)
	{
		new Thread(() =>
	   {
		   var Progress = new ProgressController(MainWindow);
		   Progress.ShowProgressBar();

		   for (int i = 0; i < 101; i += 1)
		   {
			   Progress.SetProgress(i);
			   Thread.Sleep(300);
		   }

		   Progress.HideProgressBar();
	   }).Start();

		//	Transport.FileTransport(@"D:\TEST\WWWWWWWWWW", @"D:\TEST\END");
	}
}