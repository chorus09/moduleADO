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
    private static MSSQLService? _SqlConnection;
    private static PostgreService? _PostgreConnection;
    public static MSSQLService? GetSqlConnection() => _SqlConnection;
    public static void SetSqlConnection(MSSQLService connection) => _SqlConnection = connection;
    public static PostgreService? GetPostgreConnection() => _PostgreConnection;
    public static void SetPostgreConnection(PostgreService connection) => _PostgreConnection = connection;
}
