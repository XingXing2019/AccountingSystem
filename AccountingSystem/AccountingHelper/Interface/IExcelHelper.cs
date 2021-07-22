﻿using System.Collections.Generic;

namespace AccountingHelper.Interface
{
	public interface IExcelHelper<T>
	{
		List<T> ReadExcel(string filePath);
	}
}