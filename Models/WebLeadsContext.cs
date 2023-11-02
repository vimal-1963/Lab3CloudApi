using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCApplication.Models;

public partial class WebLeadsContext : DbContext
{
    public WebLeadsContext()
    {
    }

    public WebLeadsContext(DbContextOptions<WebLeadsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LeadDetail> LeadDetails { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=database-2.cqq1eejd76kh.ca-central-1.rds.amazonaws.com,1433;Database=WebLeads;User ID=admin;Password=Vimal1996;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeadDetail>(entity =>
        {
            entity.HasKey(e => e.LeadId).HasName("PK__LeadDeta__73EF78FA371F1E68");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Firstname)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserInfo__3214EC072752662A");

            entity.ToTable("UserInfo");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Pwd).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
