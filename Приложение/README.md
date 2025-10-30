
ConcreteCrackManager
====================

.NET 9 WPF application (minimal template) for managing a PostgreSQL database.
Includes basic CRUD UI, connection dialog, and SQL dialog for PostgreSQL roles.

Prerequisites:
- .NET 9 SDK installed on Windows
- PostgreSQL installed and running
- Optional: pgAdmin or psql to run init SQL

Quick start:
1. Open PowerShell in the project folder: /src/ConcreteCrackManager
2. Restore packages: dotnet restore
3. Build: dotnet build
4. Run migrations (or run sql/init_postgres.sql manually):
   - Install dotnet-ef if needed: dotnet tool install --global dotnet-ef
   - dotnet ef migrations add InitialCreate
   - dotnet ef database update
   (Alternatively run sql/init_postgres.sql in psql/pgAdmin)
5. Run the app: dotnet run --project src/ConcreteCrackManager

Default connection file: connection.json (written by Connection dialog).

