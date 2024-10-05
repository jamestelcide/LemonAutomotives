﻿// <auto-generated />
using System;
using LemonAutomotives.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LemonAutomotives.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241004191019_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LemonAutomotives.Core.Domain.Entities.Customer", b =>
                {
                    b.Property<string>("CustomerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("CustomerFirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CustomerLastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CustomerStartDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers", (string)null);

                    b.HasData(
                        new
                        {
                            CustomerID = "CU-JUDITH-MARSH-36770",
                            CustomerAddress = "4116 Franklin Avenue",
                            CustomerFirstName = "Judith",
                            CustomerLastName = "Marsh",
                            CustomerPhone = "3618758716",
                            CustomerStartDate = new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CustomerID = "CU-KIMBERLY-TROMBETTA-45304",
                            CustomerAddress = "3847 Burton Avenue",
                            CustomerFirstName = "Kimberly",
                            CustomerLastName = "Trombetta",
                            CustomerPhone = "9015978933",
                            CustomerStartDate = new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CustomerID = "CU-DENNIS-SANDOVAL-56892",
                            CustomerAddress = "2269 Rose Street",
                            CustomerFirstName = "Dennis",
                            CustomerLastName = "Sandoval",
                            CustomerPhone = "7082569698",
                            CustomerStartDate = new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("LemonAutomotives.Core.Domain.Entities.Products", b =>
                {
                    b.Property<string>("ProductID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("ProductCommission")
                        .HasColumnType("float");

                    b.Property<string>("ProductManufacturer")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("ProductModel")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("ProductName")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<double?>("ProductPurchasePrice")
                        .HasColumnType("float");

                    b.Property<int>("ProductQty")
                        .HasColumnType("int");

                    b.Property<string>("ProductYear")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            ProductID = "2012-FIAT-500",
                            ProductCommission = 0.20000000000000001,
                            ProductManufacturer = "Fiat",
                            ProductModel = "500",
                            ProductName = "2012 Fiat 500",
                            ProductPurchasePrice = 5000.0,
                            ProductQty = 5,
                            ProductYear = "2012"
                        },
                        new
                        {
                            ProductID = "2015-CHRYSLER-300",
                            ProductCommission = 0.089999999999999997,
                            ProductManufacturer = "Chrysler",
                            ProductModel = "300",
                            ProductName = "2015 Chrysler 300",
                            ProductPurchasePrice = 3000.0,
                            ProductQty = 2,
                            ProductYear = "2015"
                        },
                        new
                        {
                            ProductID = "2007-JEEP-GRAND CHEROKEE",
                            ProductCommission = 0.050000000000000003,
                            ProductManufacturer = "Jeep",
                            ProductModel = "Grand Cherokee",
                            ProductName = "2007 Jeep Grand Cherokee",
                            ProductPurchasePrice = 4000.0,
                            ProductQty = 1,
                            ProductYear = "2007"
                        });
                });

            modelBuilder.Entity("LemonAutomotives.Core.Domain.Entities.Sales", b =>
                {
                    b.Property<Guid>("SaleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Commission")
                        .HasColumnType("float");

                    b.Property<double>("CommissionEarnings")
                        .HasColumnType("float");

                    b.Property<string>("CustomerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("PriceSold")
                        .HasColumnType("float");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SalesDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SalespersonID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SaleID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SalespersonID");

                    b.ToTable("Sales", (string)null);

                    b.HasData(
                        new
                        {
                            SaleID = new Guid("b36c0a4f-ba43-4d96-90ff-2b6a968c7981"),
                            Commission = 0.20000000000000001,
                            CommissionEarnings = 1000.0,
                            CustomerID = "CU-JUDITH-MARSH-36770",
                            PriceSold = 5000.0,
                            ProductID = "2012-FIAT-500",
                            SalesDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalespersonID = "LHUNTER84140"
                        },
                        new
                        {
                            SaleID = new Guid("3a345fcd-d4a2-4d88-a1e3-06b2777bb438"),
                            Commission = 0.050000000000000003,
                            CommissionEarnings = 200.0,
                            CustomerID = "CU-KIMBERLY-TROMBETTA-45304",
                            PriceSold = 4000.0,
                            ProductID = "2007-JEEP-GRAND CHEROKEE",
                            SalesDate = new DateTime(2023, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalespersonID = "JSTEWART33126"
                        },
                        new
                        {
                            SaleID = new Guid("1dda1c36-4e5f-4f7d-9171-aad4d575c2be"),
                            Commission = 0.050000000000000003,
                            CommissionEarnings = 270.0,
                            CustomerID = "CU-DENNIS-SANDOVAL-56892",
                            PriceSold = 6000.0,
                            ProductID = "2015-CHRYSLER-300",
                            SalesDate = new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalespersonID = "DLUCZAK88957"
                        });
                });

            modelBuilder.Entity("LemonAutomotives.Core.Domain.Entities.Salesperson", b =>
                {
                    b.Property<string>("SalespersonID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SalespersonAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("SalespersonFirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SalespersonLastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SalespersonPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SalespersonStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SalespersonTerminationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SalespersonID");

                    b.ToTable("Salespersons", (string)null);

                    b.HasData(
                        new
                        {
                            SalespersonID = "LHUNTER84140",
                            SalespersonAddress = "2840 Gambler Lane",
                            SalespersonFirstName = "Hunter",
                            SalespersonLastName = "Lahr",
                            SalespersonPhone = "8013062352",
                            SalespersonStartDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            SalespersonID = "JSTEWART33126",
                            SalespersonAddress = "2408 Hart Ridge Road",
                            SalespersonFirstName = "John",
                            SalespersonLastName = "Stewart",
                            SalespersonPhone = "2013951953",
                            SalespersonStartDate = new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            SalespersonID = "DLUCZAK88957",
                            SalespersonAddress = "2949 Juniper Drive",
                            SalespersonFirstName = "Dennis",
                            SalespersonLastName = "Luczak",
                            SalespersonPhone = "8143934893",
                            SalespersonStartDate = new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SalespersonTerminationDate = new DateTime(2023, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            SalespersonID = "DROGER52760",
                            SalespersonAddress = "4291 Harley Vincent Drive",
                            SalespersonFirstName = "Debra",
                            SalespersonLastName = "Roger",
                            SalespersonPhone = "2033872069",
                            SalespersonStartDate = new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("LemonAutomotives.Core.Domain.Entities.Sales", b =>
                {
                    b.HasOne("LemonAutomotives.Core.Domain.Entities.Customer", "Customer")
                        .WithMany("Sales")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LemonAutomotives.Core.Domain.Entities.Products", "Products")
                        .WithMany("Sales")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LemonAutomotives.Core.Domain.Entities.Salesperson", "Salesperson")
                        .WithMany("Sales")
                        .HasForeignKey("SalespersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Products");

                    b.Navigation("Salesperson");
                });

            modelBuilder.Entity("LemonAutomotives.Core.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("LemonAutomotives.Core.Domain.Entities.Products", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("LemonAutomotives.Core.Domain.Entities.Salesperson", b =>
                {
                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}