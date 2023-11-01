using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().Property(user => user.Username)
				.UseCollation("Latin1_General_CS_AS");
		}

		public DbSet<Country> Countries { get; set; }

		public DbSet<Director> Directors { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<Movie> Movies { get; set; }

		public DbSet<Review> Reviews { get; set; }

		public DbSet<User> Users { get; set; }
	}
}
