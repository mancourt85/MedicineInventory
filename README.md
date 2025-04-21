# Gestión de Inventario de Medicamentos

## Descripción

Este proyecto es una aplicación web construida utilizando **ASP.NET Core MVC**, **Entity Framework Core**, **ClosedXML** y **PdfSharpCore**.  
Está diseñada para gestionar un inventario de medicamentos, proporcionando funcionalidades para gestionar los registros de medicamentos, incluyendo la creación, actualización, eliminación y exportación de datos en formatos Excel y PDF.

## Características

- **Operaciones CRUD**: Crear, Leer, Actualizar, Eliminar registros de medicamentos.
- **Opciones de Exportación**: Exportar los datos de medicamentos a formatos Excel o PDF.
- **Base de Datos**: Utiliza **Entity Framework Core** para operaciones con la base de datos.
- **Interfaz de Usuario**: Construida con **ASP.NET Core MVC** con un diseño adaptable.

## Stack Tecnológico

### Backend

- ASP.NET Core 8.0
- Entity Framework Core
- xUnit (para pruebas)

### Frontend

- Patrón MVC

### Base de Datos

- SQL Server o cualquier otra base de datos que soporte EF Core

### Bibliotecas

- ClosedXML: Para generar archivos Excel
- PdfSharpCore: Para generar archivos PDF

### Autenticación

- Autenticación basada en JWT (si aplica)

## Requisitos

- **.NET 8 SDK**: Descargalo desde https://dotnet.microsoft.com/download/dotnet
- **SQL Server**: Utiliza una instancia local de SQL Server o una base de datos remota.
- **Visual Studio 2022** (o cualquier otro IDE que soporte el desarrollo con ASP.NET Core).

## Instrucciones de Configuración

### Configuración de la Base de Datos

Configura la base de datos utilizando Entity Framework Core:

1. Abre la **Consola del Administrador de Paquetes** en Visual Studio.
2. Ejecuta el siguiente comando para aplicar las migraciones y crear las tablas SQL en tu base de datos:  
   `Update-Database`
3. Si la migración aún no se ha creado, ejecuta `Add-Migration InitialCreate` para generar el archivo de migración.
4. Revisa y modifica la cadena de conexión en `appsettings.json` o `appsettings.Development.json`.

### Instalación

1. Clonar el repositorio:
```bash
git clone https://github.com/your-username/MedicineInventoryApp.git
cd MedicineInventoryApp
```

2. Instalar dependencias:
```bash
dotnet restore
```

3. Configurar la conexión a la base de datos en `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MedicineInventoryDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

4. Crear la base de datos:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

5. Compilar y ejecutar la aplicación:
```bash
dotnet build
dotnet run
```

6. Acceder a la aplicación:
Abre tu navegador y navega a `https://localhost:5001/Medicines/Index`

## Características
- Gestión del inventario de medicamentos (Crear, Leer, Actualizar, Eliminar)
- Exportación de datos a formatos Excel y PDF
