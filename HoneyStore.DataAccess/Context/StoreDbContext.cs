using HoneyStore.DataAccess.Entities;
using HoneyStore.DataAccess.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.DataAccess.Context
{
    public class StoreDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPhoto> ProductPhotos { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Wish> Wishes { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Wish

            builder.Entity<Wish>().HasKey(w => new
            {
                w.ProductId,
                w.UserId
            });

            #endregion

            builder.Entity<Category>()
                .HasData(
                    new Category
                    {
                        Id = 1,
                        Name = "Крем-мед"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Медова косметика"
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Настоянки та Мазі"
                    },
                    new Category
                    {
                        Id = 4,
                        Name = "Суміші з медом"
                    },
                    new Category
                    {
                        Id = 5,
                        Name = "Шоколад на меду"
                    },
                    new Category
                    {
                        Id = 6,
                        Name = "Віск і матеріали для виготовлення свічок"
                    },
                    new Category
                    {
                        Id = 7,
                        Name = "Натуральний мед"
                    },
                    new Category
                    {
                        Id = 8,
                        Name = "Пилок і Перга"
                    },
                    new Category
                    {
                        Id = 9,
                        Name = "Горіхи і сухофрукти в меду"
                    }
                );

        }
    }
}
