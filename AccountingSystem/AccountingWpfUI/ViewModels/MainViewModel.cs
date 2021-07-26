using System;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace AccountingWpfUI.ViewModels
{
	class MainViewModel : ObservableObject
	{
		private RelayCommand<MainWindow> _windowMinCommand;

		public RelayCommand<MainWindow> WindowMinCommand
		{
			get
			{
				if (_windowMinCommand == null)
					_windowMinCommand = new RelayCommand<MainWindow>(x => WindowMinCommandHandler(x));
				return _windowMinCommand;
			}
			set { _windowMinCommand = value; }
		}

		private void WindowMinCommandHandler(MainWindow window)
		{
			if (window == null)
				return;

			window.WindowState = WindowState.Minimized;
		}


		private RelayCommand<MainWindow> _windowMaxCommand;

		public RelayCommand<MainWindow> WindowMaxCommand
		{
			get
			{
				if (_windowMaxCommand == null)
					_windowMaxCommand = new RelayCommand<MainWindow>(x => WindowMaxCommandHandler(x));
				return _windowMaxCommand;
			}
			set => _windowMaxCommand = value;
		}

		private void WindowMaxCommandHandler(MainWindow window)
		{
			if (window == null)
				return;

			window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
		}


		private RelayCommand _windowCloseCommand;

		public RelayCommand WindowCloseCommand
		{
			get
			{
				if (_windowCloseCommand == null)
					_windowCloseCommand = new RelayCommand(WindowMaxCloseHandler);
				return _windowCloseCommand;
			}
			set => _windowCloseCommand = value;
		}

		private void WindowMaxCloseHandler()
		{
			Environment.Exit(Environment.ExitCode);
		}
	}
}