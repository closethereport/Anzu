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
			Progress.AddLog("Begin backup"); //�������� ������ � ���

			// try catch ����� ��� ��������� ������ ������� STOP �� UI
			// ���� ���� ������ STOP �� ���������� ����� ������� � catch (��� �� ���� ��������� ����������)
			try
			{
				string zipPath = AnzuW.Properties.Settings.Default.MainBackupFolder + "\\Desktop " + DateTime.Now.ToString("dd.MM.yyyy.hh.mm") + ".Backup.zip";
				//Environment.SpecialFolder.DesktopDirectory - ��� ���� �� �������� �����
				Progress.AddLog("Backup to " + zipPath); //�������� ������ � ���

				DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
				var FileList = dir.GetFiles();
				var DirectoriesList = dir.GetDirectories();
				Progress.SetMax(FileList.Length + 1); //������������� ��� ���� ������������ ����� (�� ��������� �� 0 �� 100, �� �� ������� �� 0 �� ��-�� ������)
				Progress.SetText("0/" + FileList.Length); //��������� ������ ��� ������� �����

				using (ZipFile zip = new ZipFile()) // ������� ������ ��� ������ � �������
				{
					zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // MAX ������� ������
					zip.UseUnicodeAsNecessary = true;

					for (int i = 0; i < FileList.Length; i++) //������ �� ������ ������
					{
						FileInfo temp = (FileInfo)FileList[i];

						Progress.AddLog("ZipFile " + temp.FullName);   //�������� ������ � ���

						//AnzuW.Properties.Settings.Default.MainBackupFolder  - ��� ���� ��������� ������ � ���������� ����� (����� ������)

						zip.AddFile(temp.FullName, "");

						//File.SetAttributes(temp.FullName.ToString(), FileAttributes.Normal);
						//File.Delete(temp.FullName.ToString());

						Progress.Inc(); //����������� �������� ��� �� 1
						Progress.SetText(i + "/" + FileList.Length); //������ ����� ����� ��� �����

						Progress.AddLog("done " + temp.FullName); //�������� ������ � ���
						zip.Save(zipPath);  // ������� �����
					}

					for (int i = 0; i < DirectoriesList.Length; i++) //������ �� ������ ������
					{
						var temp = DirectoriesList[i];

						Progress.AddLog("ZipFile " + temp.FullName);   //�������� ������ � ���

						//AnzuW.Properties.Settings.Default.MainBackupFolder  - ��� ���� ��������� ������ � ���������� ����� (����� ������)

						zip.AddDirectory(temp.FullName, "");

						//File.SetAttributes(temp.FullName.ToString(), FileAttributes.Normal);
						//File.Delete(temp.FullName.ToString());

						Progress.Inc(); //����������� �������� ��� �� 1
						Progress.SetText(i + "/" + FileList.Length); //������ ����� ����� ��� �����

						Progress.AddLog("done " + temp.FullName); //�������� ������ � ���
						zip.Save(zipPath);  // ������� �����
					}
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