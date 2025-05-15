# CreditoApp

## Proyecto Backend

<!-- DEPENDENCIAS -->

### Dependencias

#### Herramientas necesarias

- .Net 8
- SQL Server Express
- Dotnet EF

Es necesario instalar dotnet-ef para correr las migraciones.

```bash
dotnet tool install --global dotnet-ef
```

### Configuración

Se debe configurar `appsettings.Development.json` de la siguiente forma:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\\\SQLEXPRESS;Database=CreditoAppDb;User Id=sa;Password=tuPassword123;TrustServerCertificate=True;"
},
```

`UserId` y `Password` se deben cambiar por su usuario de la base de datos creado.

### Migraciones

Para crear crear la base con la migración ya creada.

```bash
dotnet ef database update -p CreditoApp.Infrastructure -s CreditoApp.API
```

### Ejecutar

```bash
dotnet run --project CreditoApp.API
```

### Usuario de prueba (Analista)

```json
{
  "email": "analyst@creditoapp.com",
  "password": "Analyst123"
}
```
