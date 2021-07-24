using System.Collections.Generic;
using AccountingDatabase.Entity;
using AccountingDatabase.Repository.Implementation;
using AccountingDatabase.Repository.Interface;
using AccountingHelper.Model;
using NLog;

namespace AccountingHelper.Helper.ModelHelper
{
	public class GlAccountModelHelper : IModelHelper<GLAccountModel, GLAccount>
	{
		private readonly IGlAccountService _glAccountService = new GlAccountService();
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public List<GLAccount> TransformValidModels(List<GLAccountModel> source)
		{
			var glAccounts = new List<GLAccount>();
			foreach (var model in source)
			{
				if (IsDuplicateGLAccount(model.AccountNumber))
				{
					_logger.Debug($"GlAccount: {model.AccountNumber} is already in DB. Skip this model");
					continue;
				}

				var glAccount = new GLAccount
				{
					AccountNumber = model.AccountNumber,
					Description = model.Description,
					Status = model.Status,
					Configuration = model.Config,
					In = model.In,
					Code = model.Code
				};

				_logger.Debug($"Transformation successed, add result for DB insertion.");
				glAccounts.Add(glAccount);
			}

			_logger.Debug($"Transform {glAccounts.Count} vaild GlAccount out of {source.Count} input. Insert them into DB");
			return glAccounts;
		}

		private bool IsDuplicateGLAccount(string accountNumber)
		{
			var glAccount = _glAccountService.GetByID(accountNumber);
			return glAccount != null;
		}
	}
}