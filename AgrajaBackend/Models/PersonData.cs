using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using cons = AgrajaBackend.Constants.PersonData;

namespace AgrajaBackend.Models
{
    /// <summary>
    /// Modelo de persona. Lo heredan tanto el modelo cliente com el modelo agricultor
    /// </summary>
    public class PersonData
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// DNI
        /// </summary>
        [Required]
        [StringLength(cons.MAX_LENGHT_DNI)]
        public string Dni { get; set; } = string.Empty;

        /// <summary>
        /// Nombre
        /// </summary>
        [Required]
        [StringLength(cons.MAX_LENGTH_NAME)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos
        /// </summary>
        [StringLength(cons.MAX_LENGTH_SURNAMES)]
        public string? Surnames { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        [StringLength(cons.MAX_LENGTH_ADDRESS)]
        public string? Address { get; set; }

        /// <summary>
        /// Teléfono
        /// </summary>
        [StringLength(cons.MAX_LENGHT_TELEPHONE)]
        public string? Telephone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength (cons.MAX_LENGHT_EMAIL)]
        public string? Email { get; set; }

        /// <summary>
        /// Flag para indicar si la persona está activa o no
        /// </summary>
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;
    }
}
