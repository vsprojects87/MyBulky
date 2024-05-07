using Microsoft.EntityFrameworkCore;
using MyBulky.Models;


namespace MyBulky.Data
{
	public class AppDBContext : DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{

		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// added after identity 8.0
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Category>().HasData(
				new Category { CategoryId = 1, Name = "Action"},
				new Category { CategoryId = 2, Name = "Drama" },
				new Category { CategoryId = 3, Name = "Sci-Fi"}
				);


			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Id = 1,
					Title = "Fortune of Time",
					Author = "Billy Spark",
					Description = "xyz",
					ISBN = "SWD9999001",
					Price = 90,
					CategoryId = 1,
					ImgUrl = ""
				},

				new Product
				{
					Id = 2,
					Title = "How to Win Friends and Influence People",
					Author = "Dale Carnegie",
					Description = "xyz",
					ISBN = "SWD9999002",
					Price = 199,
					CategoryId = 2,
					ImgUrl = ""
				});

		}

	}
}
