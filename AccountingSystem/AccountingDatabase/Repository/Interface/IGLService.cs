using System.Collections.Generic;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface IGLService
	{
		GLAccount Get(string glCode);
		List<GLAccount> GetAll();

		bool Post(GLAccount gl);
		bool PostAll(IList<GLAccount> gls);
	}
}