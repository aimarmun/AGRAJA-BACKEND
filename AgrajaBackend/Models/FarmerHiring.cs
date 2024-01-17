using System.ComponentModel.DataAnnotations;

namespace AgrajaBackend.Models
{
    /// <summary>
    /// Modelo de contratación agricultor
    /// </summary>
    public class FarmerHiring
    {
        /// <summary>
        /// Id contratación
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Cliente
        /// </summary>
        [Required]
        public Client Client { get; set; } = new();

        /// <summary>
        /// Agricultor
        /// </summary>
        [Required]
        public Farmer Farmer { get; set; } = new();

        /// <summary>
        /// Fecha y hora de la contratación
        /// </summary>
        [Required]
        public DateTime DateTimeUtc { get; set; } = new();
    }
}
