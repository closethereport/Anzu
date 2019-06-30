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
	public void Backup(bool DelFile = true)
	{
		MainWindow.BGThread = (new Thread(() =>
		{
			var Progress = new ProgressController();
			Progress.ShowProgressBar();

			try
			{
				string zipPath = AnzuW.Properties.Settings.Default.MainBackupFolder + "Desktop " + DateTime.Now.ToString("dd.MM.yyyy (hh-mm)") + ".zip";
				Progress.AddLog("Backup to " + zipPath);

				var FileList = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)).GetFiles();
				var DirectoryList = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)).GetDirectories();

				using (ZipFile zip = new ZipFile())
				{
					zip.CompressionLevel = GetComLvl();
					zip.AlternateEncoding = Encoding.UTF8;
					zip.AlternateEncodingUsage = ZipOption.AsNecessary;

					zip.SaveProgress += (sender, e) =>
					{
						switch (e.EventType)
						{
							case ZipProgressEventType.Saving_Started:
								Progress.SetMax(e.EntriesTotal * 2);
								break;

							case ZipProgressEventType.Saving_AfterRenameTempArchive:
								Progress.AddLog("Done");
								Progress.AddLog("Archive size (byte):" + new FileInfo(e.ArchiveName).Length.ToString());
								break;

							case ZipProgressEventType.Saving_BeforeWriteEntry:
								Progress.AddLog("Add:" + e.CurrentEntry.FileName);
								break;

							case ZipProgressEventType.Error_Saving:
								Progress.AddLog("Error " + e.CurrentEntry);
								break;

							case ZipProgressEventType.Saving_AfterWriteEntry:
								Progress.AddLog("Done:" + e.CurrentEntry.FileName);
								Progress.SetText(e.EntriesSaved + "/" + e.EntriesTotal);
								Progress.SetProgress(e.EntriesSaved);
								break;
						}
					};

					zip.AddDirectory(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "");

					zip.Save(zipPath);
				}

				if (DelFile)
				{
					Progress.AddLog("///Start delete file///");
					foreach (var current in FileList)
					{
						Progress.AddLog(current.Name);
						Progress.AddProgress(1);
						current.Delete();
					}
					foreach (var current in DirectoryList)
					{
						Progress.AddLog(current.Name);
						Progress.AddProgress(1);
						current.Delete(true);
					}
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

	private Ionic.Zlib.CompressionLevel GetComLvl()
	{
		switch (AnzuW.Properties.Settings.Default.ComLvl)
		{
			case 0:
				return Ionic.Zlib.CompressionLevel.None;

			case 1:
				return Ionic.Zlib.CompressionLevel.Default;

			case 2:
				return Ionic.Zlib.CompressionLevel.Level1;

			case 3:
				return Ionic.Zlib.CompressionLevel.Level4;

			case 4:
				return Ionic.Zlib.CompressionLevel.BestSpeed;

			case 5:
				return Ionic.Zlib.CompressionLevel.BestCompression;

			default: return Ionic.Zlib.CompressionLevel.Default;
		}
	}
}