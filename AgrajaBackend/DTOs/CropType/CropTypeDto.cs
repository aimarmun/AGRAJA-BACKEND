namespace AgrajaBackend.DTOs.CropType
{
    /// <summary>
    /// Dto de tipo cultivo
    /// </summary>
    public class CropTypeDto
    {

        /// <summary>
        /// Id de tipo de cultivo
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del tipo de cultivo
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del tipo de cultivo
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Mapea una array de modelo tipo cultvio a una lista de Dtos
        /// </summary>
        /// <param name="cropTypes">Array de tipos de caja</param>
        /// <returns>Lista con Dtos tipo de caja</returns>
        public static List<CropTypeDto> ParseAll(Models.CropType[] cropTypes)
        {
            List<CropTypeDto> cropTypesDtos = new();
            foreach (var cropType in cropTypes) 
            {
                cropTypesDtos.Add(
                    new CropTypeDto
                    {
                        Id = cropType.Id,
                        Name = cropType.Name,
                        Description = cropType.Description
                    }
               );
            }
            return cropTypesDtos;
        }
    }
}
