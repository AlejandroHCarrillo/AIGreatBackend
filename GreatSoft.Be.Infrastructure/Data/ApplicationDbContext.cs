using GreatSoft.Be.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Community> Communities { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<ResidentProvider> ResidentProviders { get; set; }
    public DbSet<ResidentVisit> ResidentVisits { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<ResidentPreference> ResidentPreferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(500);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(e => e.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Role configuration
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        // Company configuration
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.TaxId).HasMaxLength(50);
        });

        // Community configuration
        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.ZipCode).HasMaxLength(20);

            entity.HasOne(e => e.Company)
                .WithMany(c => c.Communities)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Pet configuration
        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Breed).HasMaxLength(100);
            entity.Property(e => e.Color).HasMaxLength(50);

            entity.HasOne(e => e.Community)
                .WithMany(c => c.Pets)
                .HasForeignKey(e => e.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Owner)
                .WithMany(u => u.Pets)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Vehicle configuration
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
            entity.HasIndex(e => e.LicensePlate).IsUnique();
            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.Color).HasMaxLength(50);

            entity.HasOne(e => e.Community)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(e => e.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Owner)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ResidentProvider configuration
        modelBuilder.Entity<ResidentProvider>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ServiceType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(255);

            entity.HasOne(e => e.Community)
                .WithMany(c => c.ResidentProviders)
                .HasForeignKey(e => e.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ResidentVisit configuration
        modelBuilder.Entity<ResidentVisit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.VisitorName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.VisitorDocument).HasMaxLength(50);
            entity.Property(e => e.Purpose).HasMaxLength(500);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);

            entity.HasOne(e => e.Community)
                .WithMany(c => c.ResidentVisits)
                .HasForeignKey(e => e.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Resident)
                .WithMany(u => u.ResidentVisits)
                .HasForeignKey(e => e.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Amenity configuration
        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Rules).HasMaxLength(2000);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.Cost).HasPrecision(18, 2);

            entity.HasOne(e => e.Community)
                .WithMany(c => c.Amenities)
                .HasForeignKey(e => e.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ResidentPreference configuration
        modelBuilder.Entity<ResidentPreference>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Value).IsRequired().HasMaxLength(500);

            entity.HasOne(e => e.Resident)
                .WithMany(u => u.ResidentPreferences)
                .HasForeignKey(e => e.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

