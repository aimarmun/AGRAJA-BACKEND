using AgrajaBackend.DTOs.Crate;
using AgrajaBackend.Models;
using AgrajaBackend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using cons = AgrajaBackend.Constants.Crate;
using Role = AgrajaBackend.Constants.Config.Users.Roles;

namespace AgrajaBackend.Controllers
{
    /// <summary>
    /// Controlador de caja
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class CrateController : ControllerBase
    {

        private readonly ICratesService _cratesService;

        private readonly IClientsService _clientsService;

        private readonly IPayOptionsService _payOptionsService;

        private readonly ILogger<CrateController> _logger;


        /// <summary>
        /// Constructor de controlador de caja
        /// </summary>
        /// <param name="logger">servio de log</param>
        /// <param name="crateService">Servicio de caja</param>
        /// <param name="clientsService">Servicio de cliente</param>
        /// <param name="payOptionsService">Servicio de opciones de pago</param>
        public CrateController
        (
            ILogger<CrateController> logger,
            ICratesService crateService,
            IClientsService clientsService,
            IPayOptionsService payOptionsService
        )
        {
            _logger = logger;
            _cratesService = crateService;
            _clientsService = clientsService;
            _payOptionsService = payOptionsService;
        }

        /// <summary>
        /// Añade una nueva caja a la base de datos
        /// </summary>
        /// <param name="newCrate">Nueva caja en formato json</param>
        /// <returns>Retornará OK si se consigue añadir la caja.
        /// Retornará un 409 si la caja que se intenta añadir ya exite.
        /// Error BadRequest para otros errores</returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Add([FromBody] CrateAddRequestDto newCrate)
        {
            _logger.LogDebug("Solicitud para crear nueva caja \"AddCrate(..)\"");

            if (newCrate == null) return BadRequest("Los datos no son válidos");

            string name = newCrate.Name;
            bool newCreateExists = await _cratesService.NameExistsAsync(name);//_context.Crates.AnyAsync(c => c.Name.ToLower() == name.ToLower());

            if (newCreateExists) return Conflict("Ya existe una caja con el mismo nombre");

            if (name.Length > cons.MAX_LENGTH_NAME)
                return BadRequest($"Excedido los máximos caracteres permitidos para el nombre ({cons.MAX_LENGTH_NAME})");

            string? description = newCrate.Description;
            if (!string.IsNullOrEmpty(description) && description.Trim().Length > cons.MAX_LENGTH_DESCRIPTION)
                return BadRequest($"La descripción no puede superar los {cons.MAX_LENGTH_DESCRIPTION} caracteres");

            Crate crate = ParseCrate(newCrate);

            await _cratesService.AddAsync(crate);

            var crateDto = CrateDto.Parse(crate);

            return Ok(crateDto);
        }


        /// <summary>
        /// Edita una caja existente a través de su Id.
        /// Solo se permite la mofidicación de nombre, descripción y stock
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editedCrate">Json con los nuevos parámetros</param>
        /// <returns>Ok si todo ha ido bien.
        /// NotFound si la caja no se encuentra.</returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> EditCrate([FromRoute] int id, [FromBody] CrateEditRequestDto editedCrate)
        {
            _logger.Log(LogLevel.Debug, "Intentando modificar una caja");

            if (editedCrate == null) return BadRequest("Los datos no son válidos");

            var existedCrate = await _cratesService.GetByIdAsync(id);//_context.Crates.FirstOrDefaultAsync(c => c.Id == crateId);

            if (existedCrate == null) return NotFound("La caja que intentas modificar no existe");

            string name = editedCrate.Name;
            bool existedName = await _cratesService.NameExistsAsync(name, id); //_context.Crates.AnyAsync(c => c.Name.ToLower() == name.ToLower());
            if (existedName) return Conflict("Ya existe una caja con el mismo nombre");

            string? description = editedCrate.Description?.Trim();
            if (!string.IsNullOrEmpty(description) && description.Length > cons.MAX_LENGTH_DESCRIPTION)
                return BadRequest($"Excedido número máximo para la descripción ({cons.MAX_LENGTH_DESCRIPTION})");

            UpdateCrateData(editedCrate, existedCrate);

            await _cratesService.UpdateAsync(existedCrate);

            var crateDto = CrateDto.Parse(existedCrate);

            return Ok(crateDto);
        }

        private static void UpdateCrateData(CrateEditRequestDto editedCrate, Crate existedCrate)
        {
            existedCrate.Name = editedCrate.Name.Trim();
            existedCrate.Description = editedCrate.Description?.Trim();
            existedCrate.IsActive = editedCrate.IsActive;
            existedCrate.Stock = editedCrate.Stock;
        }

        /// <summary>
        /// Retorna todas las cajas existentes
        /// </summary>
        /// <returns>Retorna un array jason con todos los objetos de las cajas</returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAll()
        {
            _logger.LogDebug("Devolviendo todas las cajas");

            var crates = await _cratesService.GetAllAsync();

            if (crates.Length == 0) return NotFound("Todavía no existe ninguna caja");

            var cratesDto = CrateDto.ParseAll(crates);

            return Ok(cratesDto);
        }

        /// <summary>
        /// Adquiere una caja por su ID
        /// </summary>
        /// <param name="crateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{crateId}")]
        public async Task<ActionResult> GetById([FromRoute] int crateId)
        {
            _logger.LogDebug("Adquiriendo caja por su ID {crateId}", crateId);
            var crate = await _cratesService.GetByIdAsync(crateId);
            if (crate == null) return NotFound("La caja que intentas recuperar no existe");

            var crateDto = CrateDto.Parse(crate);

            return Ok(crateDto);
        }

        /// <summary>
        /// Adquire todas las ventas de una caja por su ID
        /// </summary>
        /// <param name="crateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllSales/{crateId}")]
        public async Task<ActionResult> GetAllSalesByCrateId([FromRoute] int crateId)
        {
            _logger.LogDebug("Intentando recuperar todas las ventas de la caja con Id {crateId}", crateId);
            var sales = await _cratesService.GetAllSalesByCrateId(crateId);
            if (sales == null || sales.Length == 0) return NotFound("No se han encontrado ventas");

            return Ok(sales);
        }

        /// <summary>
        /// Devuelve todas las ventas realizadas por un cliente por su Id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllClientSales/{clientId}")]
        public async Task<ActionResult> GetAllSalesByClientId([FromRoute] int clientId)
        {
            _logger.LogDebug("Adquiriendo ventas a cliente Id {clientId}", clientId);
            var sales = await _cratesService.GetAllSalesByClientId(clientId);
            if (sales == null || sales.Length == 0) return NotFound("No se han encontrado ventas");

            return Ok(sales);
        }

        /// <summary>
        /// Elimina una caja por su ID
        /// </summary>
        /// <param name="crateId">Id de la caja</param>
        /// <returns>NotFound si la caja no se encuentra.
        /// OK si la caja ha sido eliminada con éxito</returns>
        [Obsolete("Este método desaparecerá porque se sustituira por activar desactivar")]
        [Authorize(Roles = Role.ADMIN)]
        [HttpDelete]
        [Route("DeleteCrateById")]
        public async Task<ActionResult> DeleteCrateById(int crateId)
        {
            _logger.LogDebug("Borrando caja con id {crateId}", crateId);

            var existedCrate = await _cratesService.GetByIdAsync(crateId);
            if (existedCrate == null) return NotFound("La caja que están intentando borrar no existe");

            await _cratesService.RemoveAsync(existedCrate);

            return Ok(existedCrate);
        }

        /// <summary>
        /// Crea una nueva venta de caja
        /// </summary>
        /// <param name="crateSale">Json con Id de cliente, Id de caja e Id de opción de pago</param>
        /// <returns>Devuelve un json con la información de cliente y caja asociados a la adquisición</returns>
        [HttpPost]
        [Route("CrateSale")]
        public async Task<ActionResult> AddCrateSale([FromBody] CrateSaleRequestDto crateSale)
        {
            //todo comprobar que el número de cajas que se quiere comprar no excede el stock y lanzar excepción.
            _logger.LogDebug("Intentando registrar una venta de caja");

            if (crateSale == null) return BadRequest("Los datos no son válidos");

            var existedClient = await _clientsService.GetByIdAsync(crateSale.ClientId); 
            if (existedClient == null) return NotFound("No se encuentra el cliente");

            var existedCrate = await _cratesService.GetByIdAsync(crateSale.CrateId);
            if (existedCrate == null) return NotFound("No se encuentra la caja");

            if (existedCrate.Stock < crateSale.Amount) return BadRequest("No hay suficiente stock para este artículo");

            existedCrate.Stock -= crateSale.Amount;
            await _cratesService.UpdateAsync(existedCrate);

            var existedPayOption = await _payOptionsService.GetByIdAsync(crateSale.PayOptionId);
            if (existedPayOption == null) return NotFound("No se encuentra la forma de pago");

            var newCrateSale = new CrateSale()
            {
                Client = existedClient,
                Crate = existedCrate,
                Amount = crateSale.Amount,
                TotalPrice = crateSale.Amount * existedCrate.Price,
                PayOption = existedPayOption,
                DateTimeUtc = DateTime.UtcNow 
            };

            await _cratesService.AddSaleAsync(newCrateSale);

            var returNewCrateSale = CrateSaleDto.Parse(newCrateSale);

            return Ok(returNewCrateSale);
        }

        private static Crate ParseCrate(CrateAddRequestDto newCrate)
        {
            return new()
            {
                Description = newCrate.Description?.Trim(),
                IsActive = newCrate.IsActive,
                Kilograms = newCrate.Kilograms,
                Name = newCrate.Name.Trim(),
                Price = newCrate.Price,
                Stock = newCrate.Stock
            };
        }
    }
}
