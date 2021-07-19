using AccountingDatabase.Entity;
using Microsoft.EntityFrameworkCore;

namespace AccountingDatabase
{
	public class AccountingDBContext : DbContext
	{
		private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=AccountingSystem";

		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Vendor> Vendors { get; set; }
		public DbSet<GL> Gls { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Vendor>()
				.HasKey(x => x.VendorCode);

			modelBuilder.Entity<GL>()
				.HasKey(x => x.GLCode);
		}
	}
}