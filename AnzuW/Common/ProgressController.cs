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
///Controller for progress bar
/// </summary>
public class ProgressController
{
	/// <summary>
	/// Main window
	/// </summary>
	public static MainWindow MainWindow { get; set; }

	/// <summary>
	/// Set value progress bar
	/// </summary>
	/// <param name="progress">value</param>
	public void SetProgress(int progress)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Value = progress;
		}));
	}

	/// <summary>
	/// Set text above progress bar
	/// </summary>
	/// <param name="text">text</param>
	public void SetText(string text)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressText.Content = text;
		}));
	}

	/// <summary>
	/// Set maximum for progress bar
	/// </summary>
	/// <param name="MAX">MAX</param>
	public void SetMax(int MAX)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Maximum = MAX;
		}));
	}

	/// <summary>
	/// Set minimum for progress bar
	/// </summary>
	/// <param name="MIN">MIN</param>
	public void SetMin(int MIN)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Minimum = MIN;
		}));
	}

	/// <summary>
	/// Show progress bar in UI
	/// </summary>
	public void ShowProgressBar()
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Maximum = 100;
			MainWindow.ProgressBar.Minimum = 0;
			MainWindow.ProgressBar.Value = 0;
			MainWindow.ProgressText.Content = "";
			MainWindow.ProgressStopbtn.IsEnabled = true;
			MainWindow.OutputBlock.Text = "";
			MainWindow.ProgressPanel.Visibility = Visibility.Visible;
		}));
	}

	/// <summary>
	/// Hide progress bar in UI and show done MessageBox
	/// </summary>
	public void HideProgressBar()
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressPanel.Visibility = Visibility.Collapsed;
			MessageBox.Show("Successfully completed", "Successfully",
				MessageBoxButton.OK, MessageBoxImage.None);
		}));
	}

	/// <summary>
	/// Increase bar progress by 1 value
	/// </summary>
	public void Inc()
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Value++;
		}));
	}

	/// <summary>
	/// Add line to log panel
	/// </summary>
	/// <param name="log">String</param>
	public void AddLog(string log)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.OutputBlock.Text += log + Environment.NewLine;
			MainWindow.OutputBlockScroll.ScrollToEnd();
		}));
	}
}