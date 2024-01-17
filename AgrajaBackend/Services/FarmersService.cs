using AgrajaBackend.DTOs.Client;
using AgrajaBackend.DTOs.Farmer;
using AgrajaBackend.Models;
using AgrajaBackend.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgrajaBackend.Services
{

    /// <summary>
    /// Servicio de agricultor
    /// </summary>
    public class FarmersService : IFarmersService
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del servicio
        /// </summary>
        /// <param name="context">Se inyecta el dbContext</param>
        public FarmersService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adquiere todos los agricultores
        /// </summary>
        /// <returns>Array de agricultores</returns>
        public async Task<Farmer[]> GetAllAsync()
            => await _context.Farmers.ToArrayAsync();

        /// <summary>
        /// Adquire todos los agricultores que tengan un tipo de cultivo concreto
        /// </summary>
        /// <param name="cropType">Id del tipo cultivo</param>
        /// <returns></returns>
        public async Task<Farmer[]> GetByCropTypeAsync(int cropType)
            => await _context.Farmers.Where(f => f.CropTypeId == cropType).ToArrayAsync();

        /// <summary>
        /// Añade un agricultor a la base de datos
        /// </summary>
        /// <param name="farmer">Nuevo agricultor</param>
        /// <returns></returns>
        public async Task AddAsync(Farmer farmer)
        {
            await _context.Farmers.AddAsync(farmer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza un agricutlor
        /// </summary>
        /// <param name="farmer">Agricultor modificado</param>
        /// <returns></returns>
        public async Task UpdateAsync(Farmer farmer)
        {
            _context.Farmers.Update(farmer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Añade una contratación de un agricultor con un cliente.
        /// </summary>
        /// <param name="farmerHiring">Nueva contratación</param>
        /// <returns></returns>
        public async Task AddHiringAsync(FarmerHiring farmerHiring)
        {
            await _context.FarmerHirings.AddAsync(farmerHiring);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Verifica si un DNI ya está en uso en algún agricultor.
        /// Se debe indicar el Id del agricultor que se debe ignorar para la comprobación.
        /// Esto sirve para que se permita guardar el mismo DNI del agricultor que se quiere modificar.
        /// </summary>
        /// <param name="personId">Se especifica el agricultor que se quiere ignorar</param>
        /// <param name="dni"></param>
        /// <returns></returns>
        [Obsolete("No utilizar, usar en su lugar GetByDniAsync para ver si un DNI está en uso")]
        public async Task<bool> IsDniInUseAsync(int personId, string dni)
            => await _context.Farmers.AnyAsync(f => f.Id != personId && f.Dni == dni.ToLower());

        /// <summary>
        /// Devuelve un agricultor por su DNI
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        
        public async Task<Farmer?> GetByDniAsync(string dni)
            => await _context.Farmers.OrderBy(f => f.Id)
                .LastOrDefaultAsync(f => f.Dni.ToLower().Equals(dni.ToLower().Trim()));

        /// <summary>
        /// Adquiere un agricultor por su email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<Farmer?> GetByEmailAsync(string email)
            => await _context.Farmers.OrderBy(f => f.Id)
               .LastOrDefaultAsync(f => f.Email != null && f.Email.ToLower().Equals(email.ToLower().Trim()));

        /// <summary>
        /// Verifica si un e-mail está en uso por algún otro agricultor.
        /// Se debe indicar el Id del agricultor que se debe ignorar para la comprobación.
        /// Esto sirve para que se permita guardar el mismo e-mail del agricultor que se quiere modificar.
        /// La razón es que normalmente la modificación de un agricultor solo se hara de alguna de sus propiedades.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [Obsolete("Utilizar en su lugar GetByEmailAsync en su lugar para averiguar si un email está en uso")]
        public async Task<bool> IsEmailInUseAsync(int personId, string? email)
        {
            string? normalizedEmail = email?.ToLower().Trim();
            return await _context.Farmers.AnyAsync(f => f.Id != personId && f.Email == normalizedEmail && f.Email != null);
        }

        /// <summary>
        /// Adquiere un agricultor por su Id
        /// </summary>
        /// <param name="farmerId">Id del agricultor</param>
        /// <returns>Agricultor o null si no se encuentra</returns>
        public async Task<Farmer?> GetByIdAsync(int farmerId)
            => await _context.Farmers.FirstOrDefaultAsync(f => f.Id == farmerId);

        /// <summary>
        /// Adquire un array de contrataciones de un agricultor por su id
        /// </summary>
        /// <param name="farmerId"></param>
        /// <returns></returns>
        public async Task<FarmerHiring[]?> GetHiringsByFarmerIdAsync(int farmerId)
            => await _context.FarmerHirings.Where(hirings => hirings.Farmer.Id == farmerId).ToArrayAsync();

        /// <summary>
        /// Adquire todas las contrataciones de un agricutor realizadas por un cliente
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<FarmerHiring[]?> GetHiringsByClientIdAsync(int clientId)
            => await _context.FarmerHirings.Where(hiring => hiring.Client.Id == clientId).ToArrayAsync();

        /// <summary>
        /// Devuelve todos los clientes que tienen contratado a un agricultor por su ID
        /// </summary>
        /// <param name="farmerId"></param>
        /// <returns></returns>
        public async Task<ClientFarmerHiringDto[]?> GetClientsByFarmerHiring(int farmerId)
            => await (from hiring in _context.FarmerHirings
                      join farmer in _context.Farmers on hiring.Farmer.Id equals farmer.Id
                      join client in _context.Clients on hiring.Client.Id equals client.Id
                      where farmer.Id == farmerId
                      select new ClientFarmerHiringDto
                      {
                          Name = client.Name,
                          Address = client.Address,
                          Dni = client.Dni,
                          Email = client.Email,
                          Id = client.Id,
                          IsActive = client.IsActive,
                          Surnames = client.Surnames,
                          Telephone = client.Telephone,
                          FarmerId = farmerId,
                          HiringId = hiring.Id,
                          HiringDateTime = hiring.DateTimeUtc
                      }).ToArrayAsync();

        /// <summary>
        /// Adquire un dto de contratación por modelo FarmerHiring
        /// </summary>
        /// <param name="hiring"></param>
        /// <returns></returns>
        public async Task<FarmerHiringDto?> GetHiringDto(FarmerHiring hiring)
            => await (from h in _context.FarmerHirings
                              join cropType in _context.CropTypes on h.Farmer.CropTypeId equals cropType.Id
                              join city in _context.Cities on h.Farmer.CityId equals city.Id
                              where h.Id == hiring.Id
                              select new FarmerHiringDto
                              { //todo: esto habria que cambiarlo por objetos de cada dto
                                  ClientId = hiring.Client.Id,
                                  ClientIsActive = hiring.Client.IsActive,
                                  ClientName = hiring.Client.Name,
                                  ClientSurNames = hiring.Client.Surnames,
                                  ClientEmail = hiring.Client.Email ?? "correo desconocido",
                                  ClientTelephone = hiring.Client.Telephone,
                                  ClientAddress = hiring.Client.Address,
                                  FarmerId = hiring.Farmer.Id,
                                  FarmerIsActive = hiring.Client.IsActive,
                                  FarmerName = hiring.Farmer.Name,
                                  FarmerSurnames = hiring.Farmer.Surnames,
                                  FarmerCity = city.Name,
                                  CropName = cropType.Name,
                                  SaleDateTimeUtc = hiring.DateTimeUtc
                              }).FirstAsync();
          
        /// <summary>
        /// Devuelve todos los dtos de contrataciones de un cliente
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<FarmerHiringDto[]?> GetHiringsDtos(Client client)
            => await (from hiring in _context.FarmerHirings
                      join cropType in _context.CropTypes on hiring.Farmer.CropTypeId equals cropType.Id
                      join city in _context.Cities on hiring.Farmer.CityId equals city.Id
                      where hiring.Client.Id == client.Id
                      select new FarmerHiringDto
                      {
                          ClientId = hiring.Client.Id,
                          ClientIsActive = hiring.Client.IsActive,
                          ClientName = hiring.Client.Name,
                          ClientSurNames = hiring.Client.Surnames,
                          ClientEmail = hiring.Client.Email ?? "correo desconocido",
                          ClientTelephone = hiring.Client.Telephone,
                          ClientAddress = hiring.Client.Address,
                          FarmerId = hiring.Farmer.Id,
                          FarmerIsActive = hiring.Farmer.IsActive,
                          FarmerName = hiring.Farmer.Name,
                          FarmerSurnames = hiring.Farmer.Surnames,
                          FarmerCity = city.Name,
                          CropName = cropType.Name,
                          SaleDateTimeUtc = hiring.DateTimeUtc
                      }).ToArrayAsync();



        /// <summary>
        /// Elimina contrataciones
        /// </summary>
        /// <param name="farmerHirings">Array de contrataciones</param>
        /// <returns></returns>
        public async Task<Exception?> RemoveHirings(FarmerHiring[] farmerHirings)
        {
            try
            {
                _context.RemoveRange(farmerHirings);
                await _context.SaveChangesAsync();
                return null;
            }catch (Exception ex) {
                return ex;
            }
        }

        /// <summary>
        /// Adquire una contratación por su Id
        /// </summary>
        /// <param name="hiringId"></param>
        /// <returns></returns>
        public async Task<FarmerHiring?> GetHiringById(int hiringId)
            => await _context.FarmerHirings.FindAsync(hiringId);
    }
}
