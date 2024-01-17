using AgrajaBackend.Models;
using AgrajaBackend.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgrajaBackend.Services
{
    /// <summary>
    /// Servicio de clientes
    /// </summary>
    public class ClientsService : IClientsService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del servicio
        /// </summary>
        /// <param name="context">Se inyecta el dbContext</param>
        public ClientsService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Añade un nuevo cliente de manera asíncrona
        /// </summary>
        /// <param name="client">Nuevo cliente</param>
        /// <returns></returns>
        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza un cliente
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Devuelve un array con todos los clientes
        /// </summary>
        /// <returns></returns>
        public async Task<Client[]> GetAllAsync() => await _context.Clients.ToArrayAsync();

        /// <summary>
        /// Adquiere un cliente por su Id
        /// </summary>
        /// <param name="clientId">Id del cliente</param>
        /// <returns>Retorna el cliente o null si no se encuentra</returns>
        public async Task<Client?> GetByIdAsync(int clientId)
            => await _context.Clients.FirstOrDefaultAsync(client => client.Id == clientId);

        /// <summary>
        /// Adquire un cliente por su DNI
        /// </summary>
        /// <param name="dni">DNI</param>
        /// <returns>Devuelve el cliente o null si no se encuentra</returns>
        public async Task<Client?> GetByDniAsync(string dni)
            => await _context.Clients.FirstOrDefaultAsync(client => client.Dni == dni.ToLower().Trim());

        /// <summary>
        /// Obtiene los clientes que tengan alguna coincidencia en su DNI o nombre
        /// </summary>
        /// <param name="partialDniOrName">Dni esrito parcialmente o entero</param>
        /// <param name="onlyActiveClients">Añade true para devolver solo clientes activos</param>
        /// <returns></returns>
        public async Task<Client[]?> GetByPartialDniOrNameAsync(string partialDniOrName, bool onlyActiveClients = false)
        {
            if (partialDniOrName == null) return null;
            string normalizeSearch = partialDniOrName.ToLower().Trim();
            
            if (onlyActiveClients)
            {
                return await _context.Clients.Where(client =>
                    client.IsActive &&
                    (client.Dni.Contains(normalizeSearch) ||
                    client.Name.ToLower().Contains(normalizeSearch))).ToArrayAsync();
            }
            else
            {
                return await _context.Clients.Where(client =>
                    client.Dni.Contains(normalizeSearch) ||
                    client.Name.ToLower().Contains(normalizeSearch)).ToArrayAsync();
            }
        }

        /// <summary>
        /// Verifica si un DNI ya está en uso en algún cliente.
        /// Se debe indicar el Id del cliente que se debe ignorar para la comprobación.
        /// Esto sirve para que se permita guardar el mismo DNI del cliente que se quiere modificar.
        /// </summary>
        /// <param name="personId">Se especifica el ciente que se quiere ignorar</param>
        /// <param name="dni"></param>
        /// <returns></returns>
        public async Task<bool> IsDniInUseAsync(int personId, string dni)
            => await _context.Clients.AnyAsync(f => f.Id != personId && f.Dni == dni.ToLower());


        /// <summary>
        /// Verifica si un e-mail está en uso por algún otro cliente.
        /// Se debe indicar el Id del cliente que se debe ignorar para la comprobación.
        /// Esto sirve para que se permita guardar el mismo e-mail del cliente que se quiere modificar.
        /// La razón es que normalmente la modificación de un cliente solo se hara de alguna de sus propiedades.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> IsEmailInUseAsync(int personId, string? email)
        {
            if(string.IsNullOrEmpty(email)) return false;
            string? normalizedEmail = email?.ToLower().Trim();
            return await _context.Clients.AnyAsync(f => f.Id != personId && f.Email == normalizedEmail && f.Email != null);
        }

    }
}
