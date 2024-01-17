namespace AgrajaBackend.DTOs.Farmer
{
    /// <summary>
    /// Dto de agricultor
    /// </summary>
    public class FarmersDto
    {
        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indica si el agricultor está activo
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// DNI
        /// </summary>
        public string Dni { get; set; } = string.Empty;

        /// <summary>
        /// ID de la ciudad
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// ID de tipo de cultivo
        /// </summary>
        public int CropTypeId { get; set; } 

        /// <summary>
        /// Mapea un array de modelo agricultor a una lista de sus Dto
        /// </summary>
        /// <param name="farmers"></param>
        /// <returns></returns>
        public static List<FarmersDto> ParseAll(Models.Farmer[] farmers)
        {
            var farmersList = new List<FarmersDto>();
            foreach(var farmer in farmers)
            {
                farmersList.Add(
                    new FarmersDto
                    {
                        Name = farmer.Name,
                        IsActive = farmer.IsActive,
                        Dni = farmer.Dni,
                        CityId = farmer.CityId,
                        CropTypeId = farmer.CropTypeId
                    }
                );
            }
            return farmersList;
        }
    }
}
