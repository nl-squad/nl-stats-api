using MySqlConnector;

namespace Nl.Stats.Api.Database;

public interface IConnectionFactory
{
    MySqlConnection GetConnection();
}