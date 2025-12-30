# GreatSoft Backend API

API REST para gestión de comunidades residenciales desarrollada con .NET 8.0 y arquitectura limpia.

## Características

- ✅ Autenticación JWT
- ✅ Gestión de usuarios y roles
- ✅ Gestión de empresas y comunidades
- ✅ Registro de mascotas
- ✅ Registro de vehículos
- ✅ Gestión de proveedores de residentes
- ✅ Control de visitas de residentes
- ✅ Arquitectura limpia (Clean Architecture)
- ✅ Entity Framework Core
- ✅ Swagger/OpenAPI

## Estructura del Proyecto

```
GreatSoft.Be.sln
├── GreatSoft.Be.API/              # Capa de presentación (Controllers)
├── GreatSoft.Be.Application/      # Capa de aplicación (Services, DTOs)
├── GreatSoft.Be.Domain/           # Capa de dominio (Entities)
└── GreatSoft.Be.Infrastructure/   # Capa de infraestructura (Data, Repositories)
```

## Requisitos

- .NET 8.0 SDK
- SQL Server (opcional, puede usar InMemory para desarrollo)

## Configuración

1. Clonar el repositorio
2. Abrir la solución en Visual Studio o VS Code
3. Configurar la cadena de conexión en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GreatSoftDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

Si no se proporciona una cadena de conexión, se usará una base de datos en memoria para desarrollo.

4. Configurar JWT en `appsettings.json`:

```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLong!ChangeThisInProduction",
    "Issuer": "GreatSoft",
    "Audience": "GreatSoftUsers",
    "ExpirationMinutes": "60"
  }
}
```

## Migraciones de Base de Datos

Para crear la base de datos usando Entity Framework Core:

```bash
# Desde la carpeta del proyecto Infrastructure o desde la raíz
dotnet ef migrations add InitialCreate --project GreatSoft.Be.Infrastructure --startup-project GreatSoft.Be.API
dotnet ef database update --project GreatSoft.Be.Infrastructure --startup-project GreatSoft.Be.API
```

## Ejecutar la Aplicación

```bash
dotnet run --project GreatSoft.Be.API
```

La API estará disponible en:
- HTTP: `http://localhost:5080`
- HTTPS: `https://localhost:7282`
- Swagger UI: `https://localhost:7282/swagger`

## Endpoints Principales

### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar nuevo usuario

### Usuarios
- `GET /api/users` - Listar usuarios
- `GET /api/users/{id}` - Obtener usuario por ID
- `POST /api/users` - Crear usuario (Requiere Admin)
- `PUT /api/users/{id}` - Actualizar usuario (Requiere Admin)
- `DELETE /api/users/{id}` - Eliminar usuario (Requiere Admin)

### Roles
- `GET /api/roles` - Listar roles
- `GET /api/roles/{id}` - Obtener rol por ID
- `POST /api/roles` - Crear rol (Requiere Admin)
- `PUT /api/roles/{id}` - Actualizar rol (Requiere Admin)
- `DELETE /api/roles/{id}` - Eliminar rol (Requiere Admin)

### Empresas
- `GET /api/companies` - Listar empresas
- `GET /api/companies/{id}` - Obtener empresa por ID
- `POST /api/companies` - Crear empresa (Requiere Admin)
- `PUT /api/companies/{id}` - Actualizar empresa (Requiere Admin)
- `DELETE /api/companies/{id}` - Eliminar empresa (Requiere Admin)

### Comunidades
- `GET /api/communities` - Listar comunidades
- `GET /api/communities/{id}` - Obtener comunidad por ID
- `POST /api/communities` - Crear comunidad (Requiere Admin)
- `PUT /api/communities/{id}` - Actualizar comunidad (Requiere Admin)
- `DELETE /api/communities/{id}` - Eliminar comunidad (Requiere Admin)

### Mascotas
- `GET /api/pets` - Listar mascotas
- `GET /api/pets/{id}` - Obtener mascota por ID
- `POST /api/pets` - Registrar mascota
- `PUT /api/pets/{id}` - Actualizar mascota
- `DELETE /api/pets/{id}` - Eliminar mascota (Requiere Admin)

### Vehículos
- `GET /api/vehicles` - Listar vehículos
- `GET /api/vehicles/{id}` - Obtener vehículo por ID
- `POST /api/vehicles` - Registrar vehículo
- `PUT /api/vehicles/{id}` - Actualizar vehículo
- `DELETE /api/vehicles/{id}` - Eliminar vehículo (Requiere Admin)

### Proveedores de Residentes
- `GET /api/residentproviders` - Listar proveedores
- `GET /api/residentproviders/{id}` - Obtener proveedor por ID
- `POST /api/residentproviders` - Crear proveedor (Requiere Admin)
- `PUT /api/residentproviders/{id}` - Actualizar proveedor (Requiere Admin)
- `DELETE /api/residentproviders/{id}` - Eliminar proveedor (Requiere Admin)

### Visitas de Residentes
- `GET /api/residentvisits` - Listar visitas
- `GET /api/residentvisits/{id}` - Obtener visita por ID
- `POST /api/residentvisits` - Crear visita
- `PUT /api/residentvisits/{id}` - Actualizar visita (Requiere Admin)
- `DELETE /api/residentvisits/{id}` - Eliminar visita (Requiere Admin)

## Autenticación

La mayoría de los endpoints requieren autenticación JWT. Para usar los endpoints protegidos:

1. Obtén un token mediante `/api/auth/login`
2. Incluye el token en el header `Authorization`:
   ```
   Authorization: Bearer {tu_token}
   ```

## Notas

- En producción, cambia la `SecretKey` del JWT por una clave segura
- La base de datos en memoria se reinicia cada vez que se reinicia la aplicación
- Para producción, usa SQL Server o PostgreSQL configurando la cadena de conexión apropiada

## Tecnologías Utilizadas

- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core 8.0
- JWT Bearer Authentication
- BCrypt.Net
- Swagger/OpenAPI

