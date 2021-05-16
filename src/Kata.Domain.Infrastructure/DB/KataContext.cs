using Kata.Domain.Infrastructure.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Kata.Domain.Infrastructure.DB
{
    internal class KataContext : DbContext
    {
        public KataContext()
        {
        }

        public KataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Discount>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.DiscountId);
                builder.Property(p => p.Description)
                    .HasMaxLength(200);
                builder.Property(p => p.Amount);

                builder.HasOne(p => p.Parent)
                    .WithMany(p => p.Discounts)
                    .HasForeignKey(p => p.ParentId);
            });

            modelBuilder.Entity<Item>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.ItemId)
                    .HasMaxLength(1);
                builder.Property(p => p.Price);
                builder.Property(p => p.Quantity);

                builder.HasOne(p => p.Parent)
                    .WithMany(p => p.Items)
                    .HasForeignKey(p => p.ParentId);
            });

            modelBuilder.Entity<Basket>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).ValueGeneratedNever();
            });
        }
    }
}
