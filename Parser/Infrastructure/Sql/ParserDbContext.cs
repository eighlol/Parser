using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Parser.Domain.Entities;

namespace Parser.Infrastructure.Sql
{
    [ExcludeFromCodeCoverage]
    public class ParserDbContext : DbContext
    {
        public ParserDbContext(DbContextOptions<ParserDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ShippingMethod> ShippingMethods { get; set; }

        public DbSet<ProductShipping> ProductShippings { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>()
                .HasKey(t => new { t.ProductId, t.CategoryId });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}