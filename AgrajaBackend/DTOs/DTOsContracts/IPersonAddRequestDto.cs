namespace AgrajaBackend.DTOs.Contracts
{
    /// <summary>
    /// Interface persona Dto
    /// </summary>
    public interface IPersonAddRequestDto
    {
        /// <summary>
        /// Dirección
        /// </summary>
        string? Address { get; set; }

        /// <summary>
        /// DNI
        /// </summary>
        string Dni { get; set; }

        /// <summary>
        /// Email 
        /// </summary>
        string? Email { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        string? Surnames { get; set; }

        /// <summary>
        /// Teléfono
        /// </summary>
        string? Telephone { get; set; }

        /// <summary>
        /// Indica si un modelo que hereda de persona está activa o no
        /// </summary>
        bool IsActive { get; set; }
    }
}