using AgrajaBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace AgrajaBackend.DTOs.Farmer
{
    /// <summary>
    /// Dto de Farmer
    /// </summary>
    public class FarmerDto
    {
        /// <summary>
        /// ID de Farmer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DNI
        /// </summary>
        public string Dni { get; set; } = string.Empty;

        /// <summary>
        /// Indica si un agricultor está activo o no
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos
        /// </summary>
        public string? Surnames { get; set; }
    
        /// <summary>
        /// Dirección
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Teléfono
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// ID de ciudad
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// ID De tipo de cultivo
        /// </summary>
        public int CropTypeId { get; set; }

        /// <summary>
        /// Mapea un modelo de tipo Farmer a este Dto
        /// </summary>
        /// <param name="farmer"></param>
        /// <returns></returns>
        public static FarmerDto Parse(Models.Farmer farmer)
        {
            return new FarmerDto
            {
                Id = farmer.Id,
                IsActive = farmer.IsActive,
                Name = farmer.Name,
                Surnames= farmer.Surnames,
                Dni = farmer.Dni,
                Address = farmer.Address,
                Telephone = farmer.Telephone,
                Email = farmer.Email,
                CityId = farmer.CityId,
                CropTypeId = farmer.CropTypeId
            };
        }

        /// <summary>
        /// Mapea un array de modelos Farmer a una lisa de Dtos
        /// </summary>
        /// <param name="farmers"></param>
        /// <returns></returns>
        public static List<FarmerDto> ParseAll(Models.Farmer[] farmers)
        {
            List<FarmerDto> farmersDto = new();

            foreach(var farmer in farmers)
            {
               farmersDto.Add(Parse(farmer));
            }

            return farmersDto;
        }
    }
}
