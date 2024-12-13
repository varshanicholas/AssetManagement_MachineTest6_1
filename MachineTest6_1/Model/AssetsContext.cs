using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MachineTest6_1.Model;

public partial class AssetsContext : DbContext
{
    public AssetsContext()
    {
    }

    public AssetsContext(DbContextOptions<AssetsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssetCategory> AssetCategories { get; set; }

    public virtual DbSet<AssetDefinition> AssetDefinitions { get; set; }

    public virtual DbSet<AssetsMaster> AssetsMasters { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserRegistration> UserRegistrations { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =SHAJITH-NICHOLA\\SQLEXPRESS; Initial Catalog = Assets; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Asset_Ca__6DB38D6EB69A6FE6");

            entity.ToTable("Asset_Category");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Category_Name");
        });

        modelBuilder.Entity<AssetDefinition>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Asset_De__991B58A6E2EC2ED4");

            entity.ToTable("Asset_Definition");

            entity.Property(e => e.AssetId).HasColumnName("Asset_Id");
            entity.Property(e => e.AssetClass)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Asset_Class");
            entity.Property(e => e.AssetName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Asset_Name");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

            entity.HasOne(d => d.Category).WithMany(p => p.AssetDefinitions)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Asset_Def__Categ__44FF419A");
        });

        modelBuilder.Entity<AssetsMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assets_M__3214EC07D8797E1D");

            entity.ToTable("Assets_Master");

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FromDate).HasColumnType("date");
            entity.Property(e => e.Manufacturer).HasMaxLength(100);
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PurchaseDate).HasColumnType("date");
            entity.Property(e => e.SerialNumber).HasMaxLength(100);
            entity.Property(e => e.ToDate).HasColumnType("date");
            entity.Property(e => e.WarrantyPeriod).HasMaxLength(50);

            entity.HasOne(d => d.AssetType).WithMany(p => p.AssetsMasters)
                .HasForeignKey(d => d.AssetTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assets_Ma__Asset__52593CB8");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.AssetsMasters)
                .HasForeignKey(d => d.PurchaseOrderId)
                .HasConstraintName("FK__Assets_Ma__Purch__534D60F1");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__543E6D435013CA0E");

            entity.ToTable("Purchase_Order");

            entity.HasIndex(e => e.OrderNo, "UQ__Purchase__F1E505CDBBA0C102").IsUnique();

            entity.Property(e => e.PurchaseId).HasColumnName("Purchase_Id");
            entity.Property(e => e.AssetId).HasColumnName("Asset_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("date")
                .HasColumnName("Delivery_Date");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("Order_Date");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Order_No");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Order_Status");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.VendorId).HasColumnName("Vendor_Id");

            entity.HasOne(d => d.Asset).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.AssetId)
                .HasConstraintName("FK__Purchase___Asset__4CA06362");

            entity.HasOne(d => d.Category).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Purchase___Categ__4D94879B");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK__Purchase___Vendo__4F7CD00D");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__User_Log__D7886B87747A5FCC");

            entity.ToTable("User_Login");

            entity.HasIndex(e => e.Username, "UQ__User_Log__536C85E420F3E86D").IsUnique();

            entity.Property(e => e.LoginId).HasColumnName("Login_Id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationId).HasColumnName("Registration_Id");
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Registration).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__User_Logi__Regis__403A8C7D");

            entity.HasOne(d => d.Role).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User_Logi__Role___3F466844");
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__User_Reg__80BC7BF744EF3942");

            entity.ToTable("User_Registration");

            entity.Property(e => e.RegistrationId).HasColumnName("Registration_Id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__User_Rol__D80AB4BBF0278232");

            entity.ToTable("User_Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendor__D9CCC2A8B7A77D5C");

            entity.ToTable("Vendor");

            entity.HasIndex(e => e.VendorName, "UQ__Vendor__A572CF668ABC765C").IsUnique();

            entity.Property(e => e.VendorId).HasColumnName("Vendor_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.ContractEndDate)
                .HasColumnType("date")
                .HasColumnName("Contract_End_Date");
            entity.Property(e => e.ContractStartDate)
                .HasColumnType("date")
                .HasColumnName("Contract_Start_Date");
            entity.Property(e => e.VendorAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vendor_Address");
            entity.Property(e => e.VendorName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Vendor_Name");
            entity.Property(e => e.VendorType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Vendor_Type");

            entity.HasOne(d => d.Category).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Vendor__Category__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
