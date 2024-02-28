
namespace moduleADO.Models.Observer; 
public class ClientUpdater : IClientObserver {
    private Action<Client> _updateDataClient;
    public ClientUpdater(Action<Client> action) {
        _updateDataClient = action;
    }

    public void Update(Client client, object updatedValue) {
        _updateDataClient?.Invoke(client);
    }
}
