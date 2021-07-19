using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface ITransactionService
	{
		bool Post(Transaction transaction);
		bool PostAll(IList<Transaction> transactions);
	}
}