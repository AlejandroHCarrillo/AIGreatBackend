using GreatSoft.Be.Application.Interfaces;
using GreatSoft.Be.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreatSoft.Be.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task EnsureDatabaseCreatedAsync(ApplicationDbContext context)
    {
        // Delete database if exists
        await context.Database.EnsureDeletedAsync();
        
        // Create database
        await context.Database.EnsureCreatedAsync();
    }

    public static async Task SeedDataAsync(ApplicationDbContext context, IPasswordService passwordService)
    {
        // Check if data already exists
        if (context.Roles.Any() || context.CommunityTypes.Any() || context.VehicleTypes.Any() || context.ProviderServiceTypes.Any())
        {
            return;
        }
        
        // Create ProviderServiceTypes
        var providerServiceTypes = new[]
        {
            new ProviderServiceType
            {
                Id = Guid.NewGuid(),
                Code = "COMIDA",
                Name = "Comida",
                CreatedAt = DateTime.UtcNow
            },
            new ProviderServiceType
            {
                Id = Guid.NewGuid(),
                Code = "ASEO",
                Name = "Aseo",
                CreatedAt = DateTime.UtcNow
            },
            new ProviderServiceType
            {
                Id = Guid.NewGuid(),
                Code = "JARDINERIA",
                Name = "Jardinería",
                CreatedAt = DateTime.UtcNow
            },
            new ProviderServiceType
            {
                Id = Guid.NewGuid(),
                Code = "PLOMERIA",
                Name = "Plomería",
                CreatedAt = DateTime.UtcNow
            },
            new ProviderServiceType
            {
                Id = Guid.NewGuid(),
                Code = "ALBANILERIA",
                Name = "Albañilería",
                CreatedAt = DateTime.UtcNow
            },
            new ProviderServiceType
            {
                Id = Guid.NewGuid(),
                Code = "GAS",
                Name = "Gas",
                CreatedAt = DateTime.UtcNow
            },
            new ProviderServiceType
            {
                Id = Guid.NewGuid(),
                Code = "ELECTRICIDAD",
                Name = "Electricidad",
                CreatedAt = DateTime.UtcNow
            },
            new ProviderServiceType
            {
                Id = Guid.NewGuid(),
                Code = "PINTURA",
                Name = "Pintura",
                CreatedAt = DateTime.UtcNow
            }
        };
        
        context.ProviderServiceTypes.AddRange(providerServiceTypes);
        await context.SaveChangesAsync();
        
        // Create VehicleTypes
        var vehicleTypes = new[]
        {
            new VehicleType
            {
                Id = Guid.NewGuid(),
                Code = "AUTO",
                Name = "Auto",
                CreatedAt = DateTime.UtcNow
            },
            new VehicleType
            {
                Id = Guid.NewGuid(),
                Code = "MOTOCICLETA",
                Name = "Motocicleta",
                CreatedAt = DateTime.UtcNow
            },
            new VehicleType
            {
                Id = Guid.NewGuid(),
                Code = "CAMIONETA",
                Name = "Camioneta",
                CreatedAt = DateTime.UtcNow
            },
            new VehicleType
            {
                Id = Guid.NewGuid(),
                Code = "SUV",
                Name = "SUV",
                CreatedAt = DateTime.UtcNow
            },
            new VehicleType
            {
                Id = Guid.NewGuid(),
                Code = "MOTO",
                Name = "Moto",
                CreatedAt = DateTime.UtcNow
            }
        };
        
        context.VehicleTypes.AddRange(vehicleTypes);
        await context.SaveChangesAsync();
        
        // Create CommunityTypes
        var communityTypes = new[]
        {
            new CommunityType
            {
                Id = Guid.NewGuid(),
                Code = "COLONIA",
                Name = "Colonia",
                CreatedAt = DateTime.UtcNow
            },
            new CommunityType
            {
                Id = Guid.NewGuid(),
                Code = "FRACCIONAMIENTO",
                Name = "Fraccionamiento",
                CreatedAt = DateTime.UtcNow
            },
            new CommunityType
            {
                Id = Guid.NewGuid(),
                Code = "COTO",
                Name = "Coto",
                CreatedAt = DateTime.UtcNow
            },
            new CommunityType
            {
                Id = Guid.NewGuid(),
                Code = "EDIFICIO",
                Name = "Edificio",
                CreatedAt = DateTime.UtcNow
            },
            new CommunityType
            {
                Id = Guid.NewGuid(),
                Code = "CONDOMINIO",
                Name = "Condominio",
                CreatedAt = DateTime.UtcNow
            },
            new CommunityType
            {
                Id = Guid.NewGuid(),
                Code = "COMUNIDAD",
                Name = "Comunidad",
                CreatedAt = DateTime.UtcNow
            }
        };
        
        context.CommunityTypes.AddRange(communityTypes);
        await context.SaveChangesAsync();
        
        // Create company with specific ID
        var companyId = Guid.Parse("f713434b-62ff-4057-8caa-1b2bfa356ef5");
        var company = new Company
        {
            Id = companyId,
            Name = "Compañía de Administración Happy Habitat",
            Address = "Calle Principal 123, Madrid",
            ContactName = "María García",
            Phone = "+34 123 456 789",
            Email = "contacto@happyhabitat.com",
            CreatedAt = DateTime.UtcNow
        };
        
        context.Companies.Add(company);
        await context.SaveChangesAsync();

        // Create roles
        var roles = new[]
        {
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Description = "Administrator role with full access",
                RoleType = "Admin",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "SysAdmin",
                Description = "System Administrator role",
                RoleType = "SysAdmin",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Manager",
                Description = "Manager role",
                RoleType = "Manager",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Resident",
                Description = "Resident role",
                RoleType = "Resident",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "ResidentPower",
                Description = "Resident Power role",
                RoleType = "ResidentPower",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Vigilance",
                Description = "Vigilance role",
                RoleType = "Vigilance",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Supervision",
                Description = "Supervision role",
                RoleType = "Supervision",
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Roles.AddRange(roles);
        await context.SaveChangesAsync();

        // Get Admin role
        var adminRole = roles.First(r => r.Name == "Admin");
        var sysAdminRole = roles.First(r => r.Name == "SysAdmin");

        // Create admin user
        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Admin",
            LastName = "User",
            Username = "elgrandeahc",
            Email = "admin@greatsoft.com",
            PasswordHash = passwordService.HashPassword("ahc123"),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            RoleId = adminRole.Id
        };

        // Create SysAdmin user
        var sysAdminUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "SysAdmin",
            LastName = "User",
            Username = "sysadmin",
            Email = "sysadmin@greatsoft.com",
            PasswordHash = passwordService.HashPassword("sysadmin123"),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            RoleId = sysAdminRole.Id
        };

        context.Users.Add(adminUser);
        context.Users.Add(sysAdminUser);
        await context.SaveChangesAsync();
        
        // Create 2 Admin users linked to the company
        var adminRoleForCompany = roles.First(r => r.Name == "Admin");
        
        var companyAdminUser1 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Juan",
            LastName = "Pérez",
            Username = "juan.perez",
            Email = "juan.perez@happyhabitat.com",
            PasswordHash = passwordService.HashPassword("admin123"),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            RoleId = adminRoleForCompany.Id
        };
        
        var companyAdminUser2 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Ana",
            LastName = "Martínez",
            Username = "ana.martinez",
            Email = "ana.martinez@happyhabitat.com",
            PasswordHash = passwordService.HashPassword("admin123"),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            RoleId = adminRoleForCompany.Id
        };
        
        context.Users.Add(companyAdminUser1);
        context.Users.Add(companyAdminUser2);
        await context.SaveChangesAsync();
        
        // Create CompanyUser relationships for Admin users
        var companyAdminUser1Relation = new CompanyUser
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            UserId = companyAdminUser1.Id,
            CreatedAt = DateTime.UtcNow
        };
        
        var companyAdminUser2Relation = new CompanyUser
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            UserId = companyAdminUser2.Id,
            CreatedAt = DateTime.UtcNow
        };
        
        context.CompanyUsers.Add(companyAdminUser1Relation);
        context.CompanyUsers.Add(companyAdminUser2Relation);
        await context.SaveChangesAsync();
        
        // Create 2 Manager users linked to the company
        var managerRole = roles.First(r => r.Name == "Manager");
        
        var companyManagerUser1 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Carlos",
            LastName = "Rodríguez",
            Username = "carlos.rodriguez",
            Email = "carlos.rodriguez@happyhabitat.com",
            PasswordHash = passwordService.HashPassword("manager123"),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            RoleId = managerRole.Id
        };
        
        var companyManagerUser2 = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Laura",
            LastName = "Sánchez",
            Username = "laura.sanchez",
            Email = "laura.sanchez@happyhabitat.com",
            PasswordHash = passwordService.HashPassword("manager123"),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            RoleId = managerRole.Id
        };
        
        context.Users.Add(companyManagerUser1);
        context.Users.Add(companyManagerUser2);
        await context.SaveChangesAsync();
        
        // Create CompanyUser relationships for Manager users
        var companyManagerUser1Relation = new CompanyUser
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            UserId = companyManagerUser1.Id,
            CreatedAt = DateTime.UtcNow
        };
        
        var companyManagerUser2Relation = new CompanyUser
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            UserId = companyManagerUser2.Id,
            CreatedAt = DateTime.UtcNow
        };
        
        context.CompanyUsers.Add(companyManagerUser1Relation);
        context.CompanyUsers.Add(companyManagerUser2Relation);
        await context.SaveChangesAsync();
        
        // Create first community
        var firstCommunityType = communityTypes.First(ct => ct.Code == "FRACCIONAMIENTO");
        var firstCommunity = new Community
        {
            Id = Guid.NewGuid(),
            CommunityTypeId = firstCommunityType.Id,
            Name = "Fraccionamiento Las Flores",
            Location = "Avenida Principal 456, Madrid, España",
            Lat = 402416,
            Lng = -3704,
            HousingCount = 50,
            ContactPhone = "+34 987 654 321",
            ContactEmail = "contacto@lasflores.com",
            CreatedAt = DateTime.UtcNow
        };
        
        context.Communities.Add(firstCommunity);
        await context.SaveChangesAsync();
        
        // Get Resident role
        var residentRole = roles.First(r => r.Name == "Resident");
        
        // Create 10 residents linked to the first community
        var residents = new[]
        {
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "María González López",
                Email = "maria.gonzalez@email.com",
                Phone = "+34 600 111 222",
                HouseNumber = "101",
                Address = "Calle Rosas 101, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "José Martínez Ruiz",
                Email = "jose.martinez@email.com",
                Phone = "+34 600 222 333",
                HouseNumber = "102",
                Address = "Calle Rosas 102, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "Ana Fernández García",
                Email = "ana.fernandez@email.com",
                Phone = "+34 600 333 444",
                HouseNumber = "103",
                Address = "Calle Rosas 103, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "Carlos Sánchez Pérez",
                Email = "carlos.sanchez@email.com",
                Phone = "+34 600 444 555",
                HouseNumber = "104",
                Address = "Calle Rosas 104, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "Laura Rodríguez Torres",
                Email = "laura.rodriguez@email.com",
                Phone = "+34 600 555 666",
                HouseNumber = "105",
                Address = "Calle Rosas 105, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "Pedro Jiménez Moreno",
                Email = "pedro.jimenez@email.com",
                Phone = "+34 600 666 777",
                HouseNumber = "106",
                Address = "Calle Rosas 106, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "Isabel Díaz Hernández",
                Email = "isabel.diaz@email.com",
                Phone = "+34 600 777 888",
                HouseNumber = "107",
                Address = "Calle Rosas 107, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "Miguel Ángel López Martín",
                Email = "miguel.lopez@email.com",
                Phone = "+34 600 888 999",
                HouseNumber = "108",
                Address = "Calle Rosas 108, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "Carmen Ruiz Gómez",
                Email = "carmen.ruiz@email.com",
                Phone = "+34 600 999 000",
                HouseNumber = "109",
                Address = "Calle Rosas 109, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            },
            new Resident
            {
                Id = Guid.NewGuid(),
                FullName = "Francisco Javier Serrano Castro",
                Email = "francisco.serrano@email.com",
                Phone = "+34 600 000 111",
                HouseNumber = "110",
                Address = "Calle Rosas 110, Fraccionamiento Las Flores",
                CommunityId = firstCommunity.Id,
                RoleId = residentRole.Id,
                CreatedAt = DateTime.UtcNow
            }
        };
        
        context.Residents.AddRange(residents);
        await context.SaveChangesAsync();
        
        // Create users for each resident and link them
        var residentUsers = new List<User>();
        var residentUserRelations = new List<ResidentUser>();
        
        for (int i = 0; i < residents.Length; i++)
        {
            var resident = residents[i];
            var nameParts = resident.FullName.Split(' ');
            var firstName = nameParts[0];
            var lastName = nameParts.Length > 1 ? nameParts[nameParts.Length - 1] : "";
            var username = $"{firstName.ToLower()}.{lastName.ToLower().Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")}";
            
            // Ensure unique username
            var baseUsername = username;
            var counter = 1;
            while (residentUsers.Any(u => u.Username == username))
            {
                username = $"{baseUsername}{counter}";
                counter++;
            }
            
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = resident.Email ?? $"{username}@email.com",
                PasswordHash = passwordService.HashPassword("resident123"),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                RoleId = residentRole.Id
            };
            
            residentUsers.Add(user);
            
            // Create ResidentUser relationship
            var residentUserRelation = new ResidentUser
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ResidentId = resident.Id,
                CreatedAt = DateTime.UtcNow
            };
            
            residentUserRelations.Add(residentUserRelation);
        }
        
        context.Users.AddRange(residentUsers);
        await context.SaveChangesAsync();
        
        context.ResidentUsers.AddRange(residentUserRelations);
        await context.SaveChangesAsync();
        
        // Create vehicles for residents (0-2 vehicles per resident)
        var random = new Random();
        var vehicles = new List<Vehicle>();
        var usedLicensePlates = new HashSet<string>();
        
        // Vehicle data for random generation
        var brands = new[] { "Toyota", "Ford", "Volkswagen", "Nissan", "Honda", "Chevrolet", "BMW", "Mercedes-Benz", "Audi", "Hyundai" };
        var models = new Dictionary<string, string[]>
        {
            { "Toyota", new[] { "Corolla", "Camry", "RAV4", "Highlander", "Prius" } },
            { "Ford", new[] { "F-150", "Mustang", "Explorer", "Focus", "Escape" } },
            { "Volkswagen", new[] { "Jetta", "Passat", "Tiguan", "Golf", "Atlas" } },
            { "Nissan", new[] { "Sentra", "Altima", "Rogue", "Pathfinder", "Frontier" } },
            { "Honda", new[] { "Civic", "Accord", "CR-V", "Pilot", "HR-V" } },
            { "Chevrolet", new[] { "Silverado", "Equinox", "Malibu", "Tahoe", "Traverse" } },
            { "BMW", new[] { "3 Series", "5 Series", "X3", "X5", "X1" } },
            { "Mercedes-Benz", new[] { "C-Class", "E-Class", "GLC", "GLE", "A-Class" } },
            { "Audi", new[] { "A4", "A6", "Q5", "Q7", "A3" } },
            { "Hyundai", new[] { "Elantra", "Sonata", "Tucson", "Santa Fe", "Kona" } }
        };
        
        var colors = new[] { "Blanco", "Negro", "Gris", "Plateado", "Azul", "Rojo", "Verde", "Beige" };
        var autoType = vehicleTypes.First(vt => vt.Code == "AUTO");
        var motoType = vehicleTypes.First(vt => vt.Code == "MOTOCICLETA");
        var camionetaType = vehicleTypes.First(vt => vt.Code == "CAMIONETA");
        var suvType = vehicleTypes.First(vt => vt.Code == "SUV");
        
        foreach (var resident in residents)
        {
            var vehicleCount = random.Next(0, 3); // 0, 1, or 2 vehicles
            
            for (int i = 0; i < vehicleCount; i++)
            {
                var brand = brands[random.Next(brands.Length)];
                var model = models[brand][random.Next(models[brand].Length)];
                var color = colors[random.Next(colors.Length)];
                var year = random.Next(2015, 2024);
                
                // Generate unique license plate
                string licensePlate;
                do
                {
                    var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    var numbers = "0123456789";
                    var plateLetters = new string(Enumerable.Range(0, 3).Select(_ => letters[random.Next(letters.Length)]).ToArray());
                    var plateNumbers = new string(Enumerable.Range(0, 3).Select(_ => numbers[random.Next(numbers.Length)]).ToArray());
                    licensePlate = $"{plateLetters}{plateNumbers}";
                } while (usedLicensePlates.Contains(licensePlate));
                
                usedLicensePlates.Add(licensePlate);
                
                // Select vehicle type based on brand/model (simplified logic)
                VehicleType vehicleType;
                if (brand == "BMW" || brand == "Mercedes-Benz" || brand == "Audi")
                {
                    vehicleType = random.Next(2) == 0 ? autoType : suvType;
                }
                else if (model.Contains("F-150") || model.Contains("Silverado") || model.Contains("Pathfinder") || model.Contains("Tahoe"))
                {
                    vehicleType = camionetaType;
                }
                else if (model.Contains("RAV4") || model.Contains("CR-V") || model.Contains("Tiguan") || model.Contains("Tucson") || model.Contains("X3") || model.Contains("X5") || model.Contains("GLC") || model.Contains("GLE") || model.Contains("Q5") || model.Contains("Q7"))
                {
                    vehicleType = suvType;
                }
                else
                {
                    vehicleType = autoType;
                }
                
                var vehicle = new Vehicle
                {
                    Id = Guid.NewGuid(),
                    ResidentId = resident.Id,
                    Brand = brand,
                    VehicleTypeId = vehicleType.Id,
                    Model = model,
                    Year = year,
                    Color = color,
                    LicensePlate = licensePlate,
                    CreatedAt = DateTime.UtcNow
                };
                
                vehicles.Add(vehicle);
            }
        }
        
        if (vehicles.Any())
        {
            context.Vehicles.AddRange(vehicles);
            await context.SaveChangesAsync();
        }
        
        // Create pets for some residents (0-2 pets per resident)
        var pets = new List<Pet>();
        
        // Pet data for random generation
        var petNames = new[] { "Max", "Luna", "Bella", "Charlie", "Lucy", "Cooper", "Daisy", "Rocky", "Molly", "Buddy", "Lola", "Jack", "Sophie", "Toby", "Chloe", "Oscar", "Mia", "Bear", "Zoe", "Duke" };
        var dogBreeds = new[] { "Labrador", "Pastor Alemán", "Golden Retriever", "Bulldog", "Beagle", "Poodle", "Rottweiler", "Yorkshire Terrier", "Dachshund", "Siberian Husky" };
        var catBreeds = new[] { "Siamés", "Persa", "Maine Coon", "British Shorthair", "Ragdoll", "Bengal", "Abyssinian", "Scottish Fold", "Russian Blue", "American Shorthair" };
        var birdSpecies = new[] { "Canario", "Periquito", "Cockatiel", "Agapornis", "Diamante Mandarín" };
        var petColors = new Dictionary<string, string[]>
        {
            { "Perro", new[] { "Negro", "Marrón", "Blanco", "Dorado", "Gris", "Atigrado", "Crema", "Negro y Blanco" } },
            { "Gato", new[] { "Blanco", "Negro", "Gris", "Naranja", "Atigrado", "Calicó", "Crema", "Marrón" } },
            { "Ave", new[] { "Amarillo", "Verde", "Azul", "Blanco", "Multicolor", "Gris", "Naranja", "Rojo" } }
        };
        
        foreach (var resident in residents)
        {
            // 70% chance that a resident has pets (some residents may not have pets)
            if (random.Next(100) < 70)
            {
                var petCount = random.Next(0, 3); // 0, 1, or 2 pets
                
                for (int i = 0; i < petCount; i++)
                {
                    // Determine species (70% dogs, 25% cats, 5% birds)
                    var speciesRoll = random.Next(100);
                    string species;
                    string breed;
                    
                    if (speciesRoll < 70)
                    {
                        species = "Perro";
                        breed = dogBreeds[random.Next(dogBreeds.Length)];
                    }
                    else if (speciesRoll < 95)
                    {
                        species = "Gato";
                        breed = catBreeds[random.Next(catBreeds.Length)];
                    }
                    else
                    {
                        species = "Ave";
                        breed = birdSpecies[random.Next(birdSpecies.Length)];
                    }
                    
                    var name = petNames[random.Next(petNames.Length)];
                    var age = random.Next(1, 15); // Age between 1 and 14 years
                    var availableColors = petColors[species];
                    var color = availableColors[random.Next(availableColors.Length)];
                    
                    var pet = new Pet
                    {
                        Id = Guid.NewGuid(),
                        ResidentId = resident.Id,
                        Name = name,
                        Species = species,
                        Breed = breed,
                        Age = age,
                        Color = color,
                        CreatedAt = DateTime.UtcNow
                    };
                    
                    pets.Add(pet);
                }
            }
        }
        
        if (pets.Any())
        {
            context.Pets.AddRange(pets);
            await context.SaveChangesAsync();
        }
        
        // Create visits for 4 residents
        var visits = new List<ResidentVisit>();
        var visitSubjects = new[] 
        { 
            "Visita familiar", 
            "Entrega de paquete", 
            "Reunión de trabajo", 
            "Visita social", 
            "Mantenimiento de servicios",
            "Entrega de comida",
            "Visita médica",
            "Reunión de vecinos"
        };
        var vehicleColors = new[] { "Blanco", "Negro", "Gris", "Plateado", "Azul", "Rojo", "Verde" };
        
        // Select first 4 residents
        var residentsWithVisits = residents.Take(4).ToArray();
        
        foreach (var resident in residentsWithVisits)
        {
            // Create 2-4 visits per resident
            var visitCount = random.Next(2, 5);
            
            for (int i = 0; i < visitCount; i++)
            {
                var daysAgo = random.Next(0, 30); // Visits from last 30 days
                var arrivalDate = DateTime.UtcNow.AddDays(-daysAgo).AddHours(random.Next(8, 20)).AddMinutes(random.Next(0, 60));
                
                // 80% chance the visit has ended, 20% still in progress
                DateTime? departureDate = null;
                if (random.Next(100) < 80)
                {
                    var visitDuration = TimeSpan.FromHours(random.Next(1, 6)); // Visit duration 1-6 hours
                    departureDate = arrivalDate.Add(visitDuration);
                }
                
                var hasVehicle = random.Next(100) < 60; // 60% chance visitor has a vehicle
                string? vehicleColor = null;
                string? licensePlate = null;
                
                if (hasVehicle)
                {
                    vehicleColor = vehicleColors[random.Next(vehicleColors.Length)];
                    // Generate license plate
                    var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    var numbers = "0123456789";
                    var plateLetters = new string(Enumerable.Range(0, 3).Select(_ => letters[random.Next(letters.Length)]).ToArray());
                    var plateNumbers = new string(Enumerable.Range(0, 3).Select(_ => numbers[random.Next(numbers.Length)]).ToArray());
                    licensePlate = $"{plateLetters}{plateNumbers}";
                }
                
                var visitorNames = new[] 
                { 
                    "Carlos Méndez", 
                    "Ana López", 
                    "Roberto Sánchez", 
                    "María Fernández", 
                    "Pedro Martínez",
                    "Laura García",
                    "Juan Rodríguez",
                    "Sofía Torres"
                };
                
                var visit = new ResidentVisit
                {
                    Id = Guid.NewGuid(),
                    ResidentId = resident.Id,
                    VisitorName = visitorNames[random.Next(visitorNames.Length)],
                    TotalPeople = random.Next(1, 4), // 1-3 people
                    VehicleColor = vehicleColor,
                    LicensePlate = licensePlate,
                    Subject = visitSubjects[random.Next(visitSubjects.Length)],
                    ArrivalDate = arrivalDate,
                    DepartureDate = departureDate,
                    CreatedAt = DateTime.UtcNow
                };
                
                visits.Add(visit);
            }
        }
        
        if (visits.Any())
        {
            context.ResidentVisits.AddRange(visits);
            await context.SaveChangesAsync();
        }
        
        // Create 10 providers for residents
        var providers = new[]
        {
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Restaurante El Buen Sabor",
                Description = "Servicio de comida a domicilio con menú variado y opciones saludables. Especializados en comida mexicana e internacional.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "COMIDA").Id,
                Phone = "+34 911 234 567",
                Email = "contacto@buensabor.com",
                Image = "/images/providers/buensabor.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Limpieza Profesional Express",
                Description = "Servicio de limpieza doméstica y comercial. Personal capacitado, productos ecológicos y horarios flexibles.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "ASEO").Id,
                Phone = "+34 912 345 678",
                Email = "info@limpiezaexpress.com",
                Image = "/images/providers/limpieza.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Jardines y Paisajismo Verde",
                Description = "Diseño, mantenimiento y cuidado de jardines. Podas, riego automático, fertilización y control de plagas.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "JARDINERIA").Id,
                Phone = "+34 913 456 789",
                Email = "contacto@jardinesverde.com",
                Image = "/images/providers/jardineria.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Fontanería Rápida 24/7",
                Description = "Servicio de plomería de emergencia las 24 horas. Reparaciones, instalaciones y mantenimiento preventivo.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "PLOMERIA").Id,
                Phone = "+34 914 567 890",
                Email = "emergencias@fontaneria24.com",
                Image = "/images/providers/plomeria.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Construcciones y Reformas Martínez",
                Description = "Servicios de albañilería, construcción y reformas. Trabajos de calidad con garantía y presupuestos sin compromiso.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "ALBANILERIA").Id,
                Phone = "+34 915 678 901",
                Email = "presupuestos@construccionesmartinez.com",
                Image = "/images/providers/albanileria.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Gas Seguro y Rápido",
                Description = "Instalación, mantenimiento y reparación de sistemas de gas. Certificados y con todas las garantías de seguridad.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "GAS").Id,
                Phone = "+34 916 789 012",
                Email = "servicio@gasseguro.com",
                Image = "/images/providers/gas.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Pizza Delivery Express",
                Description = "Pizzas artesanales recién horneadas entregadas en menos de 30 minutos. Amplia variedad de sabores y tamaños.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "COMIDA").Id,
                Phone = "+34 917 890 123",
                Email = "pedidos@pizzaexpress.com",
                Image = "/images/providers/pizza.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Electricistas Certificados Pro",
                Description = "Instalaciones eléctricas, reparaciones y mantenimiento. Electricistas certificados con años de experiencia.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "ELECTRICIDAD").Id,
                Phone = "+34 918 901 234",
                Email = "contacto@electricistaspro.com",
                Image = "/images/providers/electricidad.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Pinturas y Decoración Premium",
                Description = "Servicio profesional de pintura interior y exterior. Preparación de superficies, pinturas de calidad y acabados perfectos.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "PINTURA").Id,
                Phone = "+34 919 012 345",
                Email = "presupuestos@pinturaspremium.com",
                Image = "/images/providers/pintura.jpg",
                CreatedAt = DateTime.UtcNow
            },
            new ResidentProvider
            {
                Id = Guid.NewGuid(),
                Name = "Limpieza Profunda Especializada",
                Description = "Servicio de limpieza profunda para hogares y oficinas. Limpieza de alfombras, ventanas, cristales y más.",
                ProviderServiceTypeId = providerServiceTypes.First(pst => pst.Code == "ASEO").Id,
                Phone = "+34 920 123 456",
                Email = "info@limpiezaprofunda.com",
                Image = "/images/providers/limpieza-profunda.jpg",
                CreatedAt = DateTime.UtcNow
            }
        };
        
        context.ResidentProviders.AddRange(providers);
        await context.SaveChangesAsync();
    }
}

