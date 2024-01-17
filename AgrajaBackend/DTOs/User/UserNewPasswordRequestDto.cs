
namespace AgrajaBackend.DTOs.User
{
    /// <summary>
    /// Dto para cambiar el password
    /// </summary>
    public class UserNewPasswordRequestDto : UserLoginRequestDto
    {
        /// <summary>
        /// Nuevo password a cambiar
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}
