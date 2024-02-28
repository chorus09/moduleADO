using moduleADO.Models;

namespace moduleADO.Services.Database.Requests.DTO.MSSQLDTO;
public class UpdateMSSQLDTO
{
    public string TableName { get; set; }
    public string Connection { get; set; }
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
