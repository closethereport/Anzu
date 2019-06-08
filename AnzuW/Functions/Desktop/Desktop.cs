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
///Класс для вкладки Desktop
/// </summary>
internal class Desktop
{
	public void Backup() //Функция бэкапа
	{
		//Если что эта херня вызывается в MainWindow.xaml
		//Там функция Button_Click_DesktopBackup, можешь посмотреть как она вызываеться
		//(функ Button_Click_DesktopBackup вызывается когда на кнопку бекап в интерфейсе проги нажимаешь)

		//Если операция долгая, лучше создать новый поток как показано ниже

		//НОВЫЙ ПОТОК
		MainWindow.BGThread = (new Thread(() =>
		{
			////////////////////ТЕЛО ПОТОКА////////////////////////
			///

			//ОТОБРАЖЕНИЕ ПРОГРЕС БАРА В UI
			//Создаем контроллер прогрес бара
			var Progress = new ProgressController();
			Progress.ShowProgressBar(); //ПОКАЗАТЬ БАР
			Progress.AddLog("Begin backup"); //Добавить строку в лог

			// try catch Нужно для остановки потока кнопкой STOP из UI
			// Если юзер нажмет STOP на интерфейсе будет переход в catch (Так же если возникнут исключения)
			try
			{
				string zipPath = AnzuW.Properties.Settings.Default.MainBackupFolder + "\\Desktop " + DateTime.Now.ToString("dd.MM.yyyy.hh.mm") + ".Backup.zip";
				//Environment.SpecialFolder.DesktopDirectory - это путь до рабочего стола
				Progress.AddLog("Backup to " + zipPath); //Добавить строку в лог

				DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
				var FileList = dir.GetFiles();
				var DirectoriesList = dir.GetDirectories();
				Progress.SetMax(FileList.Length + 1); //Устанавливает для бара максимальную шкалу (По стандарту от 0 до 100, но мы сделаем от 0 до кл-во файлов)
				Progress.SetText("0/" + FileList.Length); //Установка текста над прогрес баром

				using (ZipFile zip = new ZipFile()) // Создаем объект для работы с архивом
				{
					zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // MAX степень сжатия
					zip.UseUnicodeAsNecessary = true;

					for (int i = 0; i < FileList.Length; i++) //Проход по списку файлов
					{
						FileInfo temp = (FileInfo)FileList[i];

						Progress.AddLog("ZipFile " + temp.FullName);   //Добавить строку в лог

						//AnzuW.Properties.Settings.Default.MainBackupFolder  - это путь выбранный юзером в настройках проги (Папка бэкапа)

						zip.AddFile(temp.FullName, "");

						//File.SetAttributes(temp.FullName.ToString(), FileAttributes.Normal);
						//File.Delete(temp.FullName.ToString());

						Progress.Inc(); //увеличиваем прогресс бар на 1
						Progress.SetText(i + "/" + FileList.Length); //Ставим новый текст над баром

						Progress.AddLog("done " + temp.FullName); //Добавить строку в лог
						zip.Save(zipPath);  // Создаем архив
					}

					for (int i = 0; i < DirectoriesList.Length; i++) //Проход по списку файлов
					{
						var temp = DirectoriesList[i];

						Progress.AddLog("ZipFile " + temp.FullName);   //Добавить строку в лог

						//AnzuW.Properties.Settings.Default.MainBackupFolder  - это путь выбранный юзером в настройках проги (Папка бэкапа)

						zip.AddDirectory(temp.FullName, "");

						//File.SetAttributes(temp.FullName.ToString(), FileAttributes.Normal);
						//File.Delete(temp.FullName.ToString());

						Progress.Inc(); //увеличиваем прогресс бар на 1
						Progress.SetText(i + "/" + FileList.Length); //Ставим новый текст над баром

						Progress.AddLog("done " + temp.FullName); //Добавить строку в лог
						zip.Save(zipPath);  // Создаем архив
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