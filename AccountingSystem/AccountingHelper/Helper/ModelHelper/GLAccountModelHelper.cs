using System;
using System.Collections.Generic;
using System.Linq;
using AccountingDatabase.Entity;
using AccountingDatabase.Services;
using AccountingDatabase.Services.Interface;
using AccountingHelper.Model;
using NLog;

namespace AccountingHelper.Helper.ModelHelper
{
	public class GlAccountModelHelper : IModelHelper<GLAccountModel, GLAccount>
	{
		private readonly IGlAccountService _glAccountService = new GlAccountService();
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public bool TransformValidModels(IList<GLAccountModel> source, out List<GLAccount> target)
		{
			target = new List<GLAccount>();
			try
			{
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
					target.Add(glAccount);
				}

				_logger.Debug($"Transform {target.Count} vaild GlAccount out of {source.Count} input. Insert them into DB");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during transforming GLAccountModel to GLAccount. Ex: {ex.Message}");
				return false;
			}
		}

		#region Helper
		
		private bool IsDuplicateGLAccount(string accountNumber)
		{
			var glAccount = _glAccountService.GetByID(accountNumber);
			return glAccount != null;
		}

		#endregion
	}
}