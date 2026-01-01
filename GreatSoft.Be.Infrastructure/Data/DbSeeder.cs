using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Common;
using GreatSoft.Be.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context, IPasswordService passwordService)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Seed Roles
        if (!await context.Roles.AnyAsync())
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Name = RoleType.SysAdmin.ToStringValue(),
                    Description = "System Administrator role",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Name = RoleType.Admin.ToStringValue(),
                    Description = "Administrator role with full access",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Name = RoleType.ResidentAdmin.ToStringValue(),
                    Description = "Resident administrator role",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Name = RoleType.Resident.ToStringValue(),
                    Description = "Resident role",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Name = RoleType.Security.ToStringValue(),
                    Description = "Security personnel role",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Name = RoleType.Manager.ToStringValue(),
                    Description = "Community Manager role",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }

        // Seed Admin User
        if (!await context.Users.AnyAsync(u => u.Email == "admin@greatsoft.com"))
        {
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == RoleType.Admin.ToStringValue());
            if (adminRole != null)
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Email = "admin@greatsoft.com",
                    PasswordHash = passwordService.HashPassword("Admin123!"),
                    FirstName = "Admin",
                    LastName = "User",
                    Phone = null,
                    RoleId = adminRole.Id,
                    CompanyId = null,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }

        // Seed SysAdmin User
        if (!await context.Users.AnyAsync(u => u.Email == "sysadmin@greatsoft.com"))
        {
            var sysAdminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == RoleType.SysAdmin.ToStringValue());
            if (sysAdminRole != null)
            {
                var sysAdminUser = new User
                {
                    Username = "sysadmin",
                    Email = "sysadmin@greatsoft.com",
                    PasswordHash = passwordService.HashPassword("SysAdmin123!"),
                    FirstName = "SysAdmin",
                    LastName = "User",
                    Phone = null,
                    RoleId = sysAdminRole.Id,
                    CompanyId = null,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await context.Users.AddAsync(sysAdminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}

