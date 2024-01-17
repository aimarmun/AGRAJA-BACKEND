using System.ComponentModel.DataAnnotations;
using AgrajaBackend.DTOs.Contracts;

namespace AgrajaBackend.DTOs.Client
{
    /// <summary>
    /// Dto de añadido de cliente
    /// </summary>
    public class ClientAddRequestDto : IPersonAddRequestDto
    {
        /// <summary>
        /// DNI Cliente
        /// </summary>
        public string Dni { get; set; } = string.Empty;


        /// <summary>
        /// Nombre de cliente
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos del cliente
        /// </summary>
        public string? Surnames { get; set; }

        /// <summary>
        /// Dirección del cliente
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Teléfono del cliente
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// Email del cliente
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Indica si un modelo tipo persona esta activo o no
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
