using LemonAutomotives.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LemonAutomotives.Infrastructure.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Salesperson> Salespersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Products>().ToTable("Products");
            modelBuilder.Entity<Sales>().ToTable("Sales");
            modelBuilder.Entity<Salesperson>().ToTable("Salespersons");

            //Seed to Customers table
            string customersJson = System.IO.File.ReadAllText("Seed/customers.json");
            List<Customer>? customers = System.Text.Json.JsonSerializer.Deserialize<List<Customer>>(customersJson);

            if (customers != null)
            {
                foreach (Customer customer in customers)
                {
                    modelBuilder.Entity<Customer>().HasData(customer);
                }
            }

            //Seed to Products table
            string productsJson = System.IO.File.ReadAllText("Seed/products.json");
            List<Products>? products = System.Text.Json.JsonSerializer.Deserialize<List<Products>>(productsJson);

            if(products != null)
            {
                foreach (Products product in products)
                {
                    modelBuilder.Entity<Products>().HasData(product);
                }
            }
            

            //Seed to Sales table
            string salesJson = System.IO.File.ReadAllText("Seed/sales.json");
            List<Sales>? sales = System.Text.Json.JsonSerializer.Deserialize<List<Sales>>(salesJson);

            if (sales != null)
            {
                foreach (Sales sale in sales)
                {
                    modelBuilder.Entity<Sales>().HasData(sale);
                }
            }
            

            //Seed to Salesperson table
            string salespersonJson = System.IO.File.ReadAllText("Seed/salesperson.json");

            List<Salesperson>? salespeople = System.Text.Json.JsonSerializer.Deserialize<List<Salesperson>>(salespersonJson);

            if(salespeople != null)
            {
                foreach (Salesperson salesperson in salespeople)
                {
                    modelBuilder.Entity<Salesperson>().HasData(salesperson);
                }
            }
            

            //Table Relations

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasOne(s => s.Products)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductID);
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasOne(s => s.Salesperson)
                .WithMany(sp => sp.Sales)
                .HasForeignKey(s => s.SalespersonID);
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerID);
            });
        }
    }
}
