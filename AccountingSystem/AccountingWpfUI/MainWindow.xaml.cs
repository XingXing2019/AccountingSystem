using System;
using System.Collections.Generic;
using System.Windows;
using AccountingHelper.Helper.DataAnalysisHelper;
using AccountingWpfUI.ViewModels;

namespace AccountingWpfUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new MainViewModel();

			
		}
	}
}
