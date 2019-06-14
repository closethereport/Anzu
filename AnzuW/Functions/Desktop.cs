#region copyright

// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

#endregion copyright

using AnzuW;
using Ionic.Zip;
using System;
using System.IO;
using System.Text;
using System.Threading;

/// <summary>
///Класс для вкладки Desktop
/// </summary>
internal class Desktop
{
	public void Backup(bool DelFile) //Функция бэкапа
	{
		MainWindow.BGThread = (new Thread(() =>
		{
			var Progress = new ProgressController();
			Progress.ShowProgressBar();

			try
			{
				string zipPath = AnzuW.Properties.Settings.Default.MainBackupFolder + "Desktop " + DateTime.Now.ToString("dd.MM.yyyy (hh-mm)") + ".zip";
				Progress.AddLog("Backup to " + zipPath); //Добавить строку в лог

				//Лист с фалами
				var FileList = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)).GetFiles();
				var DirectoryList = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)).GetDirectories();

				using (ZipFile zip = new ZipFile()) // Создаем объект для работы с архивом
				{
					zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Level2; // MAX степень сжатия
					zip.AlternateEncoding = Encoding.UTF8; //Кодировка
					zip.AlternateEncodingUsage = ZipOption.AsNecessary;  //Кодировка

					//Отслеживание событий (Прогресса архивирования )
					zip.SaveProgress += (sender, e) =>
					{
						switch (e.EventType) //e - наше событие
						{
							case ZipProgressEventType.Saving_Started: //Начало
								Progress.SetMax(e.EntriesTotal * 2); //Макс значения прогресс бара
								break;

							case ZipProgressEventType.Saving_AfterRenameTempArchive: // конец всего архивиров
								Progress.AddLog("Done"); //логи
								Progress.AddLog("Archive size (byte):" + new FileInfo(e.ArchiveName).Length.ToString());
								break;

							case ZipProgressEventType.Saving_BeforeWriteEntry: //Начало архивирование очередного файла
								Progress.AddLog("Add:" + e.CurrentEntry.FileName);
								break;

							case ZipProgressEventType.Error_Saving: //Ошибка
								Progress.AddLog("Error " + e.CurrentEntry);
								break;

							case ZipProgressEventType.Saving_AfterWriteEntry: //Конец архив очередного файла
								Progress.AddLog("Done:" + e.CurrentEntry.FileName);
								Progress.SetText(e.EntriesSaved + "/" + e.EntriesTotal);
								Progress.SetProgress(e.EntriesSaved);
								break;
						}
					};

					zip.AddDirectory(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "");

					zip.Save(zipPath);  // Создаем архив
				}

				if (DelFile)
				{
					Progress.AddLog("///Start delete file///");
					foreach (var current in FileList) //Удаляем файлы
					{
						Progress.AddLog(current.Name);
						Progress.AddProgress(1);
						current.Delete();
					}
					foreach (var current in DirectoryList) //удаляем папки
					{
						Progress.AddLog(current.Name);
						Progress.AddProgress(1);
						current.Delete(true);
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