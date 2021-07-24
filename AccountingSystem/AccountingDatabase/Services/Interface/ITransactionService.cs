using System;
using System.Collections.Generic;
using System.Linq;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Services.Interface
{
	public interface ITransactionService
	{
		Transaction GetByID(string id);

		Transaction GetByTransactionInfo(DateTime transactionDate, string glAccount, int postSequence, string batchEntry, string sourceCode);

		IQueryable<Transaction> GetAll();

		bool Post(Transaction item);

		bool PostAll(IList<Transaction> items);
	}
}