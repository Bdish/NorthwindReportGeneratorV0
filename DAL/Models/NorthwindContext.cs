using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Shipper> Shipper { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=USERPC\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaCategory_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.Name)
                    .HasName("CategoryName")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(15);

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.Property(e => e.RowTimeStamp).IsRowVersion();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaCustomer_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.City)
                    .HasName("City");

                entity.HasIndex(e => e.Id)
                    .HasName("CustomerId");

                entity.HasIndex(e => e.Name)
                    .HasName("CompanyName");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCode");

                entity.HasIndex(e => e.Region)
                    .HasName("Region");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Code).HasMaxLength(5);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.RowTimeStamp).IsRowVersion();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaEmployee_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.LastName)
                    .HasName("LastName");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCode");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Extension).HasMaxLength(4);

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.HomePhone).HasMaxLength(24);

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.Notes).HasColumnType("ntext");

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.RowTimeStamp).IsRowVersion();

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaOrder_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.CustomerId)
                    .HasName("CustomerOrder");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("EmployeeID");

                entity.HasIndex(e => e.OrderDate)
                    .HasName("OrderDate");

                entity.HasIndex(e => e.ShipPostalCode)
                    .HasName("ShipPostalCode");

                entity.HasIndex(e => e.ShippedDate)
                    .HasName("ShippedDate");

                entity.HasIndex(e => e.ShipperId)
                    .HasName("{B06018A6-1EDE-4A8F-9A0E-53D679E60CA3}");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.RowTimeStamp).IsRowVersion();

                entity.Property(e => e.ShipAddress).HasMaxLength(60);

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipCountry).HasMaxLength(15);

                entity.Property(e => e.ShipName).HasMaxLength(40);

                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);

                entity.Property(e => e.ShipRegion).HasMaxLength(15);

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("CustomerOrders");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("EmployeeOrders");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("ShipperOrders");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaOrderDetail_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.Id)
                    .HasName("OrderDetailId");

                entity.HasIndex(e => e.OrderId)
                    .HasName("OrderID");

                entity.HasIndex(e => e.ProductId)
                    .HasName("ProductID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.RowTimeStamp).IsRowVersion();

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("OrderDetails");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("ProductOrders");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaProduct_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.CategoryId)
                    .HasName("CategoryID");

                entity.HasIndex(e => e.Name)
                    .HasName("ProductName");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("SupplierID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Discontinued).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

                entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

                entity.Property(e => e.RowTimeStamp).IsRowVersion();

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("CategoryProducts");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("SupplierProducts");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaShipper_PK")
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.RowTimeStamp).IsRowVersion();
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("aaaaaSupplier_PK")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.Name)
                    .HasName("CompanyName");

                entity.HasIndex(e => e.PostalCode)
                    .HasName("PostalCode");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.HomePage).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.RowTimeStamp).IsRowVersion();
            });
        }
    }
}
