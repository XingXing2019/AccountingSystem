using System.Collections.Generic;
using System.Data;
using AccountingHelper.Helper.DataAnalysisHelper;

namespace AccountingHelper.Helper.UIHelper
{
	public class AccountingUIHelper
	{
		private readonly TransactionAnalysisHelper _transactionAnalyzer;
		private readonly VendorAnalysisHelper _vendorAnalyzer;

		public AccountingUIHelper()
		{
			_transactionAnalyzer = new TransactionAnalysisHelper();
			_vendorAnalyzer = new VendorAnalysisHelper();
		}

		public List<string> LoadGroupIDs()
		{
			var data = _vendorAnalyzer.AnalyzeVendorGroups();
			var groupIds = new List<string>();
			foreach (DataRow row in data.Rows)
			{
				groupIds.Add(row.ItemArray[0].ToString());
			}

			return groupIds;
		}
	}
}