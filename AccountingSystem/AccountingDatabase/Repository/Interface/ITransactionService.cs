using System.Threading.Tasks;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface ITransactionService
	{
		Task<bool> PostTransaction(Transaction transaction);
	}
}