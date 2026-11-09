# CS 409 Homework 2

## Quick start

Start Docker Desktop, open PowerShell in this `hw2` folder, and run:

```powershell
Copy-Item .env.example .env
docker compose --env-file .env up -d
.\scripts\Initialize-Database.ps1
```

The SQL Server database will be available at `localhost,1433`.

Useful Docker commands:

```powershell
docker compose ps
docker compose logs sqlserver
docker compose down
```
