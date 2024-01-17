using System.ComponentModel.DataAnnotations;

namespace AgrajaBackend.Models
{
    /// <summary>
    /// Modelo de venta de caja
    /// </summary>
    public class CrateSale
    {
        /// <summary>
        /// Id de venta
        /// </summary>
        [Key] 
        public int Id { get; set; }
        
        /// <summary>
        /// Cliente
        /// </summary>
        [Required]
        public Client Client { get; set; } = new();

        /// <summary>
        /// Caja
        /// </summary>
        [Required]
        public Crate Crate { get; set; } = new();

        /// <summary>
        /// Cantidad de cajas vendidas
        /// </summary>
        [Required]
        public int Amount { get; set; }

        /// <summary>
        /// Precio total de la venta
        /// </summary>
        [Required]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Opción de pago
        /// </summary>
        [Required]
        public PayOption PayOption { get; set; } = new();

        /// <summary>
        /// Fecha y hora de la compra
        /// </summary>
        [Required]
        public DateTime DateTimeUtc { get; set; } = DateTime.UtcNow;
    }
}
