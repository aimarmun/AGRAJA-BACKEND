using AgrajaBackend.DTOs.Contracts;

namespace AgrajaBackend.DTOs.Client
{
    /// <summary>
    /// Reques dto para actualizar clientes
    /// </summary>
    public class ClientUpdateRequestDto : IPersonAddRequestDto
    {
        /// <summary>
        /// Dirección de cliente
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Dni del cliente
        /// </summary>
        public string Dni { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos
        /// </summary>
        public string? Surnames { get; set; }

        /// <summary>
        /// Teléfono
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// Indica si el cliente está activo, true por defecto.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
