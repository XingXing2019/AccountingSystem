using System.Collections.Generic;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface IGLService
	{
		GL Get(string glCode);
		List<GL> GetAll();

		bool Post(GL gl);
		bool PostAll(IList<GL> gls);
	}
}