using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
using moduleADO.Services.Database.Requests.DTO.PostgreDTO;
using Npgsql;

namespace moduleADO.Services.Database.Requests.Postgre;
public class PostgreSelectRequest : IDatabaseRequest, IValues, IAll {
    private SelectPostgresDTO _dto;
    public List<List<object>> Values { get; private set; }
    public PostgreSelectRequest(SelectPostgresDTO dto) {
        _dto = dto;
        Values = [];
    }

    public void Execute() {
        using (var connection = new NpgsqlConnection(_dto.Connection)) {
            connection.Open();
            using (var cmd = new NpgsqlCommand($"SELECT * FROM {_dto.TableName} WHERE Email = @email", connection)) {
                cmd.Parameters.AddWithValue("email", _dto.Email!);
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

    public void ExecuteAll() {
        using (var connection = new NpgsqlConnection(_dto.Connection)) {
            connection.Open();
            using (var cmd = new NpgsqlCommand($"SELECT * FROM {_dto.TableName}", connection)) {
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
}
