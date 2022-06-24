using App3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.BusinessLayer
{
    public interface IClient
    {
        public Client Add(Client client);
        public Client Delete(Client client);
        public List<Client> GetAllClients();
        public Client GetClient(int clientID);
        public Client Update(Client client);

    }
}
