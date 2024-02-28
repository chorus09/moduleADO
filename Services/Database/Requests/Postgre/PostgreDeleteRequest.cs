using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
using moduleADO.Services.Database.Requests.DTO.PostgreDTO;
using Npgsql;

namespace moduleADO.Services.Database.Requests.Postgre;
public class PostgreDeleteRequest : IDatabaseRequest, IAll, IAllDeleteForOne {
    private DeletePostgresDTO _dto;

    public PostgreDeleteRequest(DeletePostgresDTO dto) {
        _dto = dto;
    }

    public void Execute() {
        using (var connection = new NpgsqlConnection(_dto.Connection)) {
            connection.Open();
            using (var cmd = new NpgsqlCommand($"DELETE FROM {_dto.TableName} WHERE Id = @id", connection)) {
                cmd.Parameters.AddWithValue("id", _dto.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void ExecuteAll() {
        using (var connection = new NpgsqlConnection(_dto.Connection)) {
            connection.Open();
            using (var cmd = new NpgsqlCommand($"DELETE FROM {_dto.TableName}", connection)) {
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void ExecuteAllForOne() {
        using (var connection = new NpgsqlConnection(_dto.Connection)) {
            connection.Open();
            using (var cmd = new NpgsqlCommand($"DELETE FROM {_dto.TableName} Email = @email", connection)) {
                cmd.Parameters.AddWithValue("email", _dto.Email);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
