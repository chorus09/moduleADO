namespace moduleADO.Services.Database.Requests.DTO.PostgreDTO;
public class UpdatePostgresDTO
{
    public string TableName { get; set; }
    public string Connection { get; set; }
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
