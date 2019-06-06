using System;
using System.IO;
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
			SettingGrid.Visibility = Visibility.Collapsed;
			DesktopGrid.Visibility = Visibility.Visible;
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
		}

		private void Button_Click_SelectMainBackupFolder(object sender, RoutedEventArgs e)
		{
			string folderName = String.Empty;
			using (WinForms.FolderBrowserDialog dlg = new WinForms.FolderBrowserDialog())
			{
				if (dlg.ShowDialog() == WinForms.DialogResult.OK)
					folderName = dlg.SelectedPath;
			}

			MainBackupFolderTextBox.Text = folderName;
			Properties.Settings.Default.MainBackupFolder = folderName;
		}
	}
}