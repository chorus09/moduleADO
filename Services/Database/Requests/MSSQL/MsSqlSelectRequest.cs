using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
using System.Data.SqlClient;

namespace moduleADO.Services.Database.Requests.MSSQL;
public class MsSqlSelectRequest : IDatabaseRequest, IValues {
    private SelectMSSQLDTO _dto;
    public List<List<object>> Values { get; private set; }

    public MsSqlSelectRequest(SelectMSSQLDTO dto) {
        _dto = dto;
        Values = new List<List<object>>();
    }

    public async Task Execute() {
        using (var connection = new SqlConnection(_dto.Connection)) {
            await connection.OpenAsync();
            using (var cmd = new SqlCommand($"SELECT * FROM {_dto.TableName}", connection))
            using (var reader = cmd.ExecuteReader()) {
                while (reader.Read()) {
                    var rowValues = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++) {
                        rowValues.Add(reader.GetValue(i));
                    }
                    Values.Add(rowValues);
                }
            }
        }
    }
}
