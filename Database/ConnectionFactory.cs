using MySqlConnector;

namespace Nl.Stats.Api.Database;

public class ConnectionFactory(string connectionString) : IConnectionFactory
{
    public MySqlConnection GetConnection() => new(connectionString);
}