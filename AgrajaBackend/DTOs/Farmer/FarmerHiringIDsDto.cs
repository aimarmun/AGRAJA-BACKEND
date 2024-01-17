namespace AgrajaBackend.DTOs.Farmer
{
    /// <summary>
    /// Dto de contrataciones de agricultor
    /// </summary>
    public class FarmerHiringIDsDto
    {

        /// <summary>
        /// Id del registro
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id del cliente
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Id del agricultor
        /// </summary>
        public int FarmerId { get; set; }

        /// <summary>
        /// Fecha de contratación
        /// </summary>
        public DateTime DateHiringDateTimeUtz { get; set; }

        /// <summary>
        /// Mapea un modelo de contratación a un Dto
        /// </summary>
        /// <param name="hiring"></param>
        /// <returns></returns>
        public static FarmerHiringIDsDto Parse(Models.FarmerHiring hiring)
        {
            return new FarmerHiringIDsDto { 
                Id = hiring.Id, 
                ClientId = hiring.Client.Id, 
                FarmerId = hiring.Farmer.Id,
                DateHiringDateTimeUtz = hiring.DateTimeUtc
            };
        }

        /// <summary>
        /// Mapea un array de modelo de contratación a una lista de dtos
        /// </summary>
        /// <param name="hirings"></param>
        /// <returns></returns>
        public static List<FarmerHiringIDsDto> ParseAll(Models.FarmerHiring[] hirings)
        {
            List<FarmerHiringIDsDto> dtos = new();
            foreach(var hiring in hirings)
            {
                dtos.Add(Parse(hiring));
            }
            return dtos;
        }
    }
}
