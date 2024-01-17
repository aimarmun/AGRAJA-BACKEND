using System.ComponentModel.DataAnnotations;

namespace AgrajaBackend.DTOs.Crate
{
    /// <summary>
    /// DTO de entrada de creación de Crate (caja)
    /// </summary>
    public class CrateAddRequestDto
    {
        /// <summary>
        /// Nombre de la cesta
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la cesta
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Kilogramos de la cesta
        /// </summary>
        public double Kilograms { get; set; }

        /// <summary>
        /// Precio de la caja
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Stock de la caja
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Indica si la caja está activa
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
