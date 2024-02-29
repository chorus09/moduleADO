using moduleADO.Services.Database;

namespace moduleADO.ViewModels.ConnectionViewModels; 
public class PostgreConnectionViewModel : DBConnectionViewModelBase {
    protected override void Connect() {
        IConnection connection =
            new PostgreService($"Host={Server};Database={Database};Username={Username};Password={Password};", TableName);
        connection.ConnectDB += (sender, message) => ConnectionStatus = message;
        connection.Connect();
        if (connection.IsConnected) { 
            IsConnected = true;
            DatabaseHelper.SetPostgreConnection((PostgreService)connection);
        }
    }
    public bool IsConnected { get; set; }
}
