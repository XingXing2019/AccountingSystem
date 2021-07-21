﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingDatabase.Entity;

namespace AccountingDatabase.Repository.Interface
{
	public interface ITransactionService
	{
		Transaction Get(string id);
		List<Transaction> GetAll();

		bool Post(Transaction item);
		bool PostAll(IList<Transaction> items);
	}
}