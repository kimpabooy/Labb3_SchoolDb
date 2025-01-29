using System;
using System.Collections.Generic;
using Labb3_SchoolDb.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3_SchoolDb.Data;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = localhost;Database=SchoolDb;Trusted_Connection=True;Trust server certificate = true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__CB1927A0B20A602E");

            entity.ToTable("Class");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Name)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StaffId).HasColumnName("Staff_Id");

            entity.HasOne(d => d.Staff).WithMany(p => p.Classes)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Class__Teacher_I__398D8EEE");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grade__54F87A3712117417");

            entity.ToTable("Grade");

            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Grade");
            entity.Property(e => e.StaffId).HasColumnName("Staff_ID");
            entity.Property(e => e.StudentId).HasColumnName("Student_ID");
            entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");

            entity.HasOne(d => d.Staff).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Grade__Staff_ID__440B1D61");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Grade__Student_I__4222D4EF");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Grade__Subject_I__4316F928");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__D80AB4BB779FD76C");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(55)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF7A18C1AB6");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.Staff)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Staff__Role_Id__49C3F6B7");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A791490B520");

            entity.ToTable("Student");

            entity.HasIndex(e => e.SecurityNum, "UQ__Student__C8B9AB7989918F0F").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.ClassId).HasColumnName("Class_Id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.SecurityNum)
                .HasMaxLength(16)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Student__Class_I__3D5E1FD2");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__AC1BA388D5811798");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
