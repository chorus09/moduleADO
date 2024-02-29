
namespace moduleADO.Services.Database {
    public interface IConnection {
        string? ConnectionString { get; }
        string? Table { get; set; }
        public bool IsConnected { get; }
        void Connect();
        event EventHandler<string> ConnectDB;
    }
}
