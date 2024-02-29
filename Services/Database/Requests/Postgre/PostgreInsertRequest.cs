using Npgsql;
using moduleADO.Services.Database.Requests.DTO.PostgreDTO;

namespace moduleADO.Services.Database.Requests.Postgre;
public class PostgreInsertRequest : IDatabaseRequest {
    private InsertIntoPostgresDTO _dto;

    public PostgreInsertRequest(InsertIntoPostgresDTO dto) {
        _dto = dto;
    }

    public async Task Execute() {
        int lastId = GetLastId();

        using (var connection = new NpgsqlConnection(_dto.Connection)) {
            await connection.OpenAsync();
            int newId = lastId + 1;

            using (var cmd = new NpgsqlCommand($"INSERT INTO {_dto.TableName} (id, productcode, productname, email) VALUES (@id, @code, @name, @email)",
                                                connection)) {
                cmd.Parameters.AddWithValue("id", newId);
                cmd.Parameters.AddWithValue("code", _dto.Product.Code);
                cmd.Parameters.AddWithValue("name", _dto.Product.Name);
                cmd.Parameters.AddWithValue("email", _dto.Product.Email);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
    private int GetLastId() {
        int lastId = 0;
        string connectionString = _dto.Connection;

        // Формируем запрос SELECT для получения максимального ID
        string query = $"SELECT MAX(id) FROM {_dto.TableName}";

        // Создаем подключение к базе данных
        using (var connection = new NpgsqlConnection(connectionString)) {
            // Открываем соединение
            connection.Open();

            // Создаем команду для выполнения запроса
            using (var command = new NpgsqlCommand(query, connection)) {
                // Выполняем запрос и получаем результат
                var result = command.ExecuteScalar();

                // Если результат не равен DBNull, преобразуем его в int
                if (result != DBNull.Value) {
                    lastId = Convert.ToInt32(result);
                }
            }
        }

        return lastId;
    }
}
