using AnzuW;
using System;
using System.IO;
using System.Threading;

internal class DownloadFolder
{
	public void Dfolder(bool DeleteFile, bool TypeFolder = false, string DownloadFold = null)
	{
		MainWindow.BGThread = (new Thread(() =>
		{
			////////////////////ТЕЛО ПОТОКА////////////////////////
			///
			var Progress = new ProgressController();
			Progress.ShowProgressBar(); //ПОКАЗАТЬ БАР

			try
			{
				var dir = new DirectoryInfo(DownloadFold ?? KnownFolders.GetPath(KnownFolder.Downloads));
				var FileList = dir.GetFiles();
				Progress.SetMax(DeleteFile == true ? FileList.Length * 2 : FileList.Length);
				string path = dir.FullName + $"/SortFiles({DateTime.Now.ToString("dd.MM.yyyy (hh-mm)")})/";
				if (!TypeFolder)
				{
					Directory.CreateDirectory(path + "/Other/");
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

				if (DeleteFile)//TODO: Записывать файлы которые не переместилтсь  catch (Exception ex) и их НЕ УДАЛЯТЬ
				{
					foreach (FileInfo file in dir.GetFiles())
					{
						file.Delete();
					}
				}
				Progress.HideProgressBar(); //СКРЫВАЕМ БАР
			}
			catch (Exception ex)
			{
				Progress.AddLog(ex.StackTrace);
				Progress.HideProgressBar("!Error!"); //Закрыть бар
			}
		}));

		MainWindow.BGThread.IsBackground = true; //Обязательно устанавливать для потока
		MainWindow.BGThread.Start(); //Запуск потока
	}
}