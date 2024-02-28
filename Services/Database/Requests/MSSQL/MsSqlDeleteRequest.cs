using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
using System.Data.SqlClient;
namespace moduleADO.Services.Database.Requests.MSSQL;
public class MsSqlDeleteRequest : IDatabaseRequest, IAll {
    private DeleteMSSQLDTO _dto;

    public MsSqlDeleteRequest(DeleteMSSQLDTO dto) {
        _dto = dto;
    }

    public void Execute() {
        using (var connection = new SqlConnection(_dto.Connection)) {
            connection.Open();
            using (var cmd = new SqlCommand($"DELETE FROM {_dto.TableName} WHERE Id = @id", connection)) {
                cmd.Parameters.AddWithValue("id", _dto.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void ExecuteAll() {
        using (var connection = new SqlConnection(_dto.Connection)) {
            connection.Open();
            using (var cmd = new SqlCommand($"DELETE FROM {_dto.TableName}", connection)) {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
