using moduleADO.Models;

namespace moduleADO.Services.Database.Requests.DTO.PostgreDTO;
public class InsertIntoPostgresDTO
{
    public string? TableName { get; set; }
    public string? Connection { get; set; }
    public Product Product { get; set; }
}
