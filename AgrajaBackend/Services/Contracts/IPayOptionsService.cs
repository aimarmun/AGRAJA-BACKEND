using AgrajaBackend.Models;

namespace AgrajaBackend.Services.Contracts
{
    /// <summary>
    /// Interface de servicio de formas de pago
    /// </summary>
    public interface IPayOptionsService
    {
        /// <summary>
        /// Adquiere todas las formas de pago de manera asíncrona
        /// </summary>
        /// <returns></returns>
        Task<PayOption[]> GetAllAsync();

        /// <summary>
        /// Adquire una forma de pago por su Id
        /// </summary>
        /// <param name="payOptionId"></param>
        /// <returns></returns>
        Task<PayOption?> GetByIdAsync(int payOptionId);
    }
}