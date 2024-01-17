using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgrajaBackend.Models
{
    /// <summary>
    /// Modelo de usuario
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id del usuario
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Rol de usuario
        /// </summary>
        [Required]
        public string Rol { get; set; } = string.Empty;

        /// <summary>
        /// Token de refresco
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Indica si el usuario está activo
        /// </summary>
        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;
    }
}
