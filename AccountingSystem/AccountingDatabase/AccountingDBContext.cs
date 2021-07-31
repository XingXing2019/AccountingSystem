using System;
using System.Runtime.InteropServices.ComTypes;
using AccountingDatabase.Entity;
using AccountingInitializer.Database;
using Microsoft.EntityFrameworkCore;

namespace AccountingDatabase
{
	public class AccountingDBContext : DbContext
	{
		private const string DatabaseName = "AccountingSystem";
		private string _connectionString = DatabaseManager.Instance.GetConnectionString(DatabaseName);

		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Vendor> Vendors { get; set; }
		public DbSet<GLAccount> GlAccounts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (string.IsNullOrEmpty(_connectionString))
				throw new ApplicationException($"Unable to get connection string with database name: {DatabaseName}");

			optionsBuilder.UseSqlServer(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Vendor>()
				.HasKey(x => x.VendorID);

			modelBuilder.Entity<GLAccount>()
				.HasKey(x => x.AccountNumber);
		}
	}
}