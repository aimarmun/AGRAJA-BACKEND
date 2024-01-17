namespace AgrajaBackend.DTOs.Crate
{
    /// <summary>
    /// Dto de request de venta de caja
    /// </summary>
    public class CrateSaleRequestDto
    {
        /// <summary>
        /// ID del cliente
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// ID de caja
        /// </summary>
        public int CrateId { get; set; }

        /// <summary>
        /// Cantidad
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// ID de opción de pago
        /// </summary>
        public int PayOptionId { get; set; }

        /// <summary>
        /// Fecha y hora de venta
        /// </summary>
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

    }
}
