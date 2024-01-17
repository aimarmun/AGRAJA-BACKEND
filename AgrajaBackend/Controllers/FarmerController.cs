using AgrajaBackend.Controllers.Validators;
using AgrajaBackend.DTOs.Farmer;
using AgrajaBackend.Models;
using AgrajaBackend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role = AgrajaBackend.Constants.Config.Users.Roles;

namespace AgrajaBackend.Controllers
{
    /// <summary>
    /// Controlador de agricultor
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class FarmerController : ControllerBase
    {
        //readonly AppDbContext _context = new();
        private readonly IFarmersService _farmersService;
        private readonly ICropTypesService _croTypesService;
        private readonly IClientsService _clientsService;
        private readonly ILogger<FarmerController> _logger;

        private readonly PersonDataValidator _personValidator;

        /// <summary>
        /// Constructor del controlador de agricultor
        /// </summary>
        /// <param name="logger">servicio de log</param>
        /// <param name="farmersService">servicio de agricultor</param>
        /// <param name="clientsService">servicio de clientes</param>
        /// <param name="cropTypesService">servicio de tipo de cultivos</param>
        /// <param name="personDataValidator">validador de persona</param>
        public FarmerController 
        (
            ILogger<FarmerController> logger, 
            IFarmersService farmersService, 
            IClientsService clientsService,
            ICropTypesService cropTypesService,
            PersonDataValidator personDataValidator
        )
        {
            _logger = logger;
            _farmersService = farmersService;
            _personValidator = personDataValidator;
            _croTypesService = cropTypesService;
            _clientsService = clientsService;
        }

        /// <summary>
        /// Añade un nuevo agricultor
        /// </summary>
        /// <param name="newFarmer">Nuevo agricultor</param>
        /// <returns></returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Add([FromBody] FarmerAddRequestDto newFarmer)
        {
            _logger.LogDebug("Añadiendo agricultor");

            var validateResult = await _personValidator.ValidateDataAsync(newFarmer);

            // Si la validación do devuelve Status200OK significa que algo ha fallado y se devuelve el resultado no OK cortando el flujo.
            if (validateResult.StatusCode != StatusCodes.Status200OK)
                return validateResult;

            Farmer farmer = Farmer.Parse(newFarmer);

            await _farmersService.AddAsync(farmer);

            var farmerDto = FarmerDto.Parse(farmer);

            return Ok(farmerDto);
        }

        /// <summary>
        /// Edita un agricultor por su Id
        /// </summary>
        /// <param name="id">Id del agricultor</param>
        /// <param name="updatedData">Datos modificados del agricultor</param>
        /// <returns></returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> EditById([FromRoute] int id, [FromBody] FarmerUpdateRequestDto updatedData)
        {
            _logger.LogDebug("Editando agricultor con Id {farmerId}", id);

            var validationResult = await _personValidator.ValidateDataAsync(updatedData, id);

            if (validationResult.StatusCode != StatusCodes.Status200OK)
                return validationResult;

            Farmer? farmer = await _farmersService.GetByIdAsync(id);
            if (farmer == null) return NotFound("El agricultor no se encuentra");
            
            UpdateFarmerData(updatedData, farmer);

            await _farmersService.UpdateAsync(farmer);

            var farmerDto = FarmerDto.Parse(farmer);

            return Ok(farmerDto);
        }

        private static void UpdateFarmerData(FarmerUpdateRequestDto editedFarmerData, Farmer farmer)
        {
            farmer.Name = editedFarmerData.Name;
            farmer.IsActive = editedFarmerData.IsActive;
            farmer.Surnames = editedFarmerData.Surnames;
            farmer.Email = editedFarmerData.Email;
            farmer.Address = editedFarmerData.Address;
            farmer.Telephone = editedFarmerData.Telephone;
            farmer.Dni = editedFarmerData.Dni;
        }

        /// <summary>
        /// Adquiere un agricultor por su Id
        /// </summary>
        /// <param name="id">Id del agricultor</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            _logger.LogDebug("Adquiriendo agricutlor por Id {id}", id);
            var farmer = await _farmersService.GetByIdAsync(id);
            if (farmer == null) return NotFound("Agricultor no encontrado");

            var farmerDto = FarmerDto.Parse(farmer);
            return Ok(farmerDto);
        }

        /// <summary>
        /// Devuelve los agricultores que utilicen el tipo de cultivo indicado.
        /// Si se indica 0 (valor por defecto), devolverá todos los agricultores.
        /// </summary>
        /// <param name="cropTypeId">Id del tipo de cultivo</param>
        /// <returns>Devuelve los agricultores que coinciden con el tipo de cultivo</returns>
        [HttpGet]
        [Route("CropType/{cropTypeId}")]
        public async Task<ActionResult> GetByCropType([FromRoute] int cropTypeId = Constants.CropType.Filters.ALL_CROP_TYPES)
        {
            _logger.LogDebug("Adquiriendo agricultores por tipo de cultivo");
            
            if(cropTypeId == Constants.CropType.Filters.ALL_CROP_TYPES)
            {
                var farmers = await _farmersService.GetAllAsync();

                if (farmers.Length == 0) return NotFound("Todavía no existen agricultores");

                var farmersDto = FarmerDto.ParseAll(farmers);

                return Ok(farmersDto);
            }

            var existedCropType = await _croTypesService.GetByIdAsync(cropTypeId);
            if (existedCropType == null)
            {
                return NotFound("No se encuentra el tipo de cultivo");
            }

            var selectedFarmers =
                await _farmersService.GetByCropTypeAsync(cropTypeId);

            if(selectedFarmers.Length == 0)
                return NotFound("No se encuentra ningún(a) agriculor(a) con ese tipo de cultivo.");
            
            var selectedFarmersDto = FarmerDto.ParseAll(selectedFarmers);
            return Ok(selectedFarmersDto);
        }

