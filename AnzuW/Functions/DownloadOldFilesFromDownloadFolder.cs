using AnzuW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class DownloadOldFilesFromDownloadFolder
{
    public void DeleteFDFolder(int days, string Folder = null)
    {

        MainWindow.BGThread = (new Thread(() =>
        {
            var Progress = new ProgressController();
            Progress.ShowProgressBar();

            try
            {

                var dir = new DirectoryInfo(Folder ?? KnownFolders.GetPath(KnownFolder.Downloads));
                var FileList = dir.GetFiles();
                int iCnt = 0;

                foreach (var file in FileList)
                {


                    file.Refresh();

                    if (file.LastAccessTime <= DateTime.Now.AddDays(-days))
                    {
                        Progress.AddLog("Deleting:" + file.Name);
                        file.Delete();
                        iCnt += 1;
                    }
                    Progress.AddProgress(1);
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
