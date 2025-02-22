﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P50_4_22.Models;

public partial class PetStoreRpmContext : DbContext
{
    public PetStoreRpmContext()
    {
    }

    public PetStoreRpmContext(DbContextOptions<PetStoreRpmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CatalogProduct> CatalogProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-EJI2V8K\\SQLEXPRESS;Initial Catalog=PetStoreRPM; Integrated Security=True; Encrypt=True; Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.IdBrands).HasName("PK__Brands__147C88F5F0D4D7AE");

            entity.Property(e => e.IdBrands).HasColumnName("ID_brands");
            entity.Property(e => e.Brand1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Brand");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__Cart__701794907D8E6D1E");

            entity.ToTable("Cart");

            entity.Property(e => e.IdCart).HasColumnName("ID_cart");
            entity.Property(e => e.CatalogId).HasColumnName("CatalogID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Catalog).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__CatalogID__59063A47");
        });

        modelBuilder.Entity<CatalogProduct>(entity =>
        {
            entity.HasKey(e => e.IdCatalogproducts).HasName("PK__CatalogP__7D82B6FAAB1FB631");

            entity.Property(e => e.IdCatalogproducts).HasColumnName("ID_catalogproducts");
            entity.Property(e => e.BrandsId).HasColumnName("brands_ID");
            entity.Property(e => e.CategoriesId).HasColumnName("categories_ID");
            entity.Property(e => e.DescriptionProduct)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Img)
                .HasMaxLength(260)
                .IsUnicode(false);
            entity.Property(e => e.PriceOfProduct).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.Brands).WithMany(p => p.CatalogProducts)
                .HasForeignKey(d => d.BrandsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CatalogPr__brand__5535A963");

            entity.HasOne(d => d.Categories).WithMany(p => p.CatalogProducts)
                .HasForeignKey(d => d.CategoriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CatalogPr__categ__5629CD9C");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategories).HasName("PK__Categori__487EC275677DC194");

            entity.Property(e => e.IdCategories).HasColumnName("ID_categories");
            entity.Property(e => e.Categories)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview).HasName("PK__Review__4D295FF859D5DD0E");

            entity.ToTable("Review");

            entity.Property(e => e.IdReview).HasColumnName("ID_review");
            entity.Property(e => e.CatalogroductId).HasColumnName("Catalogroduct_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReviewText)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("Users_ID");

            entity.HasOne(d => d.Catalogroduct).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CatalogroductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__Catalogr__5DCAEF64");

            entity.HasOne(d => d.Users).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__Users_ID__5EBF139D");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Roles__45DFFBDBB8B7C2A7");

            entity.Property(e => e.IdRole).HasColumnName("ID_role");
            entity.Property(e => e.Rolee)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("PK__Users__1806910419D156E0");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E387E181029").IsUnique();

            entity.HasIndex(e => e.Loginvhod, "UQ__Users__89F837A0702F99C7").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534AF085AD2").IsUnique();

            entity.Property(e => e.IdUsers).HasColumnName("ID_users");
            entity.Property(e => e.ClientName)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Client_Name");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Loginpassword)
                .HasMaxLength(350)
                .IsUnicode(false);
            entity.Property(e => e.Loginvhod)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.RolesId).HasColumnName("Roles_id");

            entity.HasOne(d => d.Roles).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Roles_id__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
