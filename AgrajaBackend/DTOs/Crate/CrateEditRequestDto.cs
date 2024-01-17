namespace AgrajaBackend.DTOs.Crate
{
    /// <summary>
    /// Dto de edición de caja
    /// </summary>
    public class CrateEditRequestDto
    {
        /// <summary>
        /// Nombre de la caja
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la caja
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Stock de cajas
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Indica si la caja esta activa
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
