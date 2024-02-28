using moduleADO.Services.Database.Requests;
using System.Data.SqlClient;
using moduleADO.Services.Database.Requests.MSSQL;
using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;

namespace moduleADO.Services.Database;
public class MSSQLService : IConnection {
    public string? ConnectionString { get; private set; }
    public string? Table { get; set; }

    public SqlConnection? Connection { get; private set; }

    public event EventHandler<string>? ConnectDB;

    public MSSQLService(string? connectionString, string? table) {
        ConnectionString = connectionString;
        Table = table;
    }
    public void Connect() {
        try {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            if (DatabaseHelper.TableExists(Connection, Table)) {
                ConnectDB?.Invoke(this, Table!);
            }
            ConnectDB?.Invoke(this, "successfully connection!");
        } catch (Exception) {
            ConnectDB?.Invoke(this, "error connection!");
        }
    }
}
