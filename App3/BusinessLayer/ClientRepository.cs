using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App3.BusinessLayer;
using App3.Models;

namespace App3.BusinessLayer.Repository
{
    public class ClientRepository : IClient
    {
       private readonly AppDbContext _context;
        public ClientRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public Client Add(Client client)
        {
            _context.Add(client);
            _context.SaveChanges();
            return client;
        }

        public Client Delete(Client client)
        {
            Client client1 = _context.Clients.Find(client.id);
            if(client1!=null)
            {
                _context.Remove(client1);
                _context.SaveChanges();
            }
            return client;
        }

        public List<Client> GetAllClients()
        {
            return _context.Clients?.ToList();
        }

        public Client GetClient(int clientID)
        {
            Client client = _context.Clients.Find(clientID);
            return client;
        }

        public Client Update(Client client)
        {
            var client1 = _context.Clients.Attach(client);
            client1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return client;
        }
    }
}
