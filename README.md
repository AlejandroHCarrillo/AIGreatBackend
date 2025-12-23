# GreatSoft.Be - Backend API

Solución .NET con Clean Architecture que implementa un sistema de autenticación JWT con gestión de usuarios y roles.

## Estructura del Proyecto

La solución está organizada siguiendo los principios de Clean Architecture con las siguientes capas:

- **GreatSoft.Be.Domain**: Entidades del dominio (User, Role)
- **GreatSoft.Be.Application**: Casos de uso, DTOs, interfaces y servicios de aplicación
- **GreatSoft.Be.Infrastructure**: Implementaciones (repositorios, servicios JWT, Entity Framework)
- **GreatSoft.Be.API**: Controladores, configuración de Swagger y punto de entrada

## Características

- ✅ Autenticación JWT
- ✅ Sistema de usuarios y roles
- ✅ Swagger configurado con autenticación JWT
- ✅ CRUD completo para usuarios
- ✅ CRUD completo para roles
- ✅ Registro, login y recuperación de contraseña
- ✅ Usuario admin por defecto

## Roles Disponibles

Los siguientes roles están predefinidos en el sistema:

- **Admin**: Administrador con acceso completo
- **SysAdmin**: Administrador del sistema
- **Manager**: Gerente
- **Resident**: Residente
- **ResidentPower**: Residente con permisos extendidos
- **Vigilance**: Vigilancia
- **Supervision**: Supervisión

## Usuario Admin por Defecto

El sistema incluye un usuario administrador preconfigurado:

- **Username**: `elgrandeahc`
- **Password**: `ahc123`
- **Rol**: Admin

## Configuración

### JWT Settings

La configuración de JWT se encuentra en `appsettings.json`:

```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLong!",
    "Issuer": "GreatSoft.Be",
    "Audience": "GreatSoft.Be",
    "ExpirationMinutes": "1440"
  }
}
```

**Nota**: En producción, cambia el `SecretKey` por una clave segura y guárdala de forma segura (por ejemplo, en Azure Key Vault o variables de entorno).

## Ejecución

1. Restaurar paquetes NuGet:
```bash
dotnet restore
```

2. Compilar la solución:
```bash
dotnet build
```

3. Ejecutar la aplicación:
```bash
dotnet run --project GreatSoft.Be.API
```

4. Acceder a Swagger:
   - URL: `https://localhost:5001` o `http://localhost:5000`
   - Swagger UI está disponible en la raíz de la aplicación

## Endpoints de la API

### Autenticación (sin autenticación requerida)

- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar nuevo usuario
- `POST /api/auth/forgot-password` - Solicitar recuperación de contraseña
- `POST /api/auth/reset-password` - Restablecer contraseña

### Usuarios (requiere autenticación JWT)

- `GET /api/users` - Obtener todos los usuarios
- `GET /api/users/{id}` - Obtener usuario por ID
- `POST /api/users` - Crear nuevo usuario
- `PUT /api/users/{id}` - Actualizar usuario
- `DELETE /api/users/{id}` - Eliminar usuario

### Roles (requiere autenticación JWT)

- `GET /api/roles` - Obtener todos los roles
- `GET /api/roles/{id}` - Obtener rol por ID
- `POST /api/roles` - Crear nuevo rol
- `PUT /api/roles/{id}` - Actualizar rol
- `DELETE /api/roles/{id}` - Eliminar rol

## Uso de Swagger con JWT

1. Abre Swagger UI en el navegador
2. Ejecuta el endpoint `/api/auth/login` con las credenciales del admin:
   ```json
   {
     "username": "elgrandeahc",
     "password": "ahc123"
   }
   ```
3. Copia el token JWT de la respuesta
4. Haz clic en el botón "Authorize" en la parte superior de Swagger
5. Ingresa: `Bearer {tu_token_aqui}` (reemplaza `{tu_token_aqui}` con el token real)
6. Haz clic en "Authorize" y luego en "Close"
7. Ahora puedes probar los endpoints protegidos

## Base de Datos

Por defecto, la aplicación usa una base de datos en memoria (InMemory). Los datos se perderán al reiniciar la aplicación.

Para usar una base de datos real (SQL Server, PostgreSQL, etc.), modifica la configuración en `GreatSoft.Be.Infrastructure/DependencyInjection.cs`:

```csharp
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
```

## Tecnologías Utilizadas

- .NET 8.0
- Entity Framework Core 8.0
- JWT Bearer Authentication
- BCrypt para hash de contraseñas
- Swagger/OpenAPI

## Notas de Seguridad

- Las contraseñas se hashean usando BCrypt
- Los tokens JWT tienen expiración configurable
- Todos los endpoints de gestión requieren autenticación JWT
- El secreto JWT debe ser cambiado en producción
