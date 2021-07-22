using System.Collections.Generic;

namespace AccountingHelper.Interface
{
	public interface IModelHelper<S, T>
	{
		List<T> Transform(List<S> source);
	}
}