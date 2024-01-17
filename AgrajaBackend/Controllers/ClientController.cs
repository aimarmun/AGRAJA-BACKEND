using AgrajaBackend.Controllers.Validators;
using AgrajaBackend.DTOs.Client;
using AgrajaBackend.Models;
using AgrajaBackend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role = AgrajaBackend.Constants.Config.Users.Roles;

namespace AgrajaBackend.Controllers
{
    /// <summary>
    /// Controlador de cliente
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientsService _clientsService;

        private readonly IFarmersService _farmersService;
        
        private readonly ILogger<ClientController> _logger;

        private readonly PersonDataValidator _personDataValidator;


        /// <summary>
        /// Constructor de controlador
        /// </summary>
        /// <param name="logger">sevicio de Log</param>
        /// <param name="clientServce">servicio de cliente</param>
        /// <param name="farmersService">servicio de agricultor</param>
        /// <param name="personDataValidator">validador de persona</param>
        public ClientController
        (
            ILogger<ClientController> logger, 
            IClientsService clientServce,
            IFarmersService farmersService,
            PersonDataValidator personDataValidator
        )
        {
            _logger = logger;
            _clientsService = clientServce;
            _farmersService = farmersService;
            _personDataValidator = personDataValidator;
        }

        /// <summary>
        /// Añade un nuevo cliente
        /// </summary>
        /// <param name="newClient"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Add([FromBody] ClientAddRequestDto newClient)
        {
            _logger.LogDebug("Añadiendo nuevo cliente");

            var validateResult = await _personDataValidator.ValidateDataAsync(newClient);
            
            // Si la validación no es correcta se devuelve el ActionResult con el resultado NO OK y cortando el flujo.
            if (validateResult.StatusCode != StatusCodes.Status200OK)
                return validateResult;

            Client client = ParseClient(newClient);

            await _clientsService.AddAsync(client);

            var clientDto = ClientDto.Parse(client);

            return Ok(clientDto);
        }

        /// <summary>
        /// Actualiza datos de un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedData"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> EditById([FromRoute] int id, [FromBody] ClientUpdateRequestDto updatedData)
        {
            _logger.LogDebug("Editando cliente con Id {id}", id);

            var validationResult = await _personDataValidator.ValidateDataAsync(updatedData, id);

            if (validationResult.StatusCode != StatusCodes.Status200OK)
                return validationResult;

            Client? client = await _clientsService.GetByIdAsync(id);
            if (client == null) return NotFound("El cliente no se encuentra");

            UpdateClientData(updatedData, client);

            await _clientsService.UpdateAsync(client);

            var clientDto = ClientDto.Parse(client);

            return Ok(clientDto);
        }

        private static void UpdateClientData(ClientUpdateRequestDto updatedData, Client client)
        {
            client.Name = updatedData.Name;
            client.IsActive = updatedData.IsActive;
            client.Surnames = updatedData.Surnames;
            client.Address = updatedData.Address;
            client.Telephone = updatedData.Telephone;
            client.Email = updatedData.Email;
            client.Dni = updatedData.Dni;
        }

        /// <summary>
        /// Devuelve todos los clientes o un 404 si no existen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAll()
        {
            var clients = await _clientsService.GetAllAsync();
            if (clients == null || clients.Length == 0) return NotFound("No se han encontrado clientes");

            var clientsDto = ClientDto.ParseAll(clients);   
            return Ok(clientsDto);
        }

        /// <summary>
        /// Adquire un cliente por su ID
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{clientId}")]
        public async Task<ActionResult> GetById([FromRoute] int clientId)
        {
            _logger.LogDebug("Adquirir cliente por id {clientId}", clientId);

            var client = await _clientsService.GetByIdAsync(clientId);
            if (client == null) return NotFound("Cliente no encontrado");

            var clientDto = ClientDto.Parse(client); 
            return Ok(clientDto);
        }


        /// <summary>
        /// Adquiere todos los clientes que coinciden con una parte de su dni o nombre
        /// </summary>
        /// <param name="dniOrName"></param>
        /// <param name="onlyActiveClients">Indica true para devolver solo clientes activos</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byPartialDniOrName/{dniOrName}/{onlyActiveClients}")]
        public async Task<ActionResult> GetClientByPartialDni([FromRoute] string dniOrName, 
            [FromRoute] bool onlyActiveClients = false)
        {
            _logger.LogDebug("Adquiriendo clientes por dni parcial '{dniOrName}'", dniOrName);

            var clientByDni = await _clientsService.GetByPartialDniOrNameAsync(dniOrName, onlyActiveClients);
            if (clientByDni == null) return NotFound($"Cliente con DNI '{dniOrName}' no encontrado");

            var clientResultDto = ClientDto.ParseAll(clientByDni);
            return Ok(clientResultDto);
        }

        /// <summary>
        /// Adquire todos los clientes que tienen contratado a un agricultor por el ID del agricultor
        /// </summary>
        /// <param name="farmerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("byFarmerHiring/{farmerId}")]
        public async Task<ActionResult> GetClientsByFarmerHiring([FromRoute] int farmerId)
        {
            _logger.LogDebug("Adquiriendo clientes que han contratado a un agricultor por su Id");
            var clientsHiringsDto = await _farmersService.GetClientsByFarmerHiring(farmerId);
            if (clientsHiringsDto == null) return NotFound("No se encontraron clientes asociados a este agricultor");
           // var clientsDto = ClientDto.ParseAll(clientsHiringsDto);
            return Ok(clientsHiringsDto);
        }

        /// <summary>
        /// Parsea un objeto ClientAddRequestDto a un objeto del modelo Client para su guardado en BBDD.
        /// La función también normaliza los espacios vacíos
        /// </summary>
        /// <param name="newClient"></param>
        /// <returns>Devuelve el objeto Farmer parseado</returns>
        private static Client ParseClient(ClientAddRequestDto newClient)
        {
            return new()
            {
                Name = newClient.Name.Trim(),
                Surnames = newClient.Surnames?.Trim(),
                Address = newClient.Address?.Trim(),
                Dni = newClient.Dni.ToLower().Trim(),
                Email = newClient.Email?.Trim(),
                Telephone = newClient.Telephone?.Trim()
            };
        }

    }
}
