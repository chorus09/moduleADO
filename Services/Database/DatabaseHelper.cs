using System.Data.SqlClient;
using Npgsql;

namespace moduleADO.Services.Database; 
public static class DatabaseHelper {
    public static bool TableExists(SqlConnection connection, string? table) {
        try {
            string sql = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{table}'";
            SqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public static bool TableExists(NpgsqlConnection connection, string? table) {
        try {
            string sql = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{table}'";
            NpgsqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();
            return true;
        } catch (Exception) {
            return false;
        }
    }
    private static IConnection? _connection;
    public static IConnection? GetConnection() => _connection;
    public static void SetConnection(IConnection connection) => _connection = connection;
}
