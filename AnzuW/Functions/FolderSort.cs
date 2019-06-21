#region copyright

// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

#endregion copyright

using AnzuW;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
///
/// </summary>
internal class FolderSort
{
	public void Sort(bool SortExtended, string path) //Функция бэкапа
	{
		MainWindow.BGThread = (new Thread(() =>
		{
			var Progress = new ProgressController();
			Progress.ShowProgressBar();

			try
			{
				var dir = new DirectoryInfo(path);
				var FileList = dir.GetFiles();
				Progress.SetMax(FileList.Length);
				path += $"/SortFiles({DateTime.Now.ToString("dd.MM.yyyy")})/";
				if (!SortExtended)
				{
					foreach (var t in FileList)
					{
						try
						{
							Progress.AddLog("Sort:" + t.Name);

							Directory.CreateDirectory(path + TypeFiles.GetTypePath(t));
							t.CopyTo(path + TypeFiles.GetTypePath(t) + t.Name, true);

							Progress.AddProgress(1);
						}
						catch (Exception ex)
						{
							Progress.AddLog("Error:" + t.Name);
							Progress.AddLog(ex.StackTrace.ToString());
							Progress.AddProgress(1);
						}
					}
				}
				else
				{
					foreach (var t in FileList)
					{
						try
						{
							Progress.AddLog("Sort:" + t.Name);
							Directory.CreateDirectory(path + t.Extension.ToString().Replace(".", ""));
							t.CopyTo(path + t.Extension.ToString().Replace(".", "") + "/" + t.Name, true);
							Progress.AddProgress(1);
						}
						catch (Exception ex)
						{
							Progress.AddLog("Error:" + t.Name);
							Progress.AddLog(ex.StackTrace.ToString());
							Progress.AddProgress(1);
						}
					}
				}
				foreach (FileInfo file in dir.GetFiles())
				{
					file.Delete();
				}
				Progress.HideProgressBar();
			}
			catch (Exception ex)
			{
				Progress.AddLog(ex.StackTrace);
				Progress.HideProgressBar("!Error!");
			}
		}));

		MainWindow.BGThread.IsBackground = true;
		MainWindow.BGThread.Start();
	}
}