namespace AgrajaBackend.DTOs.User
{
    /// <summary>
    /// Dto que devuelve la información login de un usuario
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Rol de usuario
        /// </summary>
        public string Rol { get; set; } = string.Empty;
    }
}
