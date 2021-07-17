using System.Threading.Tasks;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface IOrderLineRepo
	{
		Task<bool> PostOrderLine(OrderLine orderLine);
	}
}