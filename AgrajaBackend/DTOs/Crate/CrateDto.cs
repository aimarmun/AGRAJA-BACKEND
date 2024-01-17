namespace AgrajaBackend.DTOs.Crate
{
    /// <summary>
    /// Dto de caja
    /// </summary>
    public class CrateDto
    {
        /// <summary>
        /// Id de la caja
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Indica si la caja está activa
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Nombre de la caja
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la caja
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Kilogramos de la caja
        /// </summary>
        public double Kilograms { get; set; }

        /// <summary>
        /// Precio de la caja
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Stock de la caja
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Mapea el modelo de caja a un Dto de resultado
        /// </summary>
        /// <param name="crate">Modelo de caja</param>
        /// <returns>Dto de caja</returns>
        public static CrateDto Parse(Models.Crate crate)
        {
            return new CrateDto()
            {
                Id = crate.Id,
                IsActive = crate.IsActive,
                Name = crate.Name,
                Description = crate.Description,
                Kilograms = crate.Kilograms,
                Price = crate.Price,
                Stock = crate.Stock
            };
        }

        /// <summary>
        /// Mapea un array de modelos de caja a una lista de Dto de caja
        /// </summary>
        /// <param name="crates">Array de cajas</param>
        /// <returns>Lista de dtos</returns>
        public static List<CrateDto> ParseAll(Models.Crate[] crates)
        {
            List<CrateDto> cratesDto = new();
            foreach (var crate in crates) 
            {
                cratesDto.Add(Parse(crate));
            }
            return cratesDto;
        }
    }
}
