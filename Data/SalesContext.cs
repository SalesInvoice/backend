using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesInvoiceProcess.Models;

namespace SalesInvoiceProcess.Data;

public partial class SalesContext : DbContext
{
    public SalesContext()
    {
    }

    public SalesContext(DbContextOptions<SalesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<SalesInvoice> SalesInvoices { get; set; }

    public virtual DbSet<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=sales;trusted_connection=yes;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityCode).HasName("PK__cities__F98393B15E1792CB");

            entity.Property(e => e.CityCode).ValueGeneratedNever();

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.Cities).HasConstraintName("FK__cities__countryC__25869641");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryCode).HasName("PK__countrie__A09D7FE3781C1C1B");

            entity.Property(e => e.CountryCode).ValueGeneratedNever();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerCode).HasName("PK__customer__47BC9F2C7B6A8A50");

            entity.Property(e => e.CustomerCode).ValueGeneratedNever();

            entity.HasOne(d => d.CityCodeNavigation).WithMany(p => p.Customers).HasConstraintName("FK__customers__cityC__29572725");

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.Customers).HasConstraintName("FK__customers__count__286302EC");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemCode).HasName("PK__items__A22D0FD1712E0EC2");

            entity.Property(e => e.ItemCode).ValueGeneratedNever();
        });

        modelBuilder.Entity<SalesInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceNo).HasName("PK__salesInv__12539646762E70EE");

            entity.HasOne(d => d.CustomerCodeNavigation).WithMany(p => p.SalesInvoices).HasConstraintName("FK__salesInvo__custo__44FF419A");
        });

        modelBuilder.Entity<SalesInvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__salesInv__3213E83F7CCFDA83");

            entity.HasOne(d => d.InvoiceNoNavigation).WithMany(p => p.SalesInvoiceDetails).HasConstraintName("FK__salesInvo__invoi__4CA06362");

            entity.HasOne(d => d.ItemCodeNavigation).WithMany(p => p.SalesInvoiceDetails).HasConstraintName("FK__salesInvo__itemC__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
