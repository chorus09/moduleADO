
namespace moduleADO.Services.Database {
    public interface IConnection {
        string? ConnectionString { get; }
        string? Table { get; set; }
        void Connect();
        event EventHandler<string> ConnectDB;
    }
}
