

namespace moduleADO.Services.Database.Args {
    public class DatabaseErrorEventArgs : EventArgs {
        public Exception Exception { get; }
        public DatabaseErrorEventArgs(Exception exception) {
            Exception = exception;
        }
    }
}
