namespace AgrajaBackend.DTOs.PayOption
{
    /// <summary>
    /// Dto de opciones de pago
    /// </summary>
    public class PayOptionDto
    {
        /// <summary>
        /// ID de payOption
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Mapea un modelo de PayOption a su Dto
        /// </summary>
        /// <param name="payOption"></param>
        /// <returns></returns>
        public static PayOptionDto Parse(Models.PayOption payOption)
        {
            return new PayOptionDto
            {
                Id = payOption.Id,
                Name = payOption.Name,
                Description = payOption.Description
            };
        }

        /// <summary>
        /// Mapea un array de modelo de opciones de pago a un lista de sus Dto
        /// </summary>
        /// <param name="payOptions"></param>
        /// <returns></returns>
        public static List<PayOptionDto> ParseAll(Models.PayOption[] payOptions) 
        {
            List<PayOptionDto> payOptionsDto = new();
            foreach (var payOption in payOptions)
            {
                payOptionsDto.Add(Parse(payOption));
            }

            return payOptionsDto;
        }
    }
}
