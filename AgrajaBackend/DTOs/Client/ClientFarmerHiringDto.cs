namespace AgrajaBackend.DTOs.Client
{
    /// <summary>
    /// Dto que devuelve datos del cliente con fecha de contratación de agriculor e ID del agricultor
    /// </summary>
    public class ClientFarmerHiringDto : ClientDto
    {
        /// <summary>
        /// Id Agricultor
        /// </summary>
        public int FarmerId { get; set; }

        /// <summary>
        /// Id de contrato agricultor
        /// </summary>
        public int HiringId { get; set; }

        /// <summary>
        /// Fecha de contratación
        /// </summary>
        public DateTime HiringDateTime { get; set; }
    }
}
