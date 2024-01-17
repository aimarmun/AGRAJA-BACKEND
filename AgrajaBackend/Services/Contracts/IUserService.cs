using AgrajaBackend.DTOs.JwtToken;
using AgrajaBackend.DTOs.User;
using AgrajaBackend.Models;
using System.Security.Claims;

namespace AgrajaBackend.Services.Contracts
{
    /// <summary>
    /// Interface de servicio de usuarios
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Será true si se han inicializado los usuarios por primera vez o se han resetado, con la intención de inicar que es necesario configurar nuevas contraseñas.
        /// </summary>
        bool ConfigPendig { get; set; }

        /// <summary>
        /// Adquiere todos los usuarios
        /// </summary>
        /// <returns></returns>
        Task<User[]> GetAllAsync();

        /// <summary>
        /// Actualiza los tokens
        /// </summary>
        /// <param name="jwtToken">Tokens existentes en el cliente</param>
        /// <returns></returns>
        Task<JwtTokenDto?> RefreshTokenAsync(JwtTokenDto jwtToken);

        /// <summary>
        /// Añade un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddAsync(User user);

        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<JwtTokenDto?> UpdateAsync(User user);

        /// <summary>
        /// Devuelve el token de usuario si es válido
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>Devolverá null si el usuario no coincide</returns>
        Task<JwtTokenDto?> GetUserTokenAsync(UserLoginRequestDto userLogin);

        /// <summary>
        /// Crea los usuarios por defecto
        /// </summary>
        /// <returns></returns>
        Task InitializeDefaultUsers();

        /// <summary>
        /// Actualiza la contraseña y envía un nuevo token
        /// </summary>
        /// <param name="userNewPassword"></param>
        /// <returns></returns>
        Task<JwtTokenDto?> UpdateUserPasswordAsync(UserNewPasswordRequestDto userNewPassword);

        /// <summary>
        /// Revoca el token del usuario indicado
        /// </summary>
        /// <param name="username"></param>
        Task<bool> RevokeAsync(string username);

    }
}
