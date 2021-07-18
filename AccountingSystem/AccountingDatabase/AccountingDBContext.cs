using AccountingDatabase.Entity;
using Microsoft.EntityFrameworkCore;

namespace AccountingDatabase
{
	public class AccountingDBContext : DbContext
	{
		private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=AccountingSystem";

		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Source> Sources { get; set; }
		public DbSet<Vendor> Vendors { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Transaction>()
				.HasKey(x => new {x.TransactionId, x.BatchEntry});
		}
	}
}