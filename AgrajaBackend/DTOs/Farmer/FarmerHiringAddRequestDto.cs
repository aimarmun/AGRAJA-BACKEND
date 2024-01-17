using AgrajaBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace AgrajaBackend.DTOs.Farmer
{
    /// <summary>
    /// Dto de request de Farmer Hiring
    /// </summary>
    public class FarmerHiringAddRequestDto
    {
        /// <summary>
        /// ID de cliente
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// ID de agricultor
        /// </summary>
        public int FarmerId { get; set; }

        /// <summary>
        /// Fecha y hora de la contratación
        /// </summary>
        public DateTime HiringDateTimeUtz { get; set; } = DateTime.UtcNow;
    }
}
