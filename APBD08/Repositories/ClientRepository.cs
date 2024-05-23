using APBD08.Models;
using Microsoft.EntityFrameworkCore;

public interface IClientRepository
{
    Task DeleteClientAsync(int id);
}

namespace APBD08.Repositories
{
    public class ClientDbRepository : IClientRepository
    {
        private readonly Context.Context _context;

        public ClientDbRepository(Context.Context context)
        {
            _context = context;
        }

        public async Task DeleteClientAsync(int id)
        {
            bool hasTrips = await _context.ClientTrips.AnyAsync(e => e.IdClient == id);

            if (hasTrips)
            {
                throw new Exception("brak");
            }

            Client client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                throw new Exception("not found");
            }

            _context.Clients.Remove(client);
        }
    }
}