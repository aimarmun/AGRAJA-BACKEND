using AgrajaBackend.DTOs.Farmer;
using System.ComponentModel.DataAnnotations;

namespace AgrajaBackend.Models
{
    /// <summary>
    /// Modelo agricultor
    /// </summary>
    public class Farmer : PersonData
    {
        /// <summary>
        /// Id de ciudad
        /// </summary>
        [Required]
        public int CityId { get; set; }

        /// <summary>
        /// Tipo de cultivo
        /// </summary>
        [Required]
        public int CropTypeId { get; set; }

        /// <summary>
        /// Parsea un objeto FarmerAddRequestDto a un objeto del modelo Farmer para su guardado en BBDD.
        /// La función también normaliza los espacios vacíos
        /// </summary>
        /// <param name="newFarmer"></param>
        /// <returns>Devuelve el objeto Farmer parseado</returns>
        public static Farmer Parse(FarmerAddRequestDto newFarmer)
        {
            return new()
            {
                Name = newFarmer.Name.Trim(),
                Surnames = newFarmer.Surnames?.Trim(),
                Address = newFarmer.Address?.Trim(),
                CityId = newFarmer.CityId,
                Dni = newFarmer.Dni.ToLower().Trim(),
                CropTypeId = newFarmer.CropTypeId,
                Email = string.IsNullOrEmpty(newFarmer.Email?.Trim())? null : newFarmer.Email?.Trim(),
                Telephone = newFarmer.Telephone?.Trim()
            };
        }
    }
}
