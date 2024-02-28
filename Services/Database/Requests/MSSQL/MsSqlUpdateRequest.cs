using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
using System.Data.SqlClient;

namespace moduleADO.Services.Database.Requests.MSSQL;
public class MsSqlUpdateRequest : IDatabaseRequest {
    private UpdateMSSQLDTO _dto;

    public MsSqlUpdateRequest(UpdateMSSQLDTO dto) {
        _dto = dto;
    }

    public void Execute() {
        using (var connection = new SqlConnection(_dto.Connection)) {
            connection.Open();

            string query = $"UPDATE {_dto.TableName} SET FirstName = @firstName, LastName = @lastName, MiddleName = @middleName, Email = @email, Phone = @phone";
            query += " WHERE Id = @id";
            using (var cmd = new SqlCommand(query, connection)) {
                cmd.Parameters.AddWithValue("id", _dto.Id);
                cmd.Parameters.AddWithValue("firstName", _dto.FirstName);
                cmd.Parameters.AddWithValue("lastName", _dto.LastName);
                cmd.Parameters.AddWithValue("middleName", _dto.MiddleName);
                cmd.Parameters.AddWithValue("email", _dto.Email);
                cmd.Parameters.AddWithValue("phone", _dto.Phone);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
