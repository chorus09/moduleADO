using moduleADO.Models;

namespace moduleADO.Services {
    public class ClientManage {
        public static List<Client> GetClientsFromList(List<List<object>>? data) {
            var clients = new List<Client>();

            if (data != null) {
                foreach (var item in data) {
                    if (item.Count >= 6 &&
                        item[0] is int id &&
                        item[1] is string firstName &&
                        item[2] is string lastName &&
                        item[3] is string middleName &&
                        item[4] is string phone &&
                        item[5] is string email) {
                        clients.Add(new Client(id, firstName, lastName, middleName, email, phone));
                    } else {
                        return [];
                    }
                }
            }

            return clients;
        }
    }
}
