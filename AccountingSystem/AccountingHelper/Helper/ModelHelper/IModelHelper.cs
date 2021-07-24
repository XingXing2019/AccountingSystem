using System.Collections.Generic;

namespace AccountingHelper.Helper.ModelHelper
{
	public interface IModelHelper<S, T>
	{
		List<T> TransformValidModels(List<S> source);
	}
}