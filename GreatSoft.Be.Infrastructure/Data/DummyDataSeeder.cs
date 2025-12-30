using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Common;
using GreatSoft.Be.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Data;

public static class DummyDataSeeder
{
    public static async Task SeedDummyDataAsync(ApplicationDbContext context, IPasswordService passwordService)
    {
        // Get required roles
        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == RoleType.Admin.ToStringValue());
        var managerRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == RoleType.Manager.ToStringValue());
        var residentRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == RoleType.Resident.ToStringValue());

        if (managerRole == null || residentRole == null)
        {
            throw new InvalidOperationException("Required roles (Manager, Resident) must exist before seeding dummy data.");
        }

        var random = new Random();

        // Seed Admin User: elgrandeahc
        if (adminRole != null && !await context.Users.AnyAsync(u => u.Email == "elgrandeahc@greatsoft.com"))
        {
            var adminUser = new User
            {
                Email = "elgrandeahc@greatsoft.com",
                PasswordHash = passwordService.HashPassword("abc123"),
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

        // Seed 2 Companies
        var companies = new List<Company>();
        if (!await context.Companies.AnyAsync())
        {
            companies.Add(new Company
            {
                Name = "GreatSoft Residencial S.A.",
                Address = "Av. Principal 123, Ciudad",
                Phone = "+1-555-0100",
                Email = "contacto@greatsoftresidencial.com",
                TaxId = "123456789",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });

            companies.Add(new Company
            {
                Name = "Premium Communities Inc.",
                Address = "Calle Secundaria 456, Ciudad",
                Phone = "+1-555-0200",
                Email = "info@premiumcommunities.com",
                TaxId = "987654321",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });

            await context.Companies.AddRangeAsync(companies);
            await context.SaveChangesAsync();
        }
        else
        {
            companies = await context.Companies.ToListAsync();
        }

        // Seed 2 Communities (one for each company)
        var communities = new List<Community>();
        if (companies.Count >= 2)
        {
            if (!await context.Communities.AnyAsync())
            {
                communities.Add(new Community
                {
                    Name = "Residencial Los Pinos",
                    Address = "Calle Los Pinos 100",
                    City = "Ciudad",
                    State = "Estado",
                    ZipCode = "12345",
                    CompanyId = companies[0].Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });

                communities.Add(new Community
                {
                    Name = "Villa del Sol",
                    Address = "Avenida del Sol 200",
                    City = "Ciudad",
                    State = "Estado",
                    ZipCode = "12346",
                    CompanyId = companies[1].Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });

                await context.Communities.AddRangeAsync(communities);
                await context.SaveChangesAsync();
            }
            else
            {
                communities = await context.Communities.ToListAsync();
            }
        }

        // Seed 2 Manager Users for each company (4 managers total)
        var managers = new List<User>();
        var managerEmails = new[]
        {
            new { Email = "manager1@greatsoft.com", FirstName = "Carlos", LastName = "Rodríguez", CompanyIndex = 0 },
            new { Email = "manager2@greatsoft.com", FirstName = "María", LastName = "González", CompanyIndex = 0 },
            new { Email = "manager3@premium.com", FirstName = "Juan", LastName = "Pérez", CompanyIndex = 1 },
            new { Email = "manager4@premium.com", FirstName = "Ana", LastName = "Martínez", CompanyIndex = 1 }
        };

        foreach (var managerInfo in managerEmails)
        {
            if (!await context.Users.AnyAsync(u => u.Email == managerInfo.Email))
            {
                var manager = new User
                {
                    Email = managerInfo.Email,
                    PasswordHash = passwordService.HashPassword("Manager123!"),
                    FirstName = managerInfo.FirstName,
                    LastName = managerInfo.LastName,
                    Phone = $"+1-555-{random.Next(1000, 9999)}",
                    RoleId = managerRole.Id,
                    CompanyId = companies[managerInfo.CompanyIndex].Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                managers.Add(manager);
            }
        }

        if (managers.Any())
        {
            await context.Users.AddRangeAsync(managers);
            await context.SaveChangesAsync();
        }

        // Seed 10 Residents for the first community with their related users
        var residents = new List<User>();
        var residentNames = new[]
        {
            new { FirstName = "Pedro", LastName = "López" },
            new { FirstName = "Laura", LastName = "Sánchez" },
            new { FirstName = "Roberto", LastName = "Fernández" },
            new { FirstName = "Carmen", LastName = "Torres" },
            new { FirstName = "Diego", LastName = "Ramírez" },
            new { FirstName = "Sofía", LastName = "Morales" },
            new { FirstName = "Miguel", LastName = "Jiménez" },
            new { FirstName = "Elena", LastName = "Vargas" },
            new { FirstName = "Andrés", LastName = "Castro" },
            new { FirstName = "Isabel", LastName = "Ortega" }
        };

        if (communities.Any())
        {
            var firstCommunity = communities[0];
            for (int i = 0; i < residentNames.Length; i++)
            {
                var residentInfo = residentNames[i];
                var email = $"resident{i + 1}@community.com";
                
                if (!await context.Users.AnyAsync(u => u.Email == email))
                {
                    var resident = new User
                    {
                        Email = email,
                        PasswordHash = passwordService.HashPassword("Resident123!"),
                        FirstName = residentInfo.FirstName,
                        LastName = residentInfo.LastName,
                        Phone = $"+1-555-{random.Next(2000, 9999)}",
                        RoleId = residentRole.Id,
                        CompanyId = companies[0].Id, // Same company as first community
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    };

                    residents.Add(resident);
                }
            }

            if (residents.Any())
            {
                await context.Users.AddRangeAsync(residents);
                await context.SaveChangesAsync();
                
                // Refresh to get IDs from database
                var residentEmails = residents.Select(r => r.Email).ToList();
                residents = await context.Users
                    .Where(u => residentEmails.Contains(u.Email))
                    .ToListAsync();
            }
            else
            {
                // If residents already exist, get them
                var residentEmails = residentNames.Select((_, i) => $"resident{i + 1}@community.com").ToList();
                residents = await context.Users
                    .Where(u => residentEmails.Contains(u.Email))
                    .ToListAsync();
            }

            // Seed Vehicles: 0-2 vehicles for each resident
            var vehicles = new List<Vehicle>();
            var vehicleBrands = new[] { "Toyota", "Honda", "Ford", "Chevrolet", "Nissan", "BMW", "Mercedes", "Audi" };
            var vehicleModels = new[] { "Corolla", "Civic", "Focus", "Malibu", "Sentra", "320i", "C-Class", "A4" };
            var vehicleColors = new[] { "Blanco", "Negro", "Gris", "Rojo", "Azul", "Plateado" };

            foreach (var resident in residents)
            {
                int vehicleCount = random.Next(0, 3); // 0 to 2 vehicles

                for (int i = 0; i < vehicleCount; i++)
                {
                    var brand = vehicleBrands[random.Next(vehicleBrands.Length)];
                    var model = vehicleModels[random.Next(vehicleModels.Length)];
                    var color = vehicleColors[random.Next(vehicleColors.Length)];
                    var year = random.Next(2015, 2024);
                    var licensePlate = $"{GetRandomLetters(random, 3)}{random.Next(100, 999)}";

                    // Check if license plate already exists
                    if (!await context.Vehicles.AnyAsync(v => v.LicensePlate == licensePlate))
                    {
                        vehicles.Add(new Vehicle
                        {
                            LicensePlate = licensePlate,
                            Brand = brand,
                            Model = model,
                            Color = color,
                            Year = year,
                            CommunityId = firstCommunity.Id,
                            OwnerId = resident.Id,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }
            }

            if (vehicles.Any())
            {
                await context.Vehicles.AddRangeAsync(vehicles);
                await context.SaveChangesAsync();
            }

            // Seed Pets: 0-2 pets for 5 residents
            var pets = new List<Pet>();
            var petTypes = new[] { "Perro", "Gato", "Conejo", "Ave" };
            var dogBreeds = new[] { "Labrador", "Pastor Alemán", "Bulldog", "Golden Retriever", "Chihuahua" };
            var catBreeds = new[] { "Persa", "Siames", "Maine Coon", "Bengalí", "British Shorthair" };
            var petColors = new[] { "Blanco", "Negro", "Marrón", "Gris", "Naranja", "Multicolor" };
            var petNames = new[] { "Max", "Luna", "Bella", "Charlie", "Daisy", "Rocky", "Molly", "Buddy", "Lucy", "Jack" };

            // Select 5 random residents for pets
            var residentsWithPets = residents.OrderBy(x => random.Next()).Take(5).ToList();

            foreach (var resident in residentsWithPets)
            {
                int petCount = random.Next(0, 3); // 0 to 2 pets

                for (int i = 0; i < petCount; i++)
                {
                    var petType = petTypes[random.Next(petTypes.Length)];
                    string? breed = null;

                    if (petType == "Perro")
                    {
                        breed = dogBreeds[random.Next(dogBreeds.Length)];
                    }
                    else if (petType == "Gato")
                    {
                        breed = catBreeds[random.Next(catBreeds.Length)];
                    }

                    pets.Add(new Pet
                    {
                        Name = petNames[random.Next(petNames.Length)],
                        Type = petType,
                        Breed = breed,
                        Color = petColors[random.Next(petColors.Length)],
                        CommunityId = firstCommunity.Id,
                        OwnerId = resident.Id,
                        RegistrationDate = DateTime.UtcNow.AddDays(-random.Next(1, 365)),
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            if (pets.Any())
            {
                await context.Pets.AddRangeAsync(pets);
                await context.SaveChangesAsync();
            }

            // Seed Resident Preferences for all residents
            var preferences = new List<ResidentPreference>();
            var preferenceNames = new[] { "Poda del pasto", "Forma de contacto", "Desea factura", "Datos fiscales", "etc" };
            
            // Different values for each preference type
            var podaValues = new[] { "Semanal", "Quincenal", "Mensual", "No deseo", "Según necesidad" };
            var contactoValues = new[] { "Email", "Teléfono", "WhatsApp", "SMS", "Presencial" };
            var facturaValues = new[] { "Sí", "No", "Ocasionalmente", "Solo servicios pagados" };
            var fiscalesValues = new[] { "RFC: ABC123456789", "RFC: XYZ987654321", "RFC: DEF456789012", "No proporcionados", "RFC: GHI789012345" };
            var etcValues = new[] { "Notificaciones por email", "Notificaciones por SMS", "Horario preferido: Mañana", "Horario preferido: Tarde", "Sin preferencias" };

            foreach (var resident in residents)
            {
                foreach (var preferenceName in preferenceNames)
                {
                    // Check if preference already exists
                    if (!await context.ResidentPreferences.AnyAsync(rp => rp.ResidentId == resident.Id && rp.Name == preferenceName))
                    {
                        string value;
                        
                        // Assign different values based on preference name
                        switch (preferenceName)
                        {
                            case "Poda del pasto":
                                value = podaValues[random.Next(podaValues.Length)];
                                break;
                            case "Forma de contacto":
                                value = contactoValues[random.Next(contactoValues.Length)];
                                break;
                            case "Desea factura":
                                value = facturaValues[random.Next(facturaValues.Length)];
                                break;
                            case "Datos fiscales":
                                value = fiscalesValues[random.Next(fiscalesValues.Length)];
                                break;
                            case "etc":
                                value = etcValues[random.Next(etcValues.Length)];
                                break;
                            default:
                                value = "Valor por defecto";
                                break;
                        }

                        preferences.Add(new ResidentPreference
                        {
                            ResidentId = resident.Id,
                            Name = preferenceName,
                            Value = value,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }
            }

            if (preferences.Any())
            {
                await context.ResidentPreferences.AddRangeAsync(preferences);
                await context.SaveChangesAsync();
            }

            // Seed 10 Resident Providers for the first community
            var residentProviders = new List<ResidentProvider>();
            var providerData = new[]
            {
                new { Name = "Express Delivery Services", ServiceType = "Delivery", Phone = "+1-555-3001", Email = "contact@expressdelivery.com" },
                new { Name = "Quick Maintenance Solutions", ServiceType = "Maintenance", Phone = "+1-555-3002", Email = "info@quickmaintenance.com" },
                new { Name = "Clean Home Services", ServiceType = "Cleaning", Phone = "+1-555-3003", Email = "service@cleanhome.com" },
                new { Name = "Secure Guard Services", ServiceType = "Security", Phone = "+1-555-3004", Email = "contact@secureguard.com" },
                new { Name = "Green Garden Landscaping", ServiceType = "Landscaping", Phone = "+1-555-3005", Email = "info@greengarden.com" },
                new { Name = "Fast Food Express", ServiceType = "Food Delivery", Phone = "+1-555-3006", Email = "orders@fastfoodexpress.com" },
                new { Name = "Tech Support Pro", ServiceType = "Technical Support", Phone = "+1-555-3007", Email = "support@techsupportpro.com" },
                new { Name = "Plumbing Experts", ServiceType = "Plumbing", Phone = "+1-555-3008", Email = "service@plumbingexperts.com" },
                new { Name = "Electric Solutions", ServiceType = "Electrical", Phone = "+1-555-3009", Email = "contact@electricsolutions.com" },
                new { Name = "Pet Care Services", ServiceType = "Pet Care", Phone = "+1-555-3010", Email = "info@petcareservices.com" }
            };

            foreach (var providerInfo in providerData)
            {
                if (!await context.ResidentProviders.AnyAsync(rp => rp.Name == providerInfo.Name && rp.CommunityId == firstCommunity.Id))
                {
                    residentProviders.Add(new ResidentProvider
                    {
                        Name = providerInfo.Name,
                        ServiceType = providerInfo.ServiceType,
                        Phone = providerInfo.Phone,
                        Email = providerInfo.Email,
                        CommunityId = firstCommunity.Id,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            if (residentProviders.Any())
            {
                await context.ResidentProviders.AddRangeAsync(residentProviders);
                await context.SaveChangesAsync();
            }

            // Seed Amenities for Community 1
            if (communities.Count >= 1)
            {
                var community1 = communities[0];
                var amenitiesCommunity1 = new List<Amenity>();

                var amenityDataCommunity1 = new[]
                {
                    new { Name = "Alberca", Description = "Alberca comunitaria para uso de todos los residentes", Cost = (decimal?)null },
                    new { Name = "Casa Club residentes", Description = "Espacio común para reuniones y actividades de residentes", Cost = (decimal?)0m },
                    new { Name = "Casa Club eventos", Description = "Salón de eventos disponible para renta", Cost = (decimal?)1500m }
                };

                foreach (var amenityInfo in amenityDataCommunity1)
                {
                    if (!await context.Amenities.AnyAsync(a => a.Name == amenityInfo.Name && a.CommunityId == community1.Id))
                    {
                        amenitiesCommunity1.Add(new Amenity
                        {
                            Name = amenityInfo.Name,
                            Description = amenityInfo.Description,
                            Cost = amenityInfo.Cost,
                            CommunityId = community1.Id,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                if (amenitiesCommunity1.Any())
                {
                    await context.Amenities.AddRangeAsync(amenitiesCommunity1);
                    await context.SaveChangesAsync();
                }
            }

            // Seed Amenities for Community 2
            if (communities.Count >= 2)
            {
                var secondCommunity = communities[1];
                var amenitiesCommunity2 = new List<Amenity>();

                var amenityDataCommunity2 = new[]
                {
                    new { Name = "Alberca", Description = "Alberca comunitaria para uso de todos los residentes", Cost = (decimal?)null },
                    new { Name = "Casa Club residentes", Description = "Espacio común para reuniones y actividades de residentes", Cost = (decimal?)0m },
                    new { Name = "Casa Club eventos", Description = "Salón de eventos disponible para renta", Cost = (decimal?)1500m },
                    new { Name = "Gimnasio", Description = "Gimnasio equipado con máquinas de ejercicio", Cost = (decimal?)null }
                };

                foreach (var amenityInfo in amenityDataCommunity2)
                {
                    if (!await context.Amenities.AnyAsync(a => a.Name == amenityInfo.Name && a.CommunityId == secondCommunity.Id))
                    {
                        amenitiesCommunity2.Add(new Amenity
                        {
                            Name = amenityInfo.Name,
                            Description = amenityInfo.Description,
                            Cost = amenityInfo.Cost,
                            CommunityId = secondCommunity.Id,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                if (amenitiesCommunity2.Any())
                {
                    await context.Amenities.AddRangeAsync(amenitiesCommunity2);
                    await context.SaveChangesAsync();
                }
            }
        }
    }

    private static string GetRandomLetters(Random random, int count)
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Range(0, count)
            .Select(_ => letters[random.Next(letters.Length)])
            .ToArray());
    }
}

