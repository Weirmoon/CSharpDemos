using Microsoft.Extensions.Configuration;

namespace DataLib._Util;

public class ConnectionUtil
{
    protected enum EDatabaseName
    {
        TestDatabase,
    }

    protected static string? GetConnectionString(EDatabaseName databaseName = EDatabaseName.TestDatabase)
    {
        // Build the configuration
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        var configuration = builder.Build();

        // Define your connection string
        string? connString;
        
        switch (databaseName)
        {
            default:
                connString = configuration.GetConnectionString("PostgresqlConnection");
                break;
        }
        
        return connString;
    }
}