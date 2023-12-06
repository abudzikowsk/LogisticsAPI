using Dapper;
using LogisticsAPI.Database;

namespace LogisticsAPI.Services;

// Klasa do wykonania migracji bazy danych
public class MigrationService
{
    // Kontekst Dapper, który pozwala na wykonywanie operacji na bazie danych
    private readonly DapperContext _dapperContext;

    // Konstruktor przyjmujący kontekst Dapper jako argument
    public MigrationService(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    // Asynchroniczna metoda wykonująca migracje bazy danych
    public async Task RunMigrations()
    {
        // Określanie ścieżki do pliku z migracją
        var migrationsPath = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "CreateTable.sql");
        // Odczytanie treści pliku z migracją
        var migrationsScript = await File.ReadAllTextAsync(migrationsPath);
        // Utworzenie połączenia do bazy danych
        using var connection = _dapperContext.CreateConnection();
        // Wykonanie skryptu migracji na bazie danych
        await connection.ExecuteAsync(migrationsScript);
    }
}