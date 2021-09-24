using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NorthwindMVC.Models;

#nullable disable

namespace NorthwindMVC.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Customerdemographic> Customerdemographics { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }

        public DbSet<SalesByYear> SalesByYearDbSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("Name=ConnectionStrings:OracleConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("NW")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<SalesByYear>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.ShippedDate)
                    .HasColumnName("SHIPPEDDATE");
                entity.Property(e => e.OrderID)
                    .HasColumnName("ORDERID");
                entity.Property(e => e.Subtotal)
                    .HasColumnName("SUBTOTAL");
                entity.Property(e => e.Year)
                    .HasColumnName("YEAR");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORIES");

                entity.HasIndex(e => e.Categoryname, "CATEGORIESCATEGORYNAME");

                entity.Property(e => e.Categoryid)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Categoryname)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYNAME");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Picture)
                    .HasColumnType("BLOB")
                    .HasColumnName("PICTURE");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMERS");

                entity.HasIndex(e => e.City, "CUSTOMERSCITY");

                entity.HasIndex(e => e.Companyname, "CUSTOMERSCOMPANYNAME");

                entity.HasIndex(e => e.Postalcode, "CUSTOMERSPOSTALCODE");

                entity.HasIndex(e => e.Region, "CUSTOMERSREGION");

                entity.Property(e => e.Customerid)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMERID");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Companyname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYNAME");

                entity.Property(e => e.Contactname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTACTNAME");

                entity.Property(e => e.Contacttitle)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTACTTITLE");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("FAX");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Postalcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("POSTALCODE");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("REGION");
            });

            modelBuilder.Entity<Customerdemographic>(entity =>
            {
                entity.HasKey(e => e.Customertypeid);

                entity.ToTable("CUSTOMERDEMOGRAPHICS");

                entity.Property(e => e.Customertypeid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMERTYPEID");

                entity.Property(e => e.Customerdesc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMERDESC");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEES");

                entity.HasIndex(e => e.Lastname, "EMPLOYEESLASTNAME");

                entity.HasIndex(e => e.Postalcode, "EMPLOYEESPOSTALCODE");

                entity.Property(e => e.Employeeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("EMPLOYEEID");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTHDATE");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Extension)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("EXTENSION");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Hiredate)
                    .HasColumnType("DATE")
                    .HasColumnName("HIREDATE");

                entity.Property(e => e.Homephone)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("HOMEPHONE");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Notes)
                    .HasColumnType("BLOB")
                    .HasColumnName("NOTES");

                entity.Property(e => e.Photo)
                    .HasColumnType("BLOB")
                    .HasColumnName("PHOTO");

                entity.Property(e => e.Photopath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PHOTOPATH");

                entity.Property(e => e.Postalcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("POSTALCODE");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("REGION");

                entity.Property(e => e.Reportsto)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REPORTSTO");

                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");

                entity.Property(e => e.Titleofcourtesy)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("TITLEOFCOURTESY");

                entity.HasOne(d => d.ReportstoNavigation)
                    .WithMany(p => p.InverseReportstoNavigation)
                    .HasForeignKey(d => d.Reportsto)
                    .HasConstraintName("FK_EMPLOYEES_EMPLOYEES");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDERS");

                entity.HasIndex(e => e.Customerid, "ORDERSCUSTOMERID");

                entity.HasIndex(e => e.Employeeid, "ORDERSEMPLOYEEID");

                entity.HasIndex(e => e.Orderdate, "ORDERSORDERDATE");

                entity.HasIndex(e => e.Shippeddate, "ORDERSSHIPPEDDATE");

                entity.HasIndex(e => e.Shipvia, "ORDERSSHIPPERSORDERS");

                entity.HasIndex(e => e.Shippostalcode, "ORDERSSHIPPOSTALCODE");

                entity.Property(e => e.Orderid)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Customerid)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMERID");

                entity.Property(e => e.Employeeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("EMPLOYEEID");

                entity.Property(e => e.Freight)
                    .HasColumnType("NUMBER(19,4)")
                    .HasColumnName("FREIGHT");

                entity.Property(e => e.Orderdate)
                    .HasPrecision(6)
                    .HasColumnName("ORDERDATE");

                entity.Property(e => e.Requireddate)
                    .HasPrecision(6)
                    .HasColumnName("REQUIREDDATE");

                entity.Property(e => e.Shipaddress)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("SHIPADDRESS");

                entity.Property(e => e.Shipcity)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SHIPCITY");

                entity.Property(e => e.Shipcountry)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SHIPCOUNTRY");

                entity.Property(e => e.Shipname)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("SHIPNAME");

                entity.Property(e => e.Shippeddate)
                    .HasPrecision(6)
                    .HasColumnName("SHIPPEDDATE");

                entity.Property(e => e.Shippostalcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SHIPPOSTALCODE");

                entity.Property(e => e.Shipregion)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SHIPREGION");

                entity.Property(e => e.Shipvia)
                    .HasPrecision(10)
                    .HasColumnName("SHIPVIA");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("FK_ORDERS_CUSTOMERS");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Employeeid)
                    .HasConstraintName("FK_ORDERS_EMPLOYEES");

                entity.HasOne(d => d.ShipviaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Shipvia)
                    .HasConstraintName("FK_ORDERS_SHIPPERS");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.Orderid, e.Productid })
                    .HasName("PK_ORDER_PRODUCT");

                entity.ToTable("ORDER_DETAILS");

                entity.HasIndex(e => e.Orderid, "ORDERDETAILSORDERID");

                entity.HasIndex(e => e.Productid, "ORDERDETAILSPRODUCTID");

                entity.Property(e => e.Orderid)
                    .HasPrecision(10)
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Productid)
                    .HasPrecision(10)
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Discount)
                    .HasColumnType("FLOAT")
                    .HasColumnName("DISCOUNT")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Quantity)
                    .HasPrecision(10)
                    .HasColumnName("QUANTITY")
                    .HasDefaultValueSql("1 ");

                entity.Property(e => e.Unitprice)
                    .HasColumnType("NUMBER(19,4)")
                    .HasColumnName("UNITPRICE")
                    .HasDefaultValueSql("0 ");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_DETAILS_ORDERS");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_DETAILS_PRODUCTS");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCTS");

                entity.HasIndex(e => e.Categoryid, "PRODUCTSCATEGORYID");

                entity.HasIndex(e => e.Productname, "PRODUCTSPRODUCTNAME");

                entity.HasIndex(e => e.Supplierid, "PRODUCTSSUPPLIERID");

                entity.Property(e => e.Productid)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Categoryid)
                    .HasPrecision(10)
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Discontinued)
                    .HasPrecision(3)
                    .HasColumnName("DISCONTINUED")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Productname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Quantityperunit)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("QUANTITYPERUNIT");

                entity.Property(e => e.Reorderlevel)
                    .HasPrecision(5)
                    .HasColumnName("REORDERLEVEL")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Supplierid)
                    .HasPrecision(10)
                    .HasColumnName("SUPPLIERID");

                entity.Property(e => e.Unitprice)
                    .HasColumnType("NUMBER(19,4)")
                    .HasColumnName("UNITPRICE")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Unitsinstock)
                    .HasPrecision(5)
                    .HasColumnName("UNITSINSTOCK")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Unitsonorder)
                    .HasPrecision(5)
                    .HasColumnName("UNITSONORDER")
                    .HasDefaultValueSql("0 ");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("FK_PRODUCTS_CATEGORIES");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Supplierid)
                    .HasConstraintName("FK_PRODUCTS_SUPPLIERS");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("REGION");

                entity.Property(e => e.Regionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REGIONID");

                entity.Property(e => e.Regiondescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REGIONDESCRIPTION");
            });

            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("SHIPPERS");

                entity.Property(e => e.Shipperid)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("SHIPPERID");

                entity.Property(e => e.Companyname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYNAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("SUPPLIERS");

                entity.HasIndex(e => e.Companyname, "SUPPLIERSCOMPANYNAME");

                entity.HasIndex(e => e.Postalcode, "SUPPLIERSPOSTALCODE");

                entity.Property(e => e.Supplierid)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("SUPPLIERID");

                entity.Property(e => e.Address)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Companyname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYNAME");

                entity.Property(e => e.Contactname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTACTNAME");

                entity.Property(e => e.Contacttitle)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONTACTTITLE");

                entity.Property(e => e.Country)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Fax)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("FAX");

                entity.Property(e => e.Homepage)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("HOMEPAGE");

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Postalcode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("POSTALCODE");

                entity.Property(e => e.Region)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("REGION");
            });

            modelBuilder.Entity<Territory>(entity =>
            {
                entity.ToTable("TERRITORIES");

                entity.Property(e => e.Territoryid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TERRITORYID");

                entity.Property(e => e.Regionid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REGIONID");

                entity.Property(e => e.Territorydescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TERRITORYDESCRIPTION");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Territories)
                    .HasForeignKey(d => d.Regionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TERRITORIES_REGION");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
