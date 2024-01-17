using AgrajaBackend.DTOs.Crate;
using AgrajaBackend.Models;

namespace AgrajaBackend.Services.Contracts
{
    /// <summary>
    /// Interface de servicio de cajas
    /// </summary>
    public interface ICratesService
    {
        /// <summary>
        /// Añade una caja 
        /// </summary>
        /// <param name="crate"></param>
        /// <returns></returns>
        Task AddAsync(Crate crate);

        /// <summary>
        /// Añade una venta
        /// </summary>
        /// <param name="crateSale"></param>
        /// <returns></returns>
        Task AddSaleAsync(CrateSale crateSale);

        /// <summary>
        /// Devuelve true si el nombre existe.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="crateId">Para ignorar la coincidencia de nombre con esta caja</param>
        /// <returns></returns>
        Task<bool> NameExistsAsync(string name, int crateId = 0);

        /// <summary>
        /// Adquiere todas las cajas
        /// </summary>
        /// <returns></returns>
        Task<Crate[]> GetAllAsync();

        /// <summary>
        /// Adquiere una caja por su Id
        /// </summary>
        /// <param name="crateId"></param>
        /// <returns></returns>
        Task<Crate?> GetByIdAsync(int crateId);

        /// <summary>
        /// Elimina una caja por su Id
        /// </summary>
        /// <param name="crate"></param>
        /// <returns></returns>
        [Obsolete("Este metodo está marcado como obsoleto y no se debe usar, las cajas no se deben eliminar.")]
        Task RemoveAsync(Crate crate);

        /// <summary>
        /// Actualiza una caja
        /// </summary>
        /// <param name="crate"></param>
        /// <returns></returns>
        Task UpdateAsync(Crate crate);

        /// <summary>
        /// Retorna todas las ventas de la caja indicada por su Id
        /// </summary>
        /// <param name="crateId"></param>
        /// <returns></returns>
        Task<CrateSaleDto[]?> GetAllSalesByCrateId(int crateId);

        /// <summary>
        /// Retorna todas las ventas pertenecientes a un cliente
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<CrateSaleDto[]?> GetAllSalesByClientId(int clientId);
    }
}