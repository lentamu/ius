# IUS – Run Guide

## Docker
```bash
docker compose up --build
```

## EF Core Migrations

### Apply migrations
```bash
dotnet ef database update \
  --project Infrastructure/Infrastructure.csproj \
  --startup-project Api/Api.csproj
```

### Create new migration
```bash
dotnet ef migrations add \
  --project Infrastructure/Infrastructure.csproj \
  --startup-project Api/Api.csproj <MigrationName> \
  --output-dir Persistence/Migrations
```
