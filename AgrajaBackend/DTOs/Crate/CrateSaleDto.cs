using AgrajaBackend.Models;

namespace AgrajaBackend.DTOs.Crate
{
    /// <summary>
    /// Dto de venta de caja
    /// </summary>
    public class CrateSaleDto
    {
        /// <summary>
        /// Id de la venta
        /// </summary>
        public int SaleId { get; set; }

        /// <summary>
        /// Id del cliente
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Indica si el cliente está activo
        /// </summary>
        public bool ClientIsActive { get; set; }

        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Apellidos del cliente
        /// </summary>
        public string? ClientSurnames { get; set; }

        /// <summary>
        /// Dirección del cliente
        /// </summary>
        public string? ClientAddress { get; set; }

        /// <summary>
        /// Teléfono del cliente
        /// </summary>
        public string? ClientTelephone { get; set; }

        /// <summary>
        /// Email del cliente
        /// </summary>
        public string? ClientEmail { get; set; }

        /// <summary>
        /// Id de la caja
        /// </summary>
        public int CrateId { get; set; }

        /// <summary>
        /// Indica si la caja está activa
        /// </summary>
        public bool CrateIsActive { get; set; }

        /// <summary>
        /// Nombre de la caja
        /// </summary>
        public string CrateName { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la caja
        /// </summary>
        public string? CrateDescription { get; set; }

        /// <summary>
        /// Cantidad vendida
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Precio total de venta
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Kilos de la caja
        /// </summary>
        public double CrateKilograms { get; set; }

        /// <summary>
        /// Precio de la caja
        /// </summary>
        public decimal CratePrice { get; set; }

        /// <summary>
        /// Opción de pago
        /// </summary>
        public string PayOptionName { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora de la compra
        /// </summary>
        public DateTime SaleDateTimeUtz { get; set; }

        /// <summary>
        /// Mapeo un modelo de venta de caja en un Dto
        /// </summary>
        /// <param name="crateSale">Modelo de venta de caja</param>
        /// <returns>Dto de venta</returns>
        public static CrateSaleDto Parse(CrateSale crateSale)
        {
            //TODO: En vez de retornar CrateSaleDto, retornar un objeto que a su vez tenga dentro
            //      un objeto ClientDto, CrateDto, etc..
            var client = crateSale.Client;
            var crate = crateSale.Crate;
            var payOption = crateSale.PayOption;

            return new CrateSaleDto
            {
                SaleId = crateSale.Id,
                ClientId = client.Id,
                ClientIsActive = client.IsActive,
                ClientName = client.Name,
                ClientSurnames = client.Surnames,
                ClientAddress = client.Address,
                ClientTelephone = client.Telephone,
                ClientEmail = client.Email,
                CrateId = crateSale.Crate.Id,
                CrateIsActive = crateSale.Crate.IsActive,
                CrateName = crate.Name,
                CrateDescription = crate.Description,
                CrateKilograms = crate.Kilograms,
                CratePrice = crate.Price,
                Amount = crateSale.Amount,
                TotalPrice = crateSale.TotalPrice,
                PayOptionName = payOption.Name,
                SaleDateTimeUtz = crateSale.DateTimeUtc
            };
        }
    }
}
