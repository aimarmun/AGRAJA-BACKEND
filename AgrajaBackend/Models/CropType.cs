using System.ComponentModel.DataAnnotations;
using cons = AgrajaBackend.Constants.CropType;

namespace AgrajaBackend.Models
{
    /// <summary>
    /// Modelo de tipo de cultivo
    /// </summary>
    public class CropType
    {
        /// <summary>
        /// Id
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
    }
}
