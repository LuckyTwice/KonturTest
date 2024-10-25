using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kontur;

public partial class KonturContext : DbContext
{
    public KonturContext()
    {
    }

    public KonturContext(DbContextOptions<KonturContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Code> Codes { get; set; }

    public virtual DbSet<Datum> Data { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Kompik\\SQLEXPRESS;Database=Kontur;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Code>(entity =>
        {
            entity.HasKey(e => e.Code1);

            entity.Property(e => e.Code1)
                .HasMaxLength(10)
                .HasColumnName("Code");
            entity.Property(e => e.CodeName).HasMaxLength(150);

            entity.HasOne(d => d.Cat).WithMany(p => p.Codes)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Codes_Categories");
        });

        modelBuilder.Entity<Datum>(entity =>
        {
            entity.Property(e => e.CodeId).HasMaxLength(10);

            entity.HasOne(d => d.Code).WithMany(p => p.Data)
                .HasForeignKey(d => d.CodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Data_Codes");

            entity.HasOne(d => d.Dep).WithMany(p => p.Data)
                .HasForeignKey(d => d.DepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Data_Departments");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
