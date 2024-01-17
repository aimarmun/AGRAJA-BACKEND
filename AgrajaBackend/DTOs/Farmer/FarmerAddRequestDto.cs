using AgrajaBackend.DTOs.Contracts;

namespace AgrajaBackend.DTOs.Farmer
{
    /// <summary>
    /// Request Dto de Farmer
    /// </summary>
    public class FarmerAddRequestDto : IPersonAddRequestDto
    {
        /// <summary>
        /// DNI agricultor
        /// </summary>
        public string Dni { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del agricultor
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos
        /// </summary>
        public string? Surnames { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// ID de ciudad
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Teléfono
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Tipo de cultivo
        /// </summary>
        public int CropTypeId { get; set; }

        /// <summary>
        /// Indica si un agricultor está activo
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
