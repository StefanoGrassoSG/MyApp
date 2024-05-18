using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAppCourse.Models.Entities;

namespace WebAppCourse.Models.Services.Infrastructure;

public partial class WebAppDbContext : DbContext
{
    public WebAppDbContext()
    {
    }

    public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=WEBAPPCOURSE;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Courses");
            entity.HasKey(course => course.Id);
            
            #region Mapping generato automaticamente dal tool di reverse engineering
           /* entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC071C5F068D");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CurrentPriceAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("CurrentPrice_Amount");
            entity.Property(e => e.CurrentPriceCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CurrentPrice_Currency");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullPriceAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("FullPrice_Amount");
            entity.Property(e => e.FullPriceCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("FullPrice_Currency");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Rating).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);*/
                #endregion
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            #region 
            /*entity.HasKey(e => e.Id).HasName("PK__Lessons__3214EC0713F2AE6F");

            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Duration)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasDefaultValue("00:00:00");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Lessons__CourseI__3E52440B");*/
                #endregion
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
