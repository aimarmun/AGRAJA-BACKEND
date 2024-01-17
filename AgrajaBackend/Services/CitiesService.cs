using AgrajaBackend.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgrajaBackend.Services
{
    /// <summary>
    /// Servicio para CRUD clientes
    /// </summary>
    public class CitiesService : ICitiesService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Inyecta dbContext</param>
        public CitiesService(AppDbContext context)
        {
            _context = context;
        }

    
        /// <summary>
        /// Adquiere todas las ciudades de manera asíncrona
        /// </summary>
        /// <returns>Array de ciudades</returns>
        public async Task<Models.City[]> GetAllAsync() => await _context.Cities.ToArrayAsync();

        /// <summary>
        /// Comprueba si existe una ciudad por su Id
        /// </summary>
        /// <param name="cityId">Id de ciudad</param>
        /// <returns>True si existe</returns>
        public async Task<bool> ExistsByIdAsync(int cityId) => await _context.Cities.AnyAsync(c => c.Id == cityId);


        /// <summary>
        /// Adquiere un ciudad por su Id
        /// </summary>
        /// <param name="cityId">Id de la ciudad</param>
        /// <returns>Ciudad encontrada o null si no se encuentra</returns>
        public async Task<Models.City?> GetByIdAsync(int cityId)
            => await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
      
    }
}
