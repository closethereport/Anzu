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
///����� ��� ������� Desktop
/// </summary>
internal class Desktop
{
	public void Backup() //������� ������
	{
		//���� ��� ��� ����� ���������� � MainWindow.xaml
		//��� ������� Button_Click_DesktopBackup, ������ ���������� ��� ��� �����������
		//(���� Button_Click_DesktopBackup ���������� ����� �� ������ ����� � ���������� ����� ���������)

		//���� �������� ������, ����� ������� ����� ����� ��� �������� ����

		//����� �����
		MainWindow.BGThread = (new Thread(() =>
		{
			////////////////////���� ������////////////////////////
			///

			//����������� ������� ���� � UI
			//������� ���������� ������� ����
			var Progress = new ProgressController();
			Progress.ShowProgressBar(); //�������� ���

			// try catch ����� ��� ��������� ������ ������� STOP �� UI
			// ���� ���� ������ STOP �� ���������� ����� ������� � catch (��� �� ���� ��������� ����������)
			try
			{
				string zipPath = AnzuW.Properties.Settings.Default.MainBackupFolder + "Desktop " + DateTime.Now.ToString("dd.MM.yyyy (hh-mm)") + ".zip";
				//Environment.SpecialFolder.DesktopDirectory - ��� ���� �� �������� �����
				Progress.AddLog("Backup to " + zipPath); //�������� ������ � ���

				//���� � ������
				List<FileInfo> FileList = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)).GetFiles().ToList();

				//����� ������ ���� ����
				FileList.AddRange(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory)).GetFiles().ToList());

				//���� � ������������
				List<DirectoryInfo> DirectoryList = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)).GetDirectories().ToList();

				using (ZipFile zip = new ZipFile()) // ������� ������ ��� ������ � �������
				{
					zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // MAX ������� ������
					zip.AlternateEncoding = Encoding.UTF8; //���������
					zip.AlternateEncodingUsage = ZipOption.AsNecessary;  //���������

					//������������ ������� (��������� ������������� )
					zip.SaveProgress += (sender, e) =>
					{
						switch (e.EventType) //e - ���� �������
						{
							case ZipProgressEventType.Saving_Started: //������
								Progress.SetMax(e.EntriesTotal * 2); //���� �������� �������� ����
								break;

							case ZipProgressEventType.Saving_AfterRenameTempArchive: // ����� ����� ���������
								Progress.AddLog("Done"); //����
								Progress.AddLog("Archive size (byte):" + new FileInfo(e.ArchiveName).Length.ToString());
								break;

							case ZipProgressEventType.Saving_BeforeWriteEntry: //������ ������������� ���������� �����
								Progress.AddLog("Add:" + e.CurrentEntry.FileName);
								break;

							case ZipProgressEventType.Error_Saving: //������
								Progress.AddLog("ERROR ");
								break;

							case ZipProgressEventType.Saving_AfterWriteEntry: //����� ����� ���������� �����
								Progress.AddLog("Done:" + e.CurrentEntry.FileName);
								Progress.SetText(e.EntriesSaved + "/" + e.EntriesTotal);
								Progress.SetProgress(e.EntriesSaved);
								break;
						}
					};

					//��������� � ������ ����� �������� ����� (�� ��� � �����)
					zip.AddDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory), "");
					zip.AddDirectory(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "");

					zip.Save(zipPath);  // ������� �����
				}

				Progress.AddLog("///Start delete file///");
				foreach (var current in FileList) //������� �����
				{
					Progress.AddLog(current.Name);
					Progress.AddProgress(1);
					current.Delete();
				}
				foreach (var current in DirectoryList) //������� �����
				{
					Progress.AddLog(current.Name);
					Progress.AddProgress(1);
					current.Delete(true);
				}

				Progress.HideProgressBar(); //�������� ���
			}
			catch (Exception ex)
			{
				Progress.HideProgressBar(); //������� ���
			}
		}));

		MainWindow.BGThread.IsBackground = true; //����������� ������������� ��� ������
		MainWindow.BGThread.Start(); //������ ������
	}
}