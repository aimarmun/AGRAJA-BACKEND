using AgrajaBackend.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgrajaBackend.Services
{
    /// <summary>
    /// Servicio de tipo de cultivo
    /// </summary>
    public class CropTypesService : ICropTypesService
    {
        readonly AppDbContext _context;

        /// <summary>
        /// Constructor del servicio
        /// </summary>
        /// <param name="context">Se inyecta el el dbContext</param>
        public CropTypesService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adquiere todos los tipos de cultivo
        /// </summary>
        /// <returns>Array con los tipos de cultivo</returns>
        public async Task<Models.CropType[]> GetAllAsync() => await _context.CropTypes.ToArrayAsync();

        /// <summary>
        /// Comprueba si existe un tipo de cultivo por su Id
        /// </summary>
        /// <param name="cropTypeId">Id de tipo de cultivo</param>
        /// <returns>True si se encuentra</returns>
        public async Task<bool> ExistsByIdAsync(int cropTypeId) 
            => await _context.CropTypes.AnyAsync(c => c.Id == cropTypeId);

        /// <summary>
        /// Adquiere tipo de cultivo por su Id
        /// </summary>
        /// <param name="cropTypeId">Id de tipo de cultivo</param>
        /// <returns>Tipo de cultivo o null si no se encuentra</returns>
        public async Task<Models.CropType?> GetByIdAsync(int cropTypeId) 
            => await _context.CropTypes.FirstOrDefaultAsync(c => c.Id == cropTypeId);
    }
}
