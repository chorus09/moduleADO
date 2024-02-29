
using moduleADO.Services.Database;

namespace moduleADO.ViewModels.ConnectionViewModels {
    public class MSSQLConnectionViewModel : DBConnectionViewModelBase {
        protected override void Connect() {
            IConnection connection = 
                new MSSQLService($"Server={Server};Database={Database};User Id={Username};Password={Password};", TableName);
            connection.ConnectDB += (sender, message) => ConnectionStatus = message;
            connection.Connect();
            if (connection.IsConnected) { 
                IsConnected = true;
                DatabaseHelper.SetSqlConnection((MSSQLService)connection);
            }
        }
        public bool IsConnected { get; set; }
    }
}
