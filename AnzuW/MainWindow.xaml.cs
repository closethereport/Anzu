#region copyright

// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

#endregion copyright

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WinForms = System.Windows.Forms;

namespace AnzuW
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// BG Thread
		/// </summary>
		public static Thread BGThread;

		/// <summary>
		/// MainWindow
		/// </summary>
		public MainWindow()
		{
			if (Environment.GetCommandLineArgs().Length > 1)
			{
				var CommandLinePasre = new ControllerCommand(Environment.GetCommandLineArgs());
			}
			else
			{
				InitializeComponent();
				MainBackupFolderTextBox.Text = Properties.Settings.Default.MainBackupFolder;
				ProgressPanel.Visibility = Visibility.Collapsed;
				ProgressController.MainWindow = this;
			}
		}

		/// <summary>
		/// Drag and drop for program header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Exit(object sender, RoutedEventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}

		/// <summary>
		/// Button in the left side menu to enable the tab desktop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Desktop(object sender, RoutedEventArgs e)
		{
			//TODO: Сделать нормально
			SettingGrid.Visibility = Visibility.Collapsed;
			DesktopGrid.Visibility = Visibility.Visible;
			DownloadGrid.Visibility = Visibility.Collapsed;
			FolderGrid.Visibility = Visibility.Collapsed;
		}

		/// <summary>
		/// Button in the left side menu to enable the tab setting
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Setting(object sender, RoutedEventArgs e)
		{
			SettingGrid.Visibility = Visibility.Visible;
			DesktopGrid.Visibility = Visibility.Collapsed;
			DownloadGrid.Visibility = Visibility.Collapsed;
			FolderGrid.Visibility = Visibility.Collapsed;
		}

		private void Button_Click_Download(object sender, RoutedEventArgs e)
		{
			SettingGrid.Visibility = Visibility.Collapsed;
			DesktopGrid.Visibility = Visibility.Collapsed;
			FolderGrid.Visibility = Visibility.Collapsed;
			DownloadGrid.Visibility = Visibility.Visible;
		}

		private void Button_Click_Folder(object sender, RoutedEventArgs e)
		{
			SettingGrid.Visibility = Visibility.Collapsed;
			DesktopGrid.Visibility = Visibility.Collapsed;
			DownloadGrid.Visibility = Visibility.Collapsed;
			FolderGrid.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Button in settings (Select folder)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_SelectMainBackupFolder(object sender, RoutedEventArgs e)
		{
			using (WinForms.FolderBrowserDialog dlg = new WinForms.FolderBrowserDialog())
			{
				if (dlg.ShowDialog() == WinForms.DialogResult.OK)
					Properties.Settings.Default.MainBackupFolder = dlg.SelectedPath + @"\";
				MainBackupFolderTextBox.Text = Properties.Settings.Default.MainBackupFolder;
				Properties.Settings.Default.Save();
			}
		}

		/// <summary>
		/// Button in Desktop (Desktop Backup)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_DesktopBackup(object sender, RoutedEventArgs e)
		{
			if (String.IsNullOrWhiteSpace(Properties.Settings.Default.MainBackupFolder))
			{
				MessageBox.Show("You need to install the main backup folder in the settings", "Error",
				MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				var bk = new Desktop();
				//TODO: Сделать чекбокс на форме
				bk.Backup(true);
			}
		}

		private void Button_Click_DownloadSort(object sender, RoutedEventArgs e)
		{
			if (String.IsNullOrWhiteSpace(Properties.Settings.Default.MainBackupFolder))
			{
				MessageBox.Show("You need to install the main backup folder in the settings", "Error",
				MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				var bk = new DownloadFolder();
				bk.Dfolder(DelFiles.IsChecked.Value, SortExtended.IsChecked.Value);
			}
		}

		/// <summary>
		/// Button in progress bar (STOP)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_StopOtherThread(object sender, RoutedEventArgs e)
		{
			ProgressText.Content = "Wait for closing....";
			ProgressStopbtn.IsEnabled = false;
			BGThread.Abort();
		}

		private void Minimized(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void Button_Click_HideProgress(object sender, RoutedEventArgs e)
		{
			ProgressPanel.Visibility = Visibility.Collapsed;
			DoneProgress.Visibility = Visibility.Collapsed;
		}
	}
}