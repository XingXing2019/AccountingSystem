using System.Collections.Generic;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface IGlAccountService
	{
		GLAccount Get(string id);
		List<GLAccount> GetAll();

		bool Post(GLAccount item);
		bool PostAll(IList<GLAccount> items);
	}
}