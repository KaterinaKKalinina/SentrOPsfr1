using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;

namespace SeniorCenterWebApp.Services
{
    public class DatabaseConnectionService2
    {
        
        
            private readonly IConfiguration _configuration;

            public DatabaseConnectionService2(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public string GetConnectionString()
            {
                var supabaseConnection = _configuration.GetConnectionString("DefaultConnection");
                var sqlServerConnection = _configuration.GetConnectionString("SqlServerConnection");

                // Пытаемся подключиться к Supabase
                if (TryConnectToSupabase(supabaseConnection))
                {
                    return supabaseConnection;
                }

                // Если Supabase недоступен — используем SQL Server
                return sqlServerConnection;
            }

            private bool TryConnectToSupabase(string connectionString)
            {
                try
                {
                    using var connection = new NpgsqlConnection(connectionString);
                    connection.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        
    }
}
