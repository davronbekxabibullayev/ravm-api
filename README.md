## Database

### Add-Migrations

CLI
```
dotnet ef migrations add "SampleMigration" --project Services\Ravm\Ravm.Infrastructure --startup-project Services\Ravm\Ravm.Api --output-dir Persistance\EntityFramework\Migrations
```

Package-Mananger Console
```
Add-Migration InitalMigration -Project Ravm.Infrastructure -OutputDir Persistance\EntityFramework\Migrations
```
