using System;
using System.Collections.Generic;
using System.Linq;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Services.Interface
{
	public interface ITransactionService : IService<Transaction>
	{
		Transaction GetByTransactionInfo(DateTime transactionDate, string glAccount, int postSequence, string batchEntry, string sourceCode);
	}
}