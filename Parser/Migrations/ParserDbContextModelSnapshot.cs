﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Parser.Infrastructure.Sql;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Parser.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(ParserDbContext))]
    partial class ParserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Parser.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Parser.Domain.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Parser.Domain.Entities.Price", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId");

                    b.Property<decimal>("Discount");

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Parser.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<Guid>("PriceId");

                    b.Property<Guid>("ProductShippingId");

                    b.HasKey("Id");

                    b.HasIndex("PriceId");

                    b.HasIndex("ProductShippingId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Parser.Domain.Entities.ProductCategory", b =>
                {
                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("CategoryId");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ProductId", "CategoryId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("Parser.Domain.Entities.ProductShipping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CurrencyId");

                    b.Property<Guid>("ShippingMethodId");

                    b.Property<decimal>("ShippingPrice");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("ShippingMethodId");

                    b.ToTable("ProductShippings");
                });

            modelBuilder.Entity("Parser.Domain.Entities.ShippingMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ShippingMethods");
                });

            modelBuilder.Entity("Parser.Domain.Entities.Price", b =>
                {
                    b.HasOne("Parser.Domain.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Parser.Domain.Entities.Product", b =>
                {
                    b.HasOne("Parser.Domain.Entities.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Parser.Domain.Entities.ProductShipping", "ProductShipping")
                        .WithMany()
                        .HasForeignKey("ProductShippingId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Parser.Domain.Entities.ProductCategory", b =>
                {
                    b.HasOne("Parser.Domain.Entities.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Parser.Domain.Entities.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Parser.Domain.Entities.ProductShipping", b =>
                {
                    b.HasOne("Parser.Domain.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Parser.Domain.Entities.ShippingMethod", "ShippingMethod")
                        .WithMany()
                        .HasForeignKey("ShippingMethodId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
