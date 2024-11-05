using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FINAL_INTERN.Models.Models;

public partial class finalInternDbContext : DbContext
{
    public finalInternDbContext(DbContextOptions<finalInternDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<CarInfo> CarInfos { get; set; }

    public virtual DbSet<CategoriesOfCar> CategoriesOfCars { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3214EC2721221F77");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts).HasConstraintName("FK__Accounts__Role_I__398D8EEE");
        });

        modelBuilder.Entity<CarInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarInfos__3214EC27A1D9C638");

            entity.Property(e => e.StockQuantit).HasDefaultValue(5);

            entity.HasOne(d => d.CategoriesOfCar).WithMany(p => p.CarInfos).HasConstraintName("FK__CarInfos__Catego__3F466844");
        });

        modelBuilder.Entity<CategoriesOfCar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27DBCA6BBE");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC27A35FB73B");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders).HasConstraintName("FK__Orders__Account___4222D4EF");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC272D446259");

            entity.HasOne(d => d.Account).WithMany(p => p.OrderDetails).HasConstraintName("FK__OrderDeta__Accou__45F365D3");

            entity.HasOne(d => d.Car).WithMany(p => p.OrderDetails).HasConstraintName("FK__OrderDeta__Car_I__46E78A0C");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK__OrderDeta__Order__44FF419A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC278E0ADF73");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
