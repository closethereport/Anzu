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
				string path = dir.FullName + "/SortFiles/";
				if (!TypeFolder)
				{
					Directory.CreateDirectory(path + "/TextFolder/");
					Directory.CreateDirectory(path + "/VideoFolder/");
					Directory.CreateDirectory(path + "/MusicFolder/");
					Directory.CreateDirectory(path + "/PicFolder/");
					Directory.CreateDirectory(path + "/OtherFolder/");
					foreach (var t in FileList)
					{
						try
						{
							Progress.AddLog("Sort " + t.Name);
							//Ищем файлы текстового формата с последующим копированием в выбранную директорию(TextFolder)
							foreach (string x in new String[] { "*.txt", "*.rtf", "*.doc", "*.docx", "*.html", "*.pdf", "*.odt", "*.fb2", "*.epub", "*.mobi", "*.djvu" })
								if (t.Extension == x)
								{
									t.CopyTo(path + "/TextFolder/" + t.Name, true);
									continue;
								}
							//Ищем видеофайлы с последющим копированием в выбранную директорию(VideoFolder)
							foreach (string x in new String[] { "*.asf", "*.avi", "*.mp4", "*.m4v", "*.mov", "*.mpg", "*.mpeg", "*.swf", "*.wmv", "*.avi", "*.3g2" })
								if (t.Extension == x)
								{
									t.CopyTo(path + "/VideoFolder/" + t.Name, true);
									continue;
								}
							//Ищем аудиофайлы с последующим копированием в выбранную директорию(MusicFolder)
							foreach (string x in new String[] {"*.m4a", "*.aif", "*.aiff", "*.aifc", "*.aif", "*.mov", "*.moov", "*.qt", "*.alaw", "*.caf", "*.gsm", "*.wave", "*.wav", "*.mpa", "*.mp2v", "*.mp2", "*.mp3",
					"*.mpeg","*.mpg","*.midi","*.mid","*.kar","*.rmi","*.wma","*.asf",})
								if (t.Extension == x)
								{
									t.CopyTo(path + "/MusicFolder/" + t.Name, true);
									continue;
								}
							//Ищем картинки с последующим копированием в выбранную директорию(PictureFolder)
							foreach (string x in new String[] { "*.jpg", "*.jpeg", "*.tif", "*.tiff", "*.png", "*.gif", "*.bmp", "*.dib", })
								if (t.Extension == x)
								{
									t.CopyTo(path + "/PicFolder/" + t.Name, true);
									continue;
								}
							//Оставшиеся файлы копируем в выбранную директорию(OtherFolder)
							t.CopyTo(path + t.Name);
							Progress.AddProgress(1);
						}
						catch (Exception ex)
						{
							Progress.AddLog("Error " + t.Name);
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
							Directory.CreateDirectory(path + t.Extension.ToString().Replace(".", ""));
							t.CopyTo(path + t.Extension.ToString().Replace(".", "") + "/" + t.Name, true);
							Progress.AddProgress(1);
						}
						catch (Exception ex)
						{
							Progress.AddLog("Error " + t.Name);
							Progress.AddProgress(1);
						}
					}
				}

				if (DeleteFile)
				{
					foreach (FileInfo file in dir.GetFiles())
					{
						file.Delete();
					}
					foreach (DirectoryInfo di in dir.GetDirectories())
					{
						di.Delete(true);
					}
				}
				Progress.HideProgressBar(); //СКРЫВАЕМ БАР
			}
			catch (Exception ex)
			{
				Progress.HideProgressBar("Error"); //Закрыть бар
			}
		}));

		MainWindow.BGThread.IsBackground = true; //Обязательно устанавливать для потока
		MainWindow.BGThread.Start(); //Запуск потока
	}
}