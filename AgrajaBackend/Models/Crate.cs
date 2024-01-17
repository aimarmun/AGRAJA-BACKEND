using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using cons = AgrajaBackend.Constants.Crate;

namespace AgrajaBackend.Models
{
    /// <summary>
    /// Modelo de caja
    /// </summary>
    public class Crate
    {
        /// <summary>
        /// Id de caja
        /// </summary>
        [Key] 
        public int Id { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        [Required]
        [StringLength(cons.MAX_LENGTH_NAME)] 
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción
        /// </summary>
        [StringLength(cons.MAX_LENGTH_DESCRIPTION)]
        public string? Description { get; set; }

        /// <summary>
        /// Kilos de la caja
        /// </summary>
        [Required]
        public double Kilograms { get; set; }

        /// <summary>
        /// Precio de la caja
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Stock de cajas
        /// </summary>
        [Required]
        public int Stock { get; set; }

        /// <summary>
        /// Indica si una caja esta activa o no
        /// </summary>
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;

    }
}
