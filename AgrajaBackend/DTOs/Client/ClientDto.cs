namespace AgrajaBackend.DTOs.Client
{
    /// <summary>
    /// Dto de cliente de resultado
    /// </summary>
    public class ClientDto
    {
        /// <summary>
        /// Id del cliente
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DNI del cliente
        /// </summary>
        public string Dni { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos del cliente
        /// </summary>
        public string? Surnames { get; set; }

        /// <summary>
        /// Dirección del cliente
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Teléfono del cliente
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// Email del cliente
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Indica si un modelo tipo persona está activa o no
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Mapea un modelo de cliente a un Dto de cliente
        /// </summary>
        /// <param name="client">Modelo de cliente</param>
        /// <returns>Dto de cliente</returns>
        public static ClientDto Parse(Models.Client client)
        {
            return new ClientDto
            {
                Id = client.Id,
                IsActive = client.IsActive,
                Name = client.Name,
                Surnames = client.Surnames,
                Dni = client.Dni,
                Address = client.Address,
                Telephone = client.Telephone,
                Email = client.Email
            };
        }

        /// <summary>
        /// Convierte un array de modelos de cliente a una lista de dtos
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public static List<ClientDto> ParseAll(Models.Client[] clients)
        {
            List<ClientDto> clientsDto = new();
            foreach (var client in clients)
            {
                clientsDto.Add(Parse(client));
            }
            return clientsDto;
        }
    }
}
