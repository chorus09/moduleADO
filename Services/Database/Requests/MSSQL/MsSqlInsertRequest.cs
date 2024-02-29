using moduleADO.Services.Database.Requests.DTO;
using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
using Npgsql;
using System.Data.SqlClient;

namespace moduleADO.Services.Database.Requests.MSSQL;
public class MsSqlInsertRequest : IDatabaseRequest {
    private InsertIntoMSSQLDTO _dto;

    public MsSqlInsertRequest(InsertIntoMSSQLDTO dto) {
        _dto = dto;
    }

    public async Task Execute() {
        int lastId = GetLastId();

        using (var connection = new SqlConnection(_dto.Connection)) {
            await connection.OpenAsync();
            int newId = lastId + 1;

            using (var cmd = new SqlCommand($"INSERT INTO {_dto.TableName} (FirstName, LastName, MiddleName, Email, Phone) " +
                                             "VALUES (@firstName, @lastName, @middleName, @email, @phone)",
                                             connection)) {
                cmd.Parameters.AddWithValue("firstName", _dto.Client.FirstName);
                cmd.Parameters.AddWithValue("lastName", _dto.Client.LastName);
                cmd.Parameters.AddWithValue("middleName", _dto.Client.MiddleName);
                cmd.Parameters.AddWithValue("email", _dto.Client.Email);
                cmd.Parameters.AddWithValue("phone", _dto.Client.Phone);
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
        using (var connection = new SqlConnection(connectionString)) {
            // Открываем соединение
            connection.Open();

            // Создаем команду для выполнения запроса
            using (var command = new SqlCommand(query, connection)) {
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
