using AgrajaBackend.DTOs.Client;
using AgrajaBackend.DTOs.Farmer;
using AgrajaBackend.Models;

namespace AgrajaBackend.Services.Contracts
{
    /// <summary>
    /// Interface de servicio agricultor
    /// </summary>
    public interface IFarmersService
    {
        /// <summary>
        /// Añade un agricultor de manera asíncrona
        /// </summary>
        /// <param name="farmer"></param>
        /// <returns></returns>
        Task AddAsync(Farmer farmer);

        /// <summary>
        /// Añade una contratación de manera asíncrona
        /// </summary>
        /// <param name="farmerHiring"></param>
        /// <returns></returns>
        Task AddHiringAsync(FarmerHiring farmerHiring);

        /// <summary>
        /// Adquiere todos los agricultores de manera asíncrona
        /// </summary>
        /// <returns></returns>
        Task<Farmer[]> GetAllAsync();

        /// <summary>
        /// Adquiere los agricultores que coincidan con el tipo de cultivo
        /// </summary>
        /// <param name="cropType"></param>
        /// <returns></returns>
        Task<Farmer[]> GetByCropTypeAsync(int cropType);

        /// <summary>
        /// Devuelve tru si el DNI está en uso
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="dni"></param>
        /// <returns></returns>
        [Obsolete("No utilizar usar en su lugar GetByDni para comprobar si un DNI está en uso")]
        Task<bool> IsDniInUseAsync(int personId, string dni);

        /// <summary>
        /// Adquire un agricultor por su DNI
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        Task<Farmer?> GetByDniAsync(string dni);

        /// <summary>
        /// Devuelve true si el email está en uso
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// 
        [Obsolete("Utilizar GetByEmailAsync para averiguar si un email está en uso")]
        Task<bool> IsEmailInUseAsync(int personId, string? email);

        /// <summary>
        /// Adquire un agricultor por su Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Farmer?> GetByEmailAsync(string email);

        /// <summary>
        /// Acutaliza un agricultor de manera asíncrona
        /// </summary>
        /// <param name="farmer"></param>
        /// <returns></returns>
        Task UpdateAsync(Farmer farmer);

        /// <summary>
        /// Adquiere un agricultor por su Id
        /// </summary>
        /// <param name="farmerId"></param>
        /// <returns></returns>
        Task<Farmer?> GetByIdAsync(int farmerId);

        /// <summary>
        /// Adquiere las contrataciones de un agricultor por su Id
        /// </summary>
        /// <param name="farmerId"></param>
        /// <returns></returns>
        Task<FarmerHiring[]?> GetHiringsByFarmerIdAsync(int farmerId);

        /// <summary>
        /// Adquiere una contratación por su Id
        /// </summary>
        /// <param name="hiringId"></param>
        /// <returns></returns>
        Task<FarmerHiring?> GetHiringById(int hiringId);

        /// <summary>
        /// Adquire todos los cientes que coinciden con la contratación de un agricultor.
        /// </summary>
        /// <param name="farmerId"></param>
        /// <returns></returns>
        Task<ClientFarmerHiringDto[]?> GetClientsByFarmerHiring(int farmerId);

        /// <summary>
        /// Elimina varios contratos
        /// </summary>
        /// <param name="farmerHirings"></param>
        /// <returns>Devuelve una excepción si algo salio mal</returns>
        Task<Exception?> RemoveHirings(FarmerHiring[] farmerHirings);
        
        /// <summary>
        /// Aquire todas las contrataciones de un agricultor relacionadas con un cliente por su Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<FarmerHiring[]?> GetHiringsByClientIdAsync(int clientId);

        /// <summary>
        /// Adquire un dto de contratación por su modelo
        /// </summary>
        /// <param name="hiring"></param>
        /// <returns></returns>
        Task<FarmerHiringDto?> GetHiringDto(FarmerHiring hiring);

        /// <summary>
        /// Devuelve todos los dtos de contrataciones de un cliente
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        Task<FarmerHiringDto[]?> GetHiringsDtos(Client client);
    }
}