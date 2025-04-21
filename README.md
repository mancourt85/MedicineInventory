# Medicine Inventory Management

## Description

This project is a web application built using **ASP.NET Core MVC**, **Entity Framework Core**, **ClosedXML**, and **PdfSharpCore**.  
It is designed to manage a medicine inventory, providing functionality to manage medicine records, including creating, updating, deleting, and exporting data in both Excel and PDF formats.

## Features

- **CRUD Operations**: Create, Read, Update, Delete medicine records.
- **Export Options**: Export medicine data to Excel or PDF formats.
- **Database**: Utilizes **Entity Framework Core** for database operations.
- **User Interface**: Built using **ASP.NET Core MVC** with a responsive layout.

## Tech Stack

### Backend

- ASP.NET Core 8.0
- Entity Framework Core
- xUnit (for testing)

### Frontend

- MVC pattern

### Database

- SQL Server or any other database that supports EF Core

### Libraries

- ClosedXML: For generating Excel files
- PdfSharpCore: For generating PDF files

### Authentication

- JWT-based authentication (if applicable)

## Prerequisites

- **.NET 8 SDK**: Download from https://dotnet.microsoft.com/download/dotnet
- **SQL Server**: Use either a local SQL Server instance or a remote database.
- **Visual Studio 2022** (or any other IDE that supports ASP.NET Core development).

## Setup Instructions

### Database Setup

Set up the database using Entity Framework Core:

1. Open the **Package Manager Console** in Visual Studio.
2. Run the following command to apply migrations and create the SQL tables in your database:  
   `Update-Database`
3. If the migration hasn't been created yet, run `Add-Migration InitialCreate` to generate the migration file.
4. Check and modify the connection string in `appsettings.json` or `appsettings.Development.json`.

**Example connection string**:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MedicineInventory;User Id=yourusername;Password=yourpassword;"
}
