namespace AgrajaBackend.DTOs.User
{
    /// <summary>
    /// Dto de usuario y contraseña para iniciar sesión
    /// </summary>
    public class UserLoginRequestDto
    {
        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña de usuario
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
