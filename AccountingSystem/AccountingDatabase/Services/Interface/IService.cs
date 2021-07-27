using System.Collections.Generic;
using System.Linq;

namespace AccountingDatabase.Services.Interface
{
	public interface IService<T>
	{
		T GetByID(string id);
		IQueryable<T> GetAll();

		bool Post(T item);
		bool PostAll(IList<T> items);
	}
}