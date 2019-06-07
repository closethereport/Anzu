using AnzuW;
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
				//��������
				//Environment.SpecialFolder.DesktopDirectory - ��� ���� �� �������� �����

				DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
				var FileList = dir.GetFiles();

				Progress.SetMax(FileList.Length + 1); //������������� ��� ���� ������������ ����� (�� ��������� �� 0 �� 100, �� �� ������� �� 0 �� ��-�� ������)
				Progress.SetText("0/" + FileList.Length); //��������� ������ ��� ������� �����

				for (int i = 0; i < FileList.Length; i++) //������ �� ������ ������
				{
					FileInfo temp = (FileInfo)FileList[i];

					Progress.AddLog("Copy " + temp.FullName);   //�������� ������ � ���

					//AnzuW.Properties.Settings.Default.MainBackupFolder  - ��� ���� ��������� ������ � ���������� ����� (����� ������)

					temp.CopyTo(AnzuW.Properties.Settings.Default.MainBackupFolder + "\\" + temp.Name, true); //�����������

					//File.SetAttributes(temp.FullName.ToString(), FileAttributes.Normal);
					//File.Delete(temp.FullName.ToString());

					Progress.Inc(); //����������� �������� ��� �� 1
					Progress.SetText(i + "/" + FileList.Length); //������ ����� ����� ��� �����

					Progress.AddLog("done " + temp.FullName); //�������� ������ � ���
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