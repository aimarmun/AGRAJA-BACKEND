namespace AgrajaBackend.Models
{
    using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// Modelo de opciones de pago
    /// </summary>
    public class PayOption
    {
        /// <summary>
        /// Id de modelo de pago
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        [Required]
        [StringLength(Constants.PayOptions.MAX_LENGTH_NAME)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción
        /// </summary>
        [StringLength(Constants.PayOptions.MAX_LENGTH_DESCRIPTION)]
        public string? Description { get; set; }

    }
}
