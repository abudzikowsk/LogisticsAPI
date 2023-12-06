using System.Data;
using Microsoft.Data.SqlClient;

namespace LogisticsAPI.Database;

public class DapperContext
{
    // Prywatne pole przechowujące łańcuch połączenia z bazą danych
    private readonly string _connectionString;

    // Konstruktor klasy przyjmujący interfejs IConfiguration jako parametr.
    // Konfiguracja zawiera informacje konfiguracyjne dla aplikacji, takie jak łańcuchy połączenia, ustawienia logowania itp.
    public DapperContext(IConfiguration configuration)
    {
        // Pobieranie łańcucha połączenia o nazwie "DefaultConnection" z konfiguracji
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // Metoda służąca do tworzenia nowego połączenia do bazy danych
    public IDbConnection CreateConnection()
    {
        // Tworzenie nowego połączenia SQL przy użyciu łańcucha połączenia
        return new SqlConnection(_connectionString);
    }
}