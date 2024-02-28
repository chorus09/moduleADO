using moduleADO.Models;

namespace moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
public class InsertIntoMSSQLDTO
{
    public string? TableName { get; set; }
    public string? Connection { get; set; }
    public Client Client { get; set; }
}
