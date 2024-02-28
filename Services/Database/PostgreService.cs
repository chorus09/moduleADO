using moduleADO.Services.Database.Requests;
using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
using moduleADO.Services.Database.Requests.DTO.PostgreDTO;
using moduleADO.Services.Database.Requests.Postgre;
using Npgsql;

namespace moduleADO.Services.Database;
public class PostgreService : IConnection {
    public string? ConnectionString { get; private set; }
    public string? Table { get; set; }

    public NpgsqlConnection? Connection { get; private set; }
    public event EventHandler<string>? ConnectDB;

    public PostgreService(string? connectionString, string? table) {
        ConnectionString = connectionString;
        Table = table;
    }

    public void Connect() {
        try {
            Connection = new(ConnectionString);
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
