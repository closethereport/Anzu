#region copyright

// (c) 2019 Nelu & 601 (github.com/NeluQi)
// This code is licensed under MIT license (see LICENSE for details)

#endregion copyright

using AnzuW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
///
/// </summary>
internal class WindowController
{
	private static Windows ActiveWindow = Windows.NULL;
	private MainWindow Form = Application.Current.Windows[0] as MainWindow;

	public enum Windows
	{
		Desktop,
		Download,
		Folder,
		File,
		Settings,
		NULL
	}

	public WindowController(Windows SelectWindow)
	{
		if (SelectWindow == ActiveWindow)
			return;
		Hide(ActiveWindow);
		Show(SelectWindow);
		ActiveWindow = SelectWindow;
	}

	private void Hide(Windows win)
	{
		switch (win)
		{
			case Windows.Desktop:
				Form.DesktopGrid.Visibility = Visibility.Collapsed;
				Form.btnDesktop.IsEnabled = true;
				break;

			case Windows.Download:
				Form.DownloadGrid.Visibility = Visibility.Collapsed;
				Form.btnDownload.IsEnabled = true;
				break;

			case Windows.File:
				//
				break;

			case Windows.Folder:
				Form.FolderGrid.Visibility = Visibility.Collapsed;
				Form.btnFolder.IsEnabled = true;
				break;

			case Windows.Settings:
				Form.SettingGrid.Visibility = Visibility.Collapsed;
				Form.btnSetting.IsEnabled = true;
				break;

			case Windows.NULL:
				Form.DesktopGrid.Visibility = Visibility.Collapsed;
				Form.btnDesktop.IsEnabled = true;
				Form.DownloadGrid.Visibility = Visibility.Collapsed;
				Form.btnDownload.IsEnabled = true;
				Form.FolderGrid.Visibility = Visibility.Collapsed;
				Form.btnFolder.IsEnabled = true;
				Form.SettingGrid.Visibility = Visibility.Collapsed;
				Form.btnSetting.IsEnabled = true;
				break;
		}
	}

	private void Show(Windows win)
	{
		switch (win)
		{
			case Windows.Desktop:
				Form.DesktopGrid.Visibility = Visibility.Visible;
				Form.btnDesktop.IsEnabled = false;
				break;

			case Windows.Download:
				Form.DownloadGrid.Visibility = Visibility.Visible;
				Form.btnDownload.IsEnabled = false;
				break;

			case Windows.File:
				//
				break;

			case Windows.Folder:
				Form.FolderGrid.Visibility = Visibility.Visible;
				Form.btnFolder.IsEnabled = false;
				break;

			case Windows.Settings:
				Form.SettingGrid.Visibility = Visibility.Visible;
				Form.btnSetting.IsEnabled = false;
				break;
		}
	}
}