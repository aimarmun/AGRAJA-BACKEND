using AgrajaBackend.Models;

namespace AgrajaBackend.Services.Contracts
{
    /// <summary>
    /// Interface de ciudades
    /// </summary>
    public interface ICitiesService
    {
        /// <summary>
        /// Devuelve todas de manera asíncrona
        /// </summary>
        /// <returns></returns>
        public Task<City[]> GetAllAsync();

        /// <summary>
        /// Devuelve true si la ciudad existe
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public Task<bool> ExistsByIdAsync(int cityId);

        /// <summary>
        /// Devuelve la ciudad por su Id
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public Task<City?> GetByIdAsync(int cityId);
    }
}