using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Store.Api.Data.Entities;

namespace Store.Api.Data
{
    public class StoreContext : DbContext
    {
        // setup constructor with binding configuration (such as: Connection string)
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        // Entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        // mapping configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            // category
            modelBuilder.Entity<Category>(builder =>
            {
                builder.ToTable("Categories");
                
                builder.HasKey(x => x.CategoryId);
                
                builder.Property(x => x.CategoryId)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();
                
                builder.Property(x => x.CategoryName)
                .HasMaxLength(255)
                .IsRequired(true);
            });
            // customer
            modelBuilder.Entity<Customer>(builder =>
            {
                builder.ToTable("Customers");
                builder.HasKey(x => x.CustomerId);
                builder.Property(x => x.CustomerId)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                builder.Property(x => x.CustomerName)
                .HasMaxLength(255)
                .IsRequired(true);

                builder.Property(x => x.Phone)
                .HasMaxLength(255)
                .IsRequired(false);

                builder.Property(x => x.Address)
                .HasMaxLength(1000)
                .IsRequired(false);

                builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired(false);
            });

            // product
            modelBuilder.Entity<Product>(builder =>
            {
                builder.ToTable("Products");

                builder.HasKey(x => x.ProductId);

                builder.Property(x => x.ProductId)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                builder.Property(x => x.ProductName)
                .HasMaxLength(255)
                .IsRequired(true);

                // foreign key
                builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(x => x.CategoryId);
            });

            // order
            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders");
                builder.HasKey(x => x.OrderId);

                builder.Property(x => x.OrderId)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                // foreign key
                builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId);
            });

            // oder detail
            modelBuilder.Entity<OrderDetail>(builder =>
            {
                builder.ToTable("OrderDetails");

                builder.HasKey(x => new { x.OrderId, x.ProductId });

                // foreign key
                builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId);

                builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(x => x.OrderId);
            });
        }
    }

    // setup design context factory
    public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StoreDb;Trusted_Connection=True;");

            return new StoreContext(optionBuilder.Options);
        }
    }
}
