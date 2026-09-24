using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lesson06_ASPNetCore.Models;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<OrderBook> OrderBooks { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-14HSE16\\SQLEXPRESS;Database=BookStore;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5A6A3D465E5");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(512);
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Picture)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C2079F009139");

            entity.ToTable("Book");

            entity.Property(e => e.BookId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Picture).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Book__CategoryId__15502E78");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK__Book__PublisherI__145C0A3F");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BDAE71CFA");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<OrderBook>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderBoo__C3905BCF7390C291");

            entity.ToTable("OrderBook");

            entity.Property(e => e.OrderId)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.AccountId)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.Note).HasMaxLength(512);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderReceive).HasColumnType("datetime");
            entity.Property(e => e.ReceiveAddress).HasMaxLength(512);
            entity.Property(e => e.ReceivePhone)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(16)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.OrderBooks)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__OrderBook__Accou__1A14E395");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36CB91C78C1");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.BookId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OrderId)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.TotalMoney).HasComputedColumnSql("([Quantity]*[Price])", false);

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__OrderDeta__BookI__1DE57479");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__1CF15040");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657FAB68CBB8A9");

            entity.ToTable("Publisher");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PublisherName).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
