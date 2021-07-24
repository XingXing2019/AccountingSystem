namespace AccountingDatabase.SqlRepo
{
	public class TransactionSqls
	{
		// Place holder definition {0}: selected fields
		public const string SELECT_TRANSACTIONS = "SELECT\n\t{0}\nFROM\n\tTransactions";

		// Place holder definition {0}: selected criteria
		public const string DELETE_TRANSACTIONS = "DELETE FROM\n\tTransaction\nWHERE\n\t{0}";

		// Place holder definition {0}: aggregation function eg. SUM / AVG / MIN / MAX etc, {1}: column to be pivoted, {2}: column names
		public const string PIVOT_COLUMN_ROW = "SELECT\n\t*\nFROM\n\tData\nPIVOT\n\t({0}([invoices]) FOR {1} IN ({2})) AS PivotTable";
	}
}