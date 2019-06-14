using AnzuW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

internal class DownloadFolder
{
	public void Dfolder(bool DeleteFile, bool TypeFolder = false, string DownloadFold = null)
	{
		MainWindow.BGThread = (new Thread(() =>
		{
			////////////////////ТЕЛО ПОТОКА////////////////////////

			//ОТОБРАЖЕНИЕ ПРОГРЕС БАРА В UI
			//Создаем контроллер прогрес бара
			var Progress = new ProgressController();
			Progress.ShowProgressBar(); //ПОКАЗАТЬ БАР

			// try catch Нужно для остановки потока кнопкой STOP из UI
			// Если юзер нажмет STOP на интерфейсе будет переход в catch (Так же если возникнут исключения)

			try
			{
				//Создаем новую копию DirectoryInfo в которой будет храниться список файлов
				var dir = new DirectoryInfo(DownloadFold ?? KnownFolders.GetPath(KnownFolder.Downloads));
				//Получаем список файлов в директории
				var FileList = dir.GetFiles();
				Progress.SetMax(DeleteFile == true ? FileList.Length * 2 : FileList.Length);
				//Начинаем забег по листу, для поиска и копирования необходимых файлов
				string path = dir.FullName + $"/SortFiles({DateTime.Now.ToString("dd.MM.yyyy (hh-mm)")})/";
				if (!TypeFolder)
				{
					Directory.CreateDirectory(path + "/Other/");
					foreach (var t in FileList)
					{
						bool FileMove = false;
						try
						{
							Progress.AddLog("Sort " + t.Name);

							foreach (string x in new String[] { ".txt", ".rtf", ".doc", ".docx", ".html", ".pdf", ".odt", ".fb2", ".epub", ".mobi", ".djvu", ".xlsx" })
								if (t.Extension.ToLower() == x && !FileMove)
								{
									Directory.CreateDirectory(path + "/Text/");
									t.CopyTo(path + "/Text/" + t.Name, true);
									FileMove = true;
									break;
								}

							foreach (string x in new String[] { ".asf", ".avi", ".mp4", ".m4v", ".mov", ".mpg", ".mpeg", ".swf", ".wmv", ".avi", ".3g2" })
								if (t.Extension.ToLower() == x && !FileMove)
								{
									Directory.CreateDirectory(path + "/Video/");
									t.CopyTo(path + "/Video/" + t.Name, true);
									FileMove = true;
									break;
								}

							foreach (string x in new String[] {".m4a", ".aif", ".aiff", ".aifc", ".aif", ".mov", ".moov", ".qt", ".alaw", ".caf", ".gsm", ".wave", ".wav", ".mpa", ".mp2v", ".mp2", ".mp3",
					".mpeg",".mpg",".midi",".mid",".kar",".rmi",".wma",".asf", ".mid"})
								if (t.Extension.ToLower() == x && !FileMove)
								{
									Directory.CreateDirectory(path + "/Music/");
									t.CopyTo(path + "/Music/" + t.Name, true);
									FileMove = true;
									break;
								}

							foreach (string x in new String[] { ".jpg", ".jpeg", ".tif", ".tiff", ".png", ".gif", ".bmp", ".dib", })
								if (t.Extension.ToLower() == x && !FileMove)
								{
									Directory.CreateDirectory(path + "/Pic/");
									t.CopyTo(path + "/PicFolder/" + t.Name, true);
									FileMove = true;
									break;
								}

							//Файлы с данными
							foreach (string x in new String[] { ".xslt", ".xsl", ".xml", ".one", ".mdf", ".mdb", ".dat", ".csv", ".bin" })
								if (t.Extension.ToLower() == x && !FileMove)
								{
									Directory.CreateDirectory(path + "/Data/");
									t.CopyTo(path + "/Data/" + t.Name, true);
									FileMove = true;
									break;
								}

							foreach (string x in new String[] { ".zip", ".jar", ".7z", ".rar", ".gz", ".tar", ".tar-gz", ".zipx", ".xar" })
								if (t.Extension.ToLower() == x && !FileMove)
								{
									Directory.CreateDirectory(path + "/Archives/");
									t.CopyTo(path + "/Archives/" + t.Name, true);
									FileMove = true;
									break;
								}
							foreach (string x in new String[] { ".msi", ".apk" })
								if (t.Extension.ToLower() == x && !FileMove)
								{
									Directory.CreateDirectory(path + "/Installer/");
									t.CopyTo(path + "/Installer/" + t.Name, true);
									FileMove = true;
									break;
								}

							foreach (string x in new String[] { ".exe", ".bat", ".cmd", ".com", ".gadget", ".msu", ".ps1", ".scr", ".vb", ".vbs", ".wsf" })
								if (t.Extension.ToLower() == x && !FileMove)
								{
									Directory.CreateDirectory(path + "/ExecutedFiles/");
									t.CopyTo(path + "/ExecutedFiles/" + t.Name, true);
									FileMove = true;
									break;
								}

							if (!FileMove)
							{
								t.CopyTo(path + "/Other/" + t.Name);
							}

							Progress.AddProgress(1);
						}
						catch (Exception ex)
						{
							Progress.AddLog("Error " + t.Name);
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
							Progress.AddLog("Sort " + t.Name);
							Directory.CreateDirectory(path + t.Extension.ToString().Replace(".", ""));
							t.CopyTo(path + t.Extension.ToString().Replace(".", "") + "/" + t.Name, true);
							Progress.AddProgress(1);
						}
						catch (Exception ex)
						{
							Progress.AddLog("Error " + t.Name);
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
				Progress.HideProgressBar("!Error!"); //Закрыть бар
			}
		}));

		MainWindow.BGThread.IsBackground = true; //Обязательно устанавливать для потока
		MainWindow.BGThread.Start(); //Запуск потока
	}
}