using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
using moduleADO.Services.Database.Requests.DTO.PostgreDTO;
using Npgsql;

namespace moduleADO.Services.Database.Requests.Postgre;
public class PostgreDeleteRequest : IDatabaseRequest, IAll, IAllDeleteForOne {
    private DeletePostgresDTO _dto;

    public PostgreDeleteRequest(DeletePostgresDTO dto) {
        _dto = dto;
    }

    public async Task Execute() {
        using (var connection = new NpgsqlConnection(_dto.Connection)) {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand($"DELETE FROM {_dto.TableName} WHERE Id = @id", connection)) {
                cmd.Parameters.AddWithValue("id", _dto.Id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task ExecuteAll() {
        using (var connection = new NpgsqlConnection(_dto.Connection)) {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand($"DELETE FROM {_dto.TableName}", connection)) {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task ExecuteAllForOne() {
        using (var connection = new NpgsqlConnection(_dto.Connection)) {
            await connection.OpenAsync();
            using (var cmd = new NpgsqlCommand($"DELETE FROM {_dto.TableName} Email = @email", connection)) {
                cmd.Parameters.AddWithValue("email", _dto.Email);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
