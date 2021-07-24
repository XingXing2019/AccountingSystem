using System.Collections.Generic;
using System.Linq;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Services.Interface
{
	public interface IGlAccountService
	{
		GLAccount GetByID(string id);

		IQueryable<GLAccount> GetAll();

		bool Post(GLAccount item);

		bool PostAll(IList<GLAccount> items);
	}
}