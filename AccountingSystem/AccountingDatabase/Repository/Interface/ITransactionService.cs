using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface ITransactionService
	{
		Transaction GetByID(string id);

		Transaction GetByTransactionInfo(DateTime transactionDate, string glAccount, int postSequence, string batchEntry, string sourceCode);

		List<Transaction> GetAll();

		bool Post(Transaction item);

		bool PostAll(IList<Transaction> items);
	}
}