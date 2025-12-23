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
    public DbSet<CompanyUser> CompanyUsers { get; set; }
    public DbSet<CommunityType> CommunityTypes { get; set; }
    public DbSet<Community> Communities { get; set; }
    public DbSet<Resident> Residents { get; set; }
    public DbSet<ResidentUser> ResidentUsers { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<ResidentVisit> ResidentVisits { get; set; }
    public DbSet<ProviderServiceType> ProviderServiceTypes { get; set; }
    public DbSet<ResidentProvider> ResidentProviders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            
            entity.HasOne(e => e.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Role configuration
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.RoleType).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Company configuration
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
            entity.Property(e => e.ContactName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // CompanyUser configuration
        modelBuilder.Entity<CompanyUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.Company)
                .WithMany(c => c.CompanyUsers)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.CompanyUsers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Prevent duplicate Company-User relationships
            entity.HasIndex(e => new { e.CompanyId, e.UserId }).IsUnique();
        });

        // CommunityType configuration
        modelBuilder.Entity<CommunityType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Community configuration
        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Location).IsRequired().HasMaxLength(500);
            entity.Property(e => e.ContactPhone).HasMaxLength(20);
            entity.Property(e => e.ContactEmail).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
            
            entity.HasOne(e => e.CommunityType)
                .WithMany(ct => ct.Communities)
                .HasForeignKey(e => e.CommunityTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Resident configuration
        modelBuilder.Entity<Resident>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.HouseNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
            
            entity.HasOne(e => e.Community)
                .WithMany(c => c.Residents)
                .HasForeignKey(e => e.CommunityId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ResidentUser configuration (1-to-1 relationship)
        modelBuilder.Entity<ResidentUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.User)
                .WithOne(u => u.ResidentUser)
                .HasForeignKey<ResidentUser>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Resident)
                .WithOne(r => r.ResidentUser)
                .HasForeignKey<ResidentUser>(e => e.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Ensure one-to-one relationship (unique constraints)
            entity.HasIndex(e => e.UserId).IsUnique();
            entity.HasIndex(e => e.ResidentId).IsUnique();
        });

        // VehicleType configuration
        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Vehicle configuration
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Brand).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Model).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Color).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
            entity.HasIndex(e => e.LicensePlate).IsUnique();
            
            entity.HasOne(e => e.Resident)
                .WithMany(r => r.Vehicles)
                .HasForeignKey(e => e.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.VehicleType)
                .WithMany(vt => vt.Vehicles)
                .HasForeignKey(e => e.VehicleTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Pet configuration
        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Species).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Breed).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Color).IsRequired().HasMaxLength(50);
            
            entity.HasOne(e => e.Resident)
                .WithMany(r => r.Pets)
                .HasForeignKey(e => e.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ResidentVisit configuration
        modelBuilder.Entity<ResidentVisit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.VisitorName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.VehicleColor).HasMaxLength(50);
            entity.Property(e => e.LicensePlate).HasMaxLength(20);
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(500);
            
            entity.HasOne(e => e.Resident)
                .WithMany(r => r.Visits)
                .HasForeignKey(e => e.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ProviderServiceType configuration
        modelBuilder.Entity<ProviderServiceType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Code).IsUnique();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // ResidentProvider configuration
        modelBuilder.Entity<ResidentProvider>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.HasIndex(e => e.Email).IsUnique();
            
            entity.HasOne(e => e.ProviderServiceType)
                .WithMany(pst => pst.Providers)
                .HasForeignKey(e => e.ProviderServiceTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

