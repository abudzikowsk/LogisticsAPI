using Dapper;
using LogisticsAPI.Database;

namespace LogisticsAPI.Services;

public class MigrationService
{
    private readonly DapperContext _dapperContext;

    public MigrationService(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task RunMigrations()
    {
        var migrationsPath = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "CreateTable.sql");
        var migrationsScript = await File.ReadAllTextAsync(migrationsPath);
        using var connection = _dapperContext.CreateConnection();
        await connection.ExecuteAsync(migrationsScript);
    }
}