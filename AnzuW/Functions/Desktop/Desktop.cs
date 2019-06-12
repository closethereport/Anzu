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

            // try catch Нужно для остановки потока кнопкой STOP из UI
            // Если юзер нажмет STOP на интерфейсе будет переход в catch (Так же если возникнут исключения)
            try
            {
                string zipPath = AnzuW.Properties.Settings.Default.MainBackupFolder + "Desktop " + DateTime.Now.ToString("dd.MM.yyyy (hh-mm)") + ".zip";
                //Environment.SpecialFolder.DesktopDirectory - это путь до рабочего стола
                Progress.AddLog("Backup to " + zipPath); //Добавить строку в лог

                //Лист с фалами
                List<FileInfo> FileList = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)).GetFiles().ToList();

                //Чтобы ярлыки тоже были
                FileList.AddRange(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory)).GetFiles().ToList());

                //Лист с директориями
                List<DirectoryInfo> DirectoryList = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)).GetDirectories().ToList();

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
                                Progress.AddLog("ERROR ");
                                break;

                            case ZipProgressEventType.Saving_AfterWriteEntry: //Конец архив очередного файла
                                Progress.AddLog("Done:" + e.CurrentEntry.FileName);
                                Progress.SetText(e.EntriesSaved + "/" + e.EntriesTotal);
                                Progress.SetProgress(e.EntriesSaved);
                                break;
                        }
                    };

                    //Добавляем к архиву папку рабочего стала (их две у винды)
                    //zip.AddDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), "");
                    zip.AddDirectory(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "");

                    zip.Save(zipPath);  // Создаем архив
                }

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