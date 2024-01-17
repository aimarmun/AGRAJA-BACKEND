using AgrajaBackend.Models;

namespace AgrajaBackend.Services.Contracts
{
    /// <summary>
    /// Interface de servicio cliente
    /// </summary>
    public interface IClientsService
    {
        /// <summary>
        /// Añadir cliente
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        Task AddAsync(Client client);

        /// <summary>
        /// Actualiza un cliente
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        Task UpdateAsync(Client client);

        /// <summary>
        /// Obtener por su DNI
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        Task<Client?> GetByDniAsync(string dni);

        /// <summary>
        /// Devuelve todos los clientes que coincidan de manera parcial con el dni
        /// </summary>
        /// <param name="partialDni"></param>
        /// <param name="onlyClientActive">Indica true para devolver solo los clientes activos</param>
        /// <returns></returns>
        Task<Client[]?> GetByPartialDniOrNameAsync(string partialDni, bool onlyClientActive);

        /// <summary>
        /// Adquirir por su ID
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<Client?> GetByIdAsync(int clientId);

        /// <summary>
        /// Devuelve true si el Dni ya está en uso
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="dni"></param>
        /// <returns></returns>
        Task<bool> IsDniInUseAsync(int personId, string dni);

        /// <summary>
        /// Devuelve tru si el email ya está en uso
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsEmailInUseAsync(int personId, string? email);

        /// <summary>
        /// Devuelve una matriz con todos los clientes.
        /// </summary>
        /// <returns></returns>
        Task<Client[]> GetAllAsync();
    }
}