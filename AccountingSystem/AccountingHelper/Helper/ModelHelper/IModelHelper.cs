using System.Collections.Generic;

namespace AccountingHelper.Helper.ModelHelper
{
	public interface IModelHelper<S, T>
	{
		bool TransformValidModels(IList<S> source, out List<T> target);
	}
}