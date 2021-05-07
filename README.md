# RealEstate cross platform built on .Net 5

## Visual Studio 2019 and SQL Server version Express


#### Prerequisites

- SQL Server Express 2019 or latest
- [Visual Studio 2019 version 16.9.4 or latest with .NET SDK 5.0 or latest](https://dotnet.microsoft.com/download)

#### Steps to run

- Restore real.estate.bak
- Update the connection string in appsettings.json in RealEstate.WebApi
- In Visual Studoi build whole solution or in CLI execute ```dotnet build```
- In Visual Studio, press "Control + F5" or in CLI execute ```dotnet run``` inside the RealEstate.WebApi folder
- The application run on port 8000
- The backend can access via https request using the pre-created account: admin, admin123, see documentation

## Technologies and frameworks used:
- ASP.NET Core 5.0.202
- Entity Framework Core 5.0.5
- ASP.NET Identity Core 5.0.5
- Swagger 6.1.4
- JWT Authentication 5.0.5

## The architecture highlight

Use simple Clean Architecture with three projects
-RealEstate.WebApi contains services, controllers
-RealEstate.Infrastructure contains logic persistence and conexión to the database
-RealEstate.Domain contains the business logic as models and viewmodels
