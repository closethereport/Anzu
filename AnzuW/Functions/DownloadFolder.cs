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
				DirectoryInfo dir = new DirectoryInfo(DownloadFold ?? KnownFolders.GetPath(KnownFolder.Downloads));
				//Получаем список файлов в директории
				var FileList = dir.GetFiles();
				Progress.SetMax(FileList.Length);
				//Начинаем забег по листу, для поиска и копирования необходимых файлов
				if (!TypeFolder)
				{
					Directory.CreateDirectory(dir.FullName + "/TextFolder/");
					Directory.CreateDirectory(dir.FullName + "/VideoFolder/");
					Directory.CreateDirectory(dir.FullName + "/MusicFolder/");
					Directory.CreateDirectory(dir.FullName + "/PicFolder/");
					Directory.CreateDirectory(dir.FullName + "/OtherFolder/");
					foreach (var t in FileList)
					{
						File.SetAttributes(t.FullName.ToString(), FileAttributes.Normal);
						Progress.AddLog(t.Name);
						//Ищем файлы текстового формата с последующим копированием в выбранную директорию(TextFolder)
						foreach (string x in new String[] { "*.txt", "*.rtf", "*.doc", "*.docx", "*.html", "*.pdf", "*.odt", "*.fb2", "*.epub", "*.mobi", "*.djvu" })
							if (t.Extension == x)
							{
								t.CopyTo(dir.FullName + "/TextFolder/" + t.Name);
							}
						//Ищем видеофайлы с последющим копированием в выбранную директорию(VideoFolder)
						foreach (string x in new String[] { "*.asf", "*.avi", "*.mp4", "*.m4v", "*.mov", "*.mpg", "*.mpeg", "*.swf", "*.wmv", "*.avi", "*.3g2" })
							if (t.Extension == x)
							{
								t.CopyTo(dir.FullName + "/VideoFolder/" + t.Name);
							}
						//Ищем аудиофайлы с последующим копированием в выбранную директорию(MusicFolder)
						foreach (string x in new String[] {"*.m4a", "*.aif", "*.aiff", "*.aifc", "*.aif", "*.mov", "*.moov", "*.qt", "*.alaw", "*.caf", "*.gsm", "*.wave", "*.wav", "*.mpa", "*.mp2v", "*.mp2", "*.mp3",
					"*.mpeg","*.mpg","*.midi","*.mid","*.kar","*.rmi","*.wma","*.asf",})
							if (t.Extension == x)
							{
								t.CopyTo(dir.FullName + "/MusicFolder/" + t.Name);
							}
						//Ищем картинки с последующим копированием в выбранную директорию(PictureFolder)
						foreach (string x in new String[] { "*.jpg", "*.jpeg", "*.tif", "*.tiff", "*.png", "*.gif", "*.bmp", "*.dib", })
							if (t.Extension == x)
							{
								t.CopyTo(dir.FullName + "/PicFolder/" + t.Name);
							}
						//Оставшиеся файлы копируем в выбранную директорию(OtherFolder)
						t.CopyTo(dir.FullName + "/OtherFolder/" + t.Name);
						Progress.AddProgress(1);
					}
				}
				else
				{
					foreach (var t in FileList)
					{
						Directory.CreateDirectory(dir.FullName + "/" + t.Extension.ToString().Replace(".", ""));
						t.CopyTo(dir.FullName + "/" + t.Extension.ToString().Replace(".", "") + "/" + t.Name, true);
						Progress.AddProgress(1);
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
				Progress.HideProgressBar(); //Закрыть бар
			}
		}));

		MainWindow.BGThread.IsBackground = true; //Обязательно устанавливать для потока
		MainWindow.BGThread.Start(); //Запуск потока
	}
}