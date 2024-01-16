using System;
using System.Collections.Generic;
using LapTopStore_Computer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LapTopStore_Computer.Models
{
    public partial class LapTopStoreContext : IdentityDbContext<AppUser>
    {
        public LapTopStoreContext()
        {
        }

        public LapTopStoreContext(DbContextOptions<LapTopStoreContext> options)
            : base(options)
        {
        }

        // public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        //  public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        // public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        // public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        // public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        //  public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        //   public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; } = null!;
        public virtual DbSet<Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetail { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS01;Initial Catalog=LapTopStore;Integrated Security=True; Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<AspNetRole>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetRoleClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            //modelBuilder.Entity<AspNetUserRole>()
            //    .HasOne(userRole => userRole.User)
            //    .WithMany(user => user.UserRoles)
            //    .HasForeignKey(userRole => userRole.UserId)
            //    .IsRequired();

            //modelBuilder.Entity<AspNetUserRole>()
            //    .HasOne(userRole => userRole.Role)
            //    .WithMany(role => role.UserRoles)
            //    .HasForeignKey(userRole => userRole.RoleId)
            //    .IsRequired();


            //modelBuilder.Entity<AspNetUser>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);

            //    entity.HasMany(d => d.Roles)
            //        .WithMany(p => p.Users)
            //        .UsingEntity<Dictionary<string, object>>(
            //            "AspNetUserRole",
            //            l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
            //            r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
            //            j =>
            //            {
            //                j.HasKey("UserId", "RoleId");

            //                j.ToTable("AspNetUserRoles");

            //                j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
            //            });
            //});

            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserLogin>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserToken>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<Attribute>(entity =>
            //{
            //    entity.HasKey(e => e.AttrId);

            //    entity.Property(e => e.AttrId)
            //        .ValueGeneratedNever()
            //        .HasColumnName("AttrID");

            //    entity.Property(e => e.AttrName)
            //        .HasMaxLength(10)
            //        .IsFixedLength();

            //    entity.Property(e => e.AttrValue)
            //        .HasMaxLength(10)
            //        .IsFixedLength();
            //});

            //modelBuilder.Entity<Category>(entity =>
            //{
            //    entity.ToTable("Category");

            //    entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            //    entity.Property(e => e.CategoryDescription).HasMaxLength(255);

            //    entity.Property(e => e.CategoryName).HasMaxLength(255);

            //    entity.Property(e => e.CategoryPicture1)
            //        .HasMaxLength(10)
            //        .IsFixedLength();

            //    entity.Property(e => e.ProductId).HasColumnName("ProductID");
            //});

            //modelBuilder.Entity<Customer>(entity =>
            //{
            //    entity.Property(e => e.CustomerId)
            //        .ValueGeneratedNever()
            //        .HasColumnName("CustomerID");

            //    entity.Property(e => e.CustomerAddress).HasMaxLength(500);

            //    entity.Property(e => e.CustomerEmail).HasMaxLength(500);

            //    entity.Property(e => e.CustomerFirstName).HasMaxLength(250);

            //    entity.Property(e => e.CustomerPassword).HasMaxLength(500);

            //    entity.Property(e => e.CustomerPhone).HasMaxLength(250);

            //    entity.Property(e => e.CustomerLastName).HasMaxLength(250);

            //    entity.Property(e => e.CustomerImage).HasMaxLength(250);

            //    entity.Property(e => e.ConfirmPassword).HasMaxLength(250);
            //});

            //modelBuilder.Entity<Order>(entity =>
            //{
            //    entity.Property(e => e.OrderId)
            //        .ValueGeneratedNever()
            //        .HasColumnName("OrderID");

            //    entity.Property(e => e.Address).HasMaxLength(250);

            //    entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            //    entity.Property(e => e.Email).HasMaxLength(250);

            //    entity.Property(e => e.OrderDate)
            //        .HasMaxLength(10)
            //        .IsFixedLength();

            //    entity.Property(e => e.PaymentDate).HasColumnType("datetime");

            //    entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

            //    entity.Property(e => e.Phone)
            //        .HasMaxLength(50)
            //        .IsFixedLength();

            //    entity.Property(e => e.RequiredDate).HasColumnType("datetime");

            //    entity.Property(e => e.ShippedDate).HasColumnType("datetime");

            //    entity.HasOne(d => d.Customer)
            //        .WithMany(p => p.Orders)
            //        .HasForeignKey(d => d.CustomerId)
            //        .HasConstraintName("FK_Orders_Customers");

            //    entity.HasOne(d => d.Payment)
            //        .WithMany(p => p.Orders)
            //        .HasForeignKey(d => d.PaymentId)
            //        .HasConstraintName("FK_Orders_Payments");
            //});

            //modelBuilder.Entity<OrderDetail>(entity =>
            //{
            //    entity.HasKey(e => new { e.OrderId, e.ProductId });

            //    entity.ToTable("OrderDetail");

            //    entity.Property(e => e.OrderId).HasColumnName("OrderID");

            //    entity.Property(e => e.ProductId).HasColumnName("ProductID");

            //    entity.HasOne(d => d.Order)
            //        .WithMany(p => p.OrderDetails)
            //        .HasForeignKey(d => d.OrderId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_OrderDetail_Orders");

            //    entity.HasOne(d => d.Product)
            //        .WithMany(p => p.OrderDetails)
            //        .HasForeignKey(d => d.ProductId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_OrderDetail_Products");
            //});

            //modelBuilder.Entity<Payment>(entity =>
            //{
            //    entity.Property(e => e.PaymentId)
            //        .ValueGeneratedNever()
            //        .HasColumnName("PaymentID");

            //    entity.Property(e => e.PaymentType).HasMaxLength(250);
            //});

            //modelBuilder.Entity<Product>(entity =>
            //{
            //    entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryID");

            //    entity.Property(e => e.AttrId).HasColumnName("AttrID");

            //    entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            //    entity.Property(e => e.ProductDescription).HasMaxLength(255);

            //    entity.Property(e => e.ProductName).HasMaxLength(255);

            //    entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            //    entity.HasOne(d => d.Category)
            //        .WithMany(p => p.Products)
            //        .HasForeignKey(d => d.CategoryId);

            //    entity.HasOne(d => d.Supplier)
            //        .WithMany(p => p.Products)
            //        .HasForeignKey(d => d.SupplierId)
            //        .HasConstraintName("FK_Products_Suppliers");

            //    entity.HasMany(d => d.Attrs)
            //        .WithMany(p => p.Products)
            //        .UsingEntity<Dictionary<string, object>>(
            //            "ProductAttribute",
            //            l => l.HasOne<Attribute>().WithMany().HasForeignKey("AttrId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ProductAttributes_Attributes"),
            //            r => r.HasOne<Product>().WithMany().HasForeignKey("ProductId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ProductAttributes_Products"),
            //            j =>
            //            {
            //                j.HasKey("ProductId", "AttrId");

            //                j.ToTable("ProductAttributes");

            //                j.IndexerProperty<int>("ProductId").HasColumnName("ProductID");

            //                j.IndexerProperty<int>("AttrId").HasColumnName("AttrID");
            //            });
            //});

            //modelBuilder.Entity<Review>(entity =>
            //{
            //    entity.Property(e => e.ReviewId)
            //        .ValueGeneratedNever()
            //        .HasColumnName("ReviewID");

            //    entity.Property(e => e.ProductId).HasColumnName("ProductID");

            //    entity.Property(e => e.ReviewTime).HasColumnType("datetime");

            //    entity.Property(e => e.UserId).HasColumnName("UserID");

            //    entity.HasOne(d => d.Product)
            //        .WithMany(p => p.Reviews)
            //        .HasForeignKey(d => d.ProductId)
            //        .HasConstraintName("FK_Reviews_Products1");
            //});

            //modelBuilder.Entity<Supplier>(entity =>
            //{
            //    entity.HasKey(e => e.SuppilerId);

            //    entity.Property(e => e.SuppilerId)
            //        .ValueGeneratedNever()
            //        .HasColumnName("SuppilerID");

            //    entity.Property(e => e.SuppilerAddress).HasMaxLength(250);

            //    entity.Property(e => e.SuppilerCity).HasMaxLength(250);

            //    entity.Property(e => e.SuppilerCountry).HasMaxLength(250);

            //    entity.Property(e => e.SuppilerEmail).HasMaxLength(250);

            //    entity.Property(e => e.SupplierName).HasMaxLength(250);
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
