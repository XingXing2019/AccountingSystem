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
	public class VendorModelHelper : IModelHelper<VendorModel, Vendor>
	{
		private readonly IVendorService _vendorService = new VendorService();
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public bool TransformValidModels(IList<VendorModel> source, out List<Vendor> target)
		{
			target = new List<Vendor>();
			try
			{
				foreach (var model in source)
				{
					if (IsDuplicateVendor(model.VendorID))
					{
						_logger.Debug($"Vendor: {model.VendorID} is already in DB. Skip this model");
						continue;
					}

					var vendor = new Vendor
					{
						VendorID = model.VendorID,
						VendorName = model.VendName,
						ShortName = model.ShortName,
						GroupID = model.IDGRP,
						Active = model.SwActv,
						OnHold = model.SwHold,
						CurrencyCode = model.CurnCode,
						TermsCode = model.TermsCode,
						TaxClass1 = model.TaxClass1,
						LastMaintenanceDate = model.DateLastMN,
						StartDate = model.DateStart,
						Address1 = model.TextSTRE1,
						Address2 = model.TextSTRE2,
						Address3 = model.TextSTRE3,
						Address4 = model.TextSTRE4,
						City = model.NameCity,
						State = model.CodeSTTE,
						PostCode = model.CodePSTL,
						Country = model.CodeCTRY,
						Phone1 = model.TextPHON1,
						Phone2 = model.TextPHON2
					};

					_logger.Debug($"Transformation successed, add result for DB insertion.");
					target.Add(vendor);
				}

				_logger.Debug($"Transform {target.Count} vaild vendors out of {source.Count} input. Insert them into DB");
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error($"Exception happened during transforming VendorModel to Vendor. Ex: {ex.Message}");
				return false;
			}
		}

		#region Helper

		private bool IsDuplicateVendor(string vendorID)
		{
			var vendor = _vendorService.GetByID(vendorID);
			return vendor != null;
		}

		#endregion
	}
}