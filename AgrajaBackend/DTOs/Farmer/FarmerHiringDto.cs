using AgrajaBackend.Services.Contracts;

namespace AgrajaBackend.DTOs.Farmer
{
    /// <summary>
    /// Dto de contración de agricultor
    /// </summary>
    public class FarmerHiringDto
    {
        /// <summary>
        /// Id de cliente
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Indica si el cliente está activo o no
        /// </summary>
        public bool ClientIsActive { get; set; } = true;

        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos de cliente
        /// </summary>
        public string? ClientSurNames { get; set; }

        /// <summary>
        /// Email del cliente
        /// </summary>
        public string ClientEmail { get; set; } = string.Empty;

        /// <summary>
        /// Dirección
        /// </summary>
        public string? ClientAddress { get; set; }

        /// <summary>
        /// Teléfono
        /// </summary>
        public string? ClientTelephone { get; set; }

        /// <summary>
        /// Id de agricultor
        /// </summary>
        public int FarmerId { get; set; }

        /// <summary>
        /// Inidica si el agricultor está activo
        /// </summary>
        public bool FarmerIsActive { get; set; } = true;

        /// <summary>
        /// Nombre del agricultor
        /// </summary>
        public string FarmerName { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos agricultor
        /// </summary>
        public string? FarmerSurnames { get; set; }

        /// <summary>
        /// Ciudad del agricultor
        /// </summary>
        public string FarmerCity { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de tipo de cultivo
        /// </summary>
        public string CropName { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora de la contratación
        /// </summary>
        public DateTime SaleDateTimeUtc { get; set; }


    }
}
