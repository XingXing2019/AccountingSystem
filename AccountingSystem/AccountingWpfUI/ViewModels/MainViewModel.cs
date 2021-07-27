using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using AccountingHelper.Helper.DataAnalysisHelper;
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


		public DataTable Data { get; set; }

		public MainViewModel()
		{
			var selectItems = new List<string>
			{
				"VendorCode",
				"VendorName",
				"COUNT(DISTINCT InvoiceNo) AS Invoices",
				"SUBSTRING(CONVERT(VARCHAR(50), YearPeriod), 1, 7) AS YearPeriod"
			};
			var criterion = new List<string> { "VendorCode IS NOT NULL" };
			var groupByItems = new List<string> { "VendorCode", "VendorName", "YearPeriod" };
			var orderByItems = new List<string> { "VendorCode" };
			var joinItems = new Dictionary<string, string> { { "Vendors", "VendorID = VendorCode" } };

			var startPeriod = new DateTime(2020, 10, 01);
			var endPeriod = new DateTime(2021, 08, 01);
			int pageSize = 10, pageNumber = 2;
			var data = new TransactionAnalysisHelper().AnalyzeTransactionsInYearPeriod(selectItems, joinItems, criterion, groupByItems, orderByItems, startPeriod, endPeriod);

			Data = data;
		}
	}
}