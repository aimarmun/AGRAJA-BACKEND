using AgrajaBackend.Models;
using AgrajaBackend.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgrajaBackend.Services
{
    /// <summary>
    /// Servicion de opciones de pago
    /// </summary>
    public class PayOptionsService : IPayOptionsService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor de servicio de opciones de pago.
        /// </summary>
        /// <param name="appDbContext">Se inyecta dbContext</param>
        public PayOptionsService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        /// <summary>
        /// Obtiene todas las opciones de compra
        /// </summary>
        /// <returns>Un array con todas las opciones de compra</returns>
        public async Task<PayOption[]> GetAllAsync()
            => await _context.PayOptions.ToArrayAsync();

        /// <summary>
        /// Obtiene una opción de compra por su Id
        /// </summary>
        /// <param name="payOptionId">Id de opción de compra</param>
        /// <returns></returns>
        public async Task<PayOption?> GetByIdAsync(int payOptionId)
            => await _context.PayOptions.FirstOrDefaultAsync(p => p.Id == payOptionId);
    }
}
