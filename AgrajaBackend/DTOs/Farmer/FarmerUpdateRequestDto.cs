using AgrajaBackend.DTOs.Contracts;

namespace AgrajaBackend.DTOs.Farmer
{
    /// <summary>
    /// Dto de actualización de agricultor
    /// </summary>
    public class FarmerUpdateRequestDto : IPersonAddRequestDto
    {
        /// <summary>
        /// DNI agricultor
        /// </summary>
        public string Dni { get; set; } = string.Empty;

        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos agricultor
        /// </summary>
        public string? Surnames { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Teléfono
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Indica si el agricultor está activo
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
