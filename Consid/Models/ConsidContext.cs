using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Consid
{
    public partial class ConsidContext : DbContext
    {
        public ConsidContext()
        {
        }

        public ConsidContext(DbContextOptions<ConsidContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<LibraryItem> LibraryItem { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Consid"));
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        //    modelBuilder.Entity<Category>(entity =>
        //    {
        //        entity.ToTable("Category");

        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.Property(e => e.CategoryName)
        //            .IsRequired()
        //            .HasMaxLength(32);
        //    });

        //    modelBuilder.Entity<Employee>(entity =>
        //    {
        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.Property(e => e.FirstName)
        //            .IsRequired()
        //            .HasMaxLength(32);

        //        entity.Property(e => e.IsCeo).HasColumnName("IsCEO");

        //        entity.Property(e => e.LastName)
        //            .IsRequired()
        //            .HasMaxLength(32);

        //        entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");
        //    });

        //    modelBuilder.Entity<LibraryItem>(entity =>
        //    {
        //        entity.ToTable("LibraryItem");

        //        entity.Property(e => e.Id).ValueGeneratedNever();

        //        entity.Property(e => e.Author)
        //            .IsRequired()
        //            .HasMaxLength(32);

        //        entity.Property(e => e.BorrowDate).HasColumnType("date");

        //        entity.Property(e => e.Borrower)
        //            .IsRequired()
        //            .HasMaxLength(32);

        //        entity.Property(e => e.Title)
        //            .IsRequired()
        //            .HasMaxLength(32);

        //        entity.Property(e => e.Type)
        //            .IsRequired()
        //            .HasMaxLength(32);

        //        entity.HasOne(d => d.Category)
        //            .WithMany(p => p.LibraryItems)
        //            .HasForeignKey(d => d.CategoryId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK__LibraryIt__Categ__33D4B598");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
