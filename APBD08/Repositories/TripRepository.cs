using APBD08.DTO;
using APBD08.Models;
using Microsoft.EntityFrameworkCore;

public interface ITripRepository
{
    Task<IEnumerable<TripDTO>> GetTripsAsync();
    Task AddTrip(int id, AddTrip dto);
}

namespace APBD08.Repositories
{
    public class TripDbRepository : ITripRepository
    {
        private readonly Context.Context _context;

        public TripDbRepository(Context.Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TripDTO>> GetTripsAsync()
        {
            var trips = await _context.Trips
                .OrderByDescending(t => t.DateFrom)
                .Select(row => new TripDTO
                {
                    Name = row.Name,
                    Description = row.Description,
                    DateFrom = row.DateFrom,
                    DateTo = row.DateTo,
                    MaxPeople = row.MaxPeople,
                    Countries = row.CountryTrips.Select(ct => new CountryDTO { Name = ct.IdCountryNavigation.Name }),
                    Clients = row.ClientTrips.Select(ct => new ClientDTO
                    {
                        FirstName = ct.IDN.FirstName,
                        LastName = ct.IDN.LastName
                    })
                })
                .ToListAsync();

            if (!trips.Any())
            {
                throw new Exception("no trips");
            }

            return trips;
        }

        public async Task AddTrip(int id, AddTrip dto)
        {
            
            var tripExists = await _context.Trips.AnyAsync(e => e.IdTrip == id);
            if (!tripExists)
            {
                throw new Exception($"no trip");
            }

            
            var client = await _context.Clients.FirstOrDefaultAsync(e => e.Pesel == dto.Pesel);
            if (client == null)
            {
                
                int newClientId = await _context.Clients.AnyAsync() ? await _context.Clients.MaxAsync(e => e.IdClient) + 1 : 1;
                client = new Client
                {
                    IdClient = newClientId,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Telephone = dto.Telephone,
                    Pesel = dto.Pesel
                };

                await _context.Clients.AddAsync(client);
            }

            
            var isClientAlreadyReservedThisTrip = await _context.ClientTrips.AnyAsync(row => row.IdClient == client.IdClient && row.IdTrip == id);
            if (isClientAlreadyReservedThisTrip)
            {
                throw new Exception("exc");
            }

            
            await _context.ClientTrips.AddAsync(new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = id,
                RegisteredAt = DateTime.Now,
                PaymentDate = dto.PaymentDate
            });
            
        }
    }
}