        /// <summary>
        /// Añade un la contratación de un agricultor por un cliente y devuelve los datos del contrato.
        /// </summary>
        /// <param name="hiring"></param>
        /// <returns>Devuelve un json con los datos del contrato</returns>
        [HttpPost]
        [Route("Hiring")]
        public async Task<ActionResult> AddHiring([FromBody] FarmerHiringAddRequestDto hiring)
        {
            _logger.LogDebug("Añadiendo nueva reserva de agricultor");

            if (hiring == null) return BadRequest("Datos no válidos");

            int clientId = hiring.ClientId;
            int farmerId = hiring.FarmerId;

            var existedClient = await _clientsService.GetByIdAsync(clientId);
            if (existedClient == null) return NotFound("Cliente no encontrado");

            var existedFarmer = await _farmersService.GetByIdAsync(farmerId);
            if (existedFarmer == null) return NotFound("Agricultor no encontrado");

            var existedFarmerHirings = await _farmersService.GetHiringsByFarmerIdAsync(farmerId);
            if(existedFarmerHirings != null)
            {
                if(existedFarmerHirings.Any(h => h.Client.Id == clientId))
                {
                    return Conflict("El agricultor ya tiene en su lista a este cliente");
                }
            }

            FarmerHiring farmerHiring = new()
            {
                Client = existedClient,
                Farmer = existedFarmer,
                DateTimeUtc = DateTime.UtcNow,
            };
            
            await _farmersService.AddHiringAsync(farmerHiring);

            var hiringDto = await _farmersService.GetHiringDto(farmerHiring);//await FarmerHiringDto.ParseAsync(_citiesService, _croTypesService, farmerHiring);

            return Ok(hiringDto);
        }

        /// <summary>
        /// Devuelve un lista de contrataciones con solo IDs de un agricultor por su ID
        /// </summary>
        /// <param name="farmerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Hiring/{farmerId}")]
        public async Task<ActionResult> GetHiringsByFarmerId([FromRoute] int farmerId)
        {
            _logger.LogDebug("Adquiriendo contrataciones de agricultor por id {farmerId}", farmerId);
            var farmer = await _farmersService.GetByIdAsync(farmerId);
            if (farmer == null) return NotFound("Agricultor no encontrado");
            var hirings = await _farmersService.GetHiringsByFarmerIdAsync(farmerId);
            if (hirings == null) return NotFound("Contrataciones no encontradas");

            var dtos = FarmerHiringIDsDto.ParseAll(hirings);

            return Ok(dtos);

        }

        /// <summary>
        /// Devuelve todos los dtos de contrataciones de un cliente
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Hiring/ByClient/{clientId}")]
        public async Task<ActionResult> GetHiringsByClientId([FromRoute] int clientId)
        {
            _logger.LogDebug("Adquiriendo contratos por id de cliente {clientId}", clientId);
            var client = await _clientsService.GetByIdAsync(clientId);
            if (client == null) return NotFound("Cliente no encontrado");
            var hirings = await _farmersService.GetHiringsByClientIdAsync(clientId);
            if (hirings == null || hirings.Length == 0) return NotFound("No se han encontrado contratos para este cliente");

            var dtos = await _farmersService.GetHiringsDtos(client);
            if (dtos == null || dtos.Length == 0) return NotFound("Este cliente no tiene contrataciones");
            
            return Ok(dtos);
        }

        /// <summary>
        /// Elimina una contratatación por el Id del agricultor y el id del cliente
        /// </summary>
        /// <param name="farmerId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [Obsolete("Metodo obsoleto utiliza HttpDelete por ID de contrato")]
        [Authorize(Roles = Role.ADMIN)]
        [HttpDelete]
        [Route("Hiring/{farmerId}/{clientId}")]
        public async Task<ActionResult> DeleteHiringsByFarmerAndClientId([FromRoute] int farmerId, [FromRoute] int clientId)
        {
            _logger.LogDebug("Eliminando contratación de agricultor por agricultor id {farmerId} y cliente Id {clientId}", farmerId, clientId);
            var farmer = await _farmersService.GetByIdAsync(farmerId);
            if (farmer == null) return NotFound("Agricultor no encontrado");

            var client = await _clientsService.GetByIdAsync(clientId);
            if (client == null) return NotFound("Cliente no encontrado");

            var hirings = await _farmersService.GetHiringsByFarmerIdAsync(farmer.Id);
            if (hirings == null) return NotFound("Contrato no encontrado");


            if(hirings.Any(h => h.Client.Id == client.Id))
            {
                var hirignsClient = hirings.Where(h => h.Client.Id == clientId).ToArray();
                await _farmersService.RemoveHirings(hirignsClient);
            }
            else
            {
                return NotFound("Contrato no encontrado");
            }

            return Ok();
        }

        /// <summary>
        /// Elimina una contratación por su Id
        /// </summary>
        /// <param name="hiringId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = Role.ADMIN)]
        [Route("Hiring/{hiringId}")]
        public async Task<ActionResult> DeleteHiringByIdAsync([FromRoute] int hiringId)
        {
            _logger.LogDebug("Eliminando contrato por id ${hiringId}", hiringId);
            var hiring = await _farmersService.GetHiringById(hiringId);
            if (hiring == null) return NotFound("El contrato que quieres eliminar no existe");

            var error = await _farmersService.RemoveHirings(new[] { hiring });
            if (error != null) return BadRequest($"Algo salio mal: {error.Message}");

            return Ok();
        }
    }
}
