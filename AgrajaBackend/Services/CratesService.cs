using AgrajaBackend.DTOs.Client;
using AgrajaBackend.DTOs.Crate;
using AgrajaBackend.Models;
using AgrajaBackend.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgrajaBackend.Services
{
    /// <summary>
    /// Servicio de cajas
    /// </summary>
    public class CratesService : ICratesService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del servicio
        /// </summary>
        /// <param name="context">Se inyecta el dbContext</param>
        public CratesService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Comprueba si el nombre de la caja ya existe
        /// </summary>
        /// <param name="name">Nombre de la caja</param>
        /// <param name="crateId">Id de la caja para no incluirlo en la búsqueda</param>
        /// <returns>True si existe</returns>
        public async Task<bool> NameExistsAsync( string name, int crateId = 0)
            => await _context.Crates.AnyAsync(c => c.Id != crateId && c.Name.ToLower() == name.ToLower().Trim());

        /// <summary>
        /// Adquiere un caja por su Id
        /// </summary>
        /// <param name="crateId">Id de la caja</param>
        /// <returns>Caja o null si no existe</returns>
        public async Task<Crate?> GetByIdAsync(int crateId)
            => await _context.Crates.FirstOrDefaultAsync(c => c.Id == crateId);

        /// <summary>
        /// Adquiere todas las cajas
        /// </summary>
        /// <returns>Array de cajas existentes</returns>
        public async Task<Crate[]> GetAllAsync()
            => await _context.Crates.ToArrayAsync();

        /// <summary>
        /// Añade una nueva caja
        /// </summary>
        /// <param name="crate">Caja a añadir</param>
        /// <returns></returns>
        public async Task AddAsync(Crate crate)
        {
            await _context.Crates.AddAsync(crate);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza una caja existente
        /// </summary>
        /// <param name="crate">Caja a actualizar</param>
        /// <returns></returns>
        public async Task UpdateAsync(Crate crate)
        {
            _context.Crates.Update(crate);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obsoleto! elimina una caja.
        /// Se va a eliminar y se sustituira por activar/desactivar
        /// </summary>
        /// <param name="crate"></param>
        /// <returns></returns>
        [Obsolete("No utilizar, en vez de eso utilizar el método para desactivar la caja")]
        public async Task RemoveAsync(Crate crate)
        {
            _context.Crates.Remove(crate);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Añade una venta de caja
        /// </summary>
        /// <param name="crateSale">venta de caja</param>
        /// <returns></returns>
        public async Task AddSaleAsync(CrateSale crateSale)
        {
            await _context.CratesSales.AddAsync(crateSale);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adquiere todas las ventas de una caja por su Id, con ciertas estadísticas
        /// </summary>
        /// <param name="crateId"></param>
        /// <returns></returns>
        public async Task<CrateSaleDto[]?> GetAllSalesByCrateId(int crateId)
        {
            //TODO: En vez de retornar CrateSaleDto, retornar un objeto que a su vez tenga dentro
            //      un objeto ClientDto, CrateDto, etc..
            return  await (
                                 from sale in _context.CratesSales
                                 join client in _context.Clients on sale.Client.Id equals client.Id
                                 where sale.Client.Id == client.Id && sale.Crate.Id == crateId
                                 orderby sale.Id descending
                                 select new CrateSaleDto
                                 {
                                     SaleId = sale.Id,
                                     ClientId = client.Id,
                                     ClientIsActive = client.IsActive,
                                     ClientName = client.Name,
                                     ClientAddress = client.Address,
                                     ClientEmail = client.Email,
                                     ClientSurnames = client.Surnames,
                                     ClientTelephone = client.Telephone,
                                     Amount = sale.Amount,
                                     CrateId = sale.Crate.Id,
                                     CrateDescription = sale.Crate.Description,
                                     CrateKilograms = sale.Crate.Kilograms,
                                     CrateName = sale.Crate.Name,
                                     CratePrice = sale.Crate.Price,
                                     PayOptionName = sale.PayOption.Name,
                                     SaleDateTimeUtz = sale.DateTimeUtc,
                                     TotalPrice = sale.TotalPrice

                                 }
                                 ).ToArrayAsync();
            
        }

        /// <summary>
        /// Asquiere todas las ventas por Id de usuario
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<CrateSaleDto[]?> GetAllSalesByClientId(int clientId)
        {
            //TODO: En vez de retornar CrateSaleDto, retornar un objeto que a su vez tenga dentro
            //      un objeto ClientDto, CrateDto, etc..
            return await (
                                 from sale in _context.CratesSales
                                 join client in _context.Clients on sale.Client.Id equals client.Id
                                 where sale.Client.Id == clientId
                                 orderby sale.Id descending
                                 select new CrateSaleDto
                                 {
                                     SaleId = sale.Id,
                                     ClientId = client.Id,
                                     ClientIsActive = client.IsActive,
                                     ClientName = client.Name,
                                     ClientAddress = client.Address,
                                     ClientEmail = client.Email,
                                     ClientSurnames = client.Surnames,
                                     ClientTelephone = client.Telephone,
                                     Amount = sale.Amount,
                                     CrateId = sale.Crate.Id,
                                     CrateIsActive = sale.Crate.IsActive,
                                     CrateDescription = sale.Crate.Description,
                                     CrateKilograms = sale.Crate.Kilograms,
                                     CrateName = sale.Crate.Name,
                                     CratePrice = sale.Crate.Price,
                                     PayOptionName = sale.PayOption.Name,
                                     SaleDateTimeUtz = sale.DateTimeUtc,
                                     TotalPrice = sale.TotalPrice

                                 }
                                 ).ToArrayAsync();

        }
    }
}
