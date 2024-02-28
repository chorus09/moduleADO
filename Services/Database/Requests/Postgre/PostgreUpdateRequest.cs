using moduleADO.Models.Observer;
using moduleADO.Services.Database.Requests.DTO.PostgreDTO;
using Npgsql;

namespace moduleADO.Services.Database.Requests.Postgre
{
    public class PostgreUpdateProductRequest : IDatabaseRequest {
        private UpdatePostgresDTO _dto;
        public PostgreUpdateProductRequest(UpdatePostgresDTO dto) {
            _dto = dto;
        }

        public void Execute() {
            using (var connection = new NpgsqlConnection(_dto.Connection)) {
                connection.Open();
                string query = $"UPDATE {_dto.TableName} SET productcode = @code, productname = @name, email = @email";
                query += " WHERE id = @id";
                using (var cmd = new NpgsqlCommand(query, connection)) {
                    cmd.Parameters.AddWithValue("code", _dto.Code);
                    cmd.Parameters.AddWithValue("name", _dto.Name);
                    cmd.Parameters.AddWithValue("email", _dto.Email);
                    cmd.Parameters.AddWithValue("id", _dto.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
