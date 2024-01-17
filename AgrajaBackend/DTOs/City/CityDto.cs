namespace AgrajaBackend.DTOs.City
{
    /// <summary>
    /// Dto de City
    /// </summary>
    public class CityDto
    {
        /// <summary>
        /// Id de ciudad
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de ciudad
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Mapea los datos de un array de modelos City a una lista de CityDto
        /// </summary>
        /// <param name="cities">Array de City</param>
        /// <returns>Lista de CityDto</returns>
        public static List<CityDto> ParseAll(Models.City[] cities)
        {
            List<CityDto> citiesDtos = new();
            foreach(var city in cities)
            {
                citiesDtos.Add(new CityDto
                {
                    Id = city.Id,
                    Name = city.Name,
                });
            }
            return citiesDtos;
        }
    }
}
