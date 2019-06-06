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
public class ProgressController
{
	private MainWindow MainWindow { get; }

	public ProgressController(MainWindow MainWindow)
	{
		this.MainWindow = MainWindow;
	}

	public void SetProgress(int progress)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Value = progress;
		}));
	}

	public void SetMax(int MAX)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Maximum = MAX;
		}));
	}

	public void SetMin(int MIN)
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressBar.Minimum = MIN;
		}));
	}

	public void ShowProgressBar()
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressPanel.Visibility = Visibility.Visible;
		}));
	}

	public void HideProgressBar()
	{
		MainWindow.Dispatcher.Invoke(new Action(() =>
		{
			MainWindow.ProgressPanel.Visibility = Visibility.Collapsed;
		}));
	}
}