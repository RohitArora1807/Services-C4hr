using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;


namespace C4HR_KB_PROJECT_KPMG.ModelsKB;

public partial class DbC4HRKBContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbC4HRKBContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("C4HRKBConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public DbC4HRKBContext(DbContextOptions<DbC4HRKBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClientMaster> ClientMasters { get; set; }

    public virtual DbSet<Maststate> Maststates { get; set; }

    public virtual DbSet<SiteMaster> SiteMasters { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    //    => optionsBuilder.UseSqlServer("Server=LAPTOP-J8UV7612;Database=C4HRKPMGKB;User ID=sa;Password=jazz69;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientMaster>(entity =>
        {
            entity.HasKey(e => e.ClientId);

            entity.ToTable("ClientMaster");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ContactName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Hoaddress)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("HOaddress");
            entity.Property(e => e.Natofbusiness)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Maststate>(entity =>
        {
            entity.HasKey(e => e.Stateid).HasName("PK_MastStates");

            entity.ToTable("MASTSTATES");

            entity.HasIndex(e => new { e.Stateid, e.Statedesc }, "notistate").IsUnique();

            entity.Property(e => e.Stateid)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STATEID");
            entity.Property(e => e.Stactive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STACTIVE");
            entity.Property(e => e.Statedesc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STATEDESC");
        });

        modelBuilder.Entity<SiteMaster>(entity =>
        {
            entity.HasKey(e => e.SiteId);

            entity.ToTable("SiteMaster");

            entity.HasIndex(e => e.SiteId, "SiteName_SiteMaster").IsUnique();

            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CemployeeHeadCount).HasColumnName("CEmployeeHeadCount");
            entity.Property(e => e.City)
                .HasMaxLength(25)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.SiteName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Type)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Client).WithMany(p => p.SiteMasters)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SiteMaster_ClientMaster");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
