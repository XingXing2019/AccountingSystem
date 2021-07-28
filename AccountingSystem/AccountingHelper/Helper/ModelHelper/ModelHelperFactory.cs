namespace AccountingHelper.Helper.ModelHelper
{
	public class ModelHelperFactory
	{
		public static IModelHelper<S, T> CreateModelHelper<S, T>()
		{
			var modelName = typeof(T).Name;
			switch (modelName)
			{
				case "Vendor":
					return new VendorModelHelper() as IModelHelper<S, T>;
				case "GLAccount":
					return new GlAccountModelHelper() as IModelHelper<S, T>;
				case "Transaction":
					return new TransactionModelHelper() as IModelHelper<S, T>;
				default:
					return null;
			}
		}
	}
}