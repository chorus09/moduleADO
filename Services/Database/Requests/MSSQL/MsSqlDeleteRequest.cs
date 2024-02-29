using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
using System.Data.SqlClient;
namespace moduleADO.Services.Database.Requests.MSSQL;
public class MsSqlDeleteRequest : IDatabaseRequest, IAll {
    private DeleteMSSQLDTO _dto;

    public MsSqlDeleteRequest(DeleteMSSQLDTO dto) {
        _dto = dto;
    }

    public async Task Execute() {
        using (var connection = new SqlConnection(_dto.Connection)) {
            await connection.OpenAsync();
            using (var cmd = new SqlCommand($"DELETE FROM {_dto.TableName} WHERE Id = @id", connection)) {
                cmd.Parameters.AddWithValue("id", _dto.Id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
    public async Task ExecuteAll() {
        using (var connection = new SqlConnection(_dto.Connection)) {
            await connection.OpenAsync();
            using (var cmd = new SqlCommand($"DELETE FROM {_dto.TableName}", connection)) {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
