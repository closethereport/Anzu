#region copyright

// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

#endregion copyright

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
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

				ConsoleHelper.Initialize();

				Console.WriteLine("Hello, World!");
				Console.Write("Press a key to continue...");
				Console.Read();
			}
			else
			{
				InitializeComponent();
				MainBackupFolderTextBox.Text = Properties.Settings.Default.MainBackupFolder;
				ProgressPanel.Visibility = Visibility.Collapsed;
				ProgressController.MainWindow = this;
				new WindowController(WindowController.Windows.Desktop);
			}
		}

		/// <summary>
		/// Button in the left side menu to enable the tab desktop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Desktop(object sender, RoutedEventArgs e)
		{
			new WindowController(WindowController.Windows.Desktop);
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
				bk.Backup();
			}
		}

		/// <summary>
		/// Button in the left side menu to enable the tab download
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Download(object sender, RoutedEventArgs e)
		{
			new WindowController(WindowController.Windows.Download);
		}

		/// <summary>
		/// btn in download page (Download Sort)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
		/// Button in the left side menu to enable the tab folder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Folder(object sender, RoutedEventArgs e)
		{
			new WindowController(WindowController.Windows.Folder);
		}

		private void Button_Click_HideProgress(object sender, RoutedEventArgs e)
		{
			ProgressPanel.Visibility = Visibility.Collapsed;
			DoneProgress.Visibility = Visibility.Collapsed;
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
		/// Button in the left side menu to enable the tab setting
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button_Click_Setting(object sender, RoutedEventArgs e)
		{
			new WindowController(WindowController.Windows.Settings);
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

		/// <summary>
		/// Exit button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Exit(object sender, RoutedEventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}

		private void Minimized(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
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

		private void Button_Click_DesktopSort(object sender, RoutedEventArgs e)
		{
			var bk = new DesktopSort();
			bk.Sort(DSortExtension.IsChecked.Value);
		}

		private void Button_Click_DesktopBackupAndSort(object sender, RoutedEventArgs e)
		{
		}

		private void Button_Click_DownloadDelOld(object sender, RoutedEventArgs e)
		{
		}

		private void Button_Click_SortFolder(object sender, RoutedEventArgs e)
		{
		}

		private void Button_Click_DelOldFolder(object sender, RoutedEventArgs e)
		{
		}

		private void Border_DragEnter(object sender, DragEventArgs e)
		{
			DropSortFiles.Visibility = Visibility.Visible;
		}

		private void Border_DragLeave(object sender, DragEventArgs e)
		{
			DropSortFiles.Visibility = Visibility.Collapsed;
		}

		private void Border_DragEnter_1(object sender, DragEventArgs e)
		{
			DropOldFiles.Visibility = Visibility.Visible;
		}

		private void Border_DragLeave_1(object sender, DragEventArgs e)
		{
			DropOldFiles.Visibility = Visibility.Collapsed;
		}

		private void Border_Drop(object sender, DragEventArgs e)
		{
			string[] Args = (string[])e.Data.GetData(DataFormats.FileDrop, true);
			DropSortFiles.Visibility = Visibility.Collapsed;
			foreach (var path in Args)
			{
				if (System.IO.Directory.Exists(path))
				{
				}
			}
		}

		private void Border_Drop_1(object sender, DragEventArgs e)
		{
			string[] Args = (string[])e.Data.GetData(DataFormats.FileDrop, true);
			DropOldFiles.Visibility = Visibility.Collapsed;
			foreach (var path in Args)
			{
				if (System.IO.Directory.Exists(path))
				{
				}
			}
		}
	}
}