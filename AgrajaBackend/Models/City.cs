using System.ComponentModel.DataAnnotations;

namespace AgrajaBackend.Models
{
    /// <summary>
    /// Modelo de ciudad
    /// </summary>
    public class City
    {
        /// <summary>
        /// Id de ciudad
        /// </summary>
        [Key] 
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la ciudad
        /// </summary>
        [Required]
        [StringLength(Constants.City.MAX_LENGTH_NAME)] 
        public string Name { get; set; } = string.Empty;
    }
}
