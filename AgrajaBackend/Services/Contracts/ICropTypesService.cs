using AgrajaBackend.Models;

namespace AgrajaBackend.Services.Contracts
{
    /// <summary>
    /// Interface de servicio de tipo de cultivo
    /// </summary>
    public interface ICropTypesService
    {

        /// <summary>
        /// Retorna todos los cultivos de manera asíncrona
        /// </summary>
        /// <returns></returns>
        public Task<CropType[]> GetAllAsync();

        /// <summary>
        /// Devuelve tru si existe el tipo de cultivo
        /// </summary>
        /// <param name="cropTypeId"></param>
        /// <returns></returns>
        public Task<bool> ExistsByIdAsync(int cropTypeId);

        /// <summary>
        /// Adquiere tipo de cultivo por su Id
        /// </summary>
        /// <param name="cropTypeId"></param>
        /// <returns></returns>
        public Task<CropType?> GetByIdAsync(int cropTypeId);
    }
}