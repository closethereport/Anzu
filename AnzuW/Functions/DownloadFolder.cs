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
    public void Dfolder(string DownloadFold, string TextFolder, string VideoFolder, string OtherFolder, string MusicFolder, string PictureFolder)
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
                DirectoryInfo dir = new DirectoryInfo(@DownloadFold);
                //Получаем список файлов в директории
                var FileList = dir.GetFiles();

                //Начинаем забег по листу, для поиска и копирования необходимых файлов
                foreach (var t in FileList)
                {
                    //Ищем файлы текстового формата с последующим копированием в выбранную директорию(TextFolder)
                    foreach (string x in new String[] { "*.txt", "*.rtf", "*.doc", "*.docx", "*.html", "*.pdf", "*.odt", "*.fb2", "*.epub", "*.mobi", "*.djvu" })
                        if (t.Extension == x)
                        {
                            t.CopyTo(TextFolder);
                        }
                    //Ищем видеофайлы с последющим копированием в выбранную директорию(VideoFolder)
                    foreach (string x in new String[] { "*.asf", "*.avi", "*.mp4", "*.m4v", "*.mov", "*.mpg", "*.mpeg", "*.swf", "*.wmv", "*.avi", "*.3g2" })
                        if (t.Extension == x)
                        {
                            t.CopyTo(VideoFolder);
                        }
                    //Ищем аудиофайлы с последующим копированием в выбранную директорию(MusicFolder)
                    foreach (string x in new String[] {"*.m4a", "*.aif", "*.aiff", "*.aifc", "*.aif", "*.mov", "*.moov", "*.qt", "*.alaw", "*.caf", "*.gsm", "*.wave", "*.wav", "*.mpa", "*.mp2v", "*.mp2", "*.mp3",
                    "*.mpeg","*.mpg","*.midi","*.mid","*.kar","*.rmi","*.wma","*.asf",})
                        if (t.Extension == x)
                        {
                            t.CopyTo(MusicFolder);
                        }
                    //Ищем картинки с последующим копированием в выбранную директорию(PictureFolder)
                    foreach (string x in new String[] { "*.jpg", "*.jpeg", "*.tif", "*.tiff", "*.png", "*.gif", "*.bmp", "*.dib", })
                        if (t.Extension == x)
                        {
                            t.CopyTo(PictureFolder);
                        }
                    //Оставшиеся файлы копируем в выбранную директорию(OtherFolder)
                    t.CopyTo(OtherFolder);
                }

                //        Тут та хуета с галочкой и удалением
                //             if (кнопка нажата)
                //{
                //            foreach (FileInfo file in dir.GetFiles())
                //{
                //    file.Delete();
                //}
                //foreach (DirectoryInfo di in dir.GetDirectories())
                //{
                //    di.Delete(true);
                //}
                Progress.HideProgressBar(); //СКРЫВАЕМ БАР
            }

            catch (Exception ex)
            {
                Progress.HideProgressBar(); //Закрыть бар
            }
        }));
    }
}

