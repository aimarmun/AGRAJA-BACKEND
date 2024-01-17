using AgrajaBackend.DTOs.Client;
using AgrajaBackend.DTOs.Contracts;
using AgrajaBackend.DTOs.Farmer;
using AgrajaBackend.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using cons = AgrajaBackend.Constants.PersonData;

namespace AgrajaBackend.Controllers.Validators
{
    /// <summary>
    /// Clase que sirve para validar una persona
    /// </summary>
    public class PersonDataValidator : ControllerBase
    {
        private readonly IClientsService _clientsService;
        private readonly IFarmersService _farmersService;
        private readonly ICitiesService _citiesService;
        private readonly ICropTypesService _cropTypesService;
        private readonly ILogger<PersonDataValidator> _logger;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="clientsService"></param>
        /// <param name="farmersService"></param>
        /// <param name="citiesService"></param>
        /// <param name="cropTypesService"></param>
        /// <param name="logger"></param>
        public PersonDataValidator
        (
            IClientsService clientsService, 
            IFarmersService farmersService,
            ICitiesService citiesService,
            ICropTypesService cropTypesService,
            ILogger<PersonDataValidator> logger
        )
        {
            _clientsService = clientsService;
            _farmersService = farmersService;
            _citiesService = citiesService;
            _cropTypesService = cropTypesService;
            _logger = logger;
        }


        /// <summary>
        /// Genera un ObjectResult con el código de estado. 200 si todo Ok.
        /// Sirve para validar si Person cumple con todas las validaciones.
        /// Person puede ser Farmer o Client.
        /// </summary>
        /// <param name="newPerson"></param>
        /// <param name="personId">Parámetro opcional. Si se especifica clientId entonces se quiere validar la edición de un cliente existente</param>
        /// <returns>Devuelve </returns>
        public async Task<ObjectResult> ValidateDataAsync(IPersonAddRequestDto newPerson, int personId = 0)
        {
            if (newPerson == null) return BadRequest("No se han proporcionado datos");

            
            bool isClient = newPerson is ClientAddRequestDto || newPerson is ClientUpdateRequestDto;
            bool isFarmer = newPerson is FarmerAddRequestDto;
            bool isFarmerUpdate = newPerson is FarmerUpdateRequestDto;


            if(!isClient && !isFarmer && !isFarmerUpdate)
            {
                _logger.LogError("Solo se puede validar un cliente o un agricultor");
                throw new Exception("El parámetro newPerson debe ser de tipo IPersonAddRequestDto");
            }

            _logger.LogDebug("Intentando validar datos de {farmerOrClient}", (isClient ? "cliente" : "agricultor"));

            if (isFarmer || isFarmerUpdate)
            {
                return await ValidateFarmerData(newPerson, personId, isFarmerUpdate);
            }
            

            return await ValidateClientData(newPerson, personId);
           

        }

        /// <summary>
        /// Validación para persona de tipo Agricultor
        /// </summary>
        /// <param name="newPerson"></param>
        /// <param name="personId"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        private async Task<ObjectResult> ValidateFarmerData(IPersonAddRequestDto newPerson, int personId = 0, bool isUpdate = false)
        {
            string name = newPerson.Name.Trim();
            if (string.IsNullOrEmpty(name))
                return BadRequest("El nombre no puede estar vacío");

            if (name.Length > cons.MAX_LENGTH_NAME)
                return BadRequest($"El nombre {name} exceder de los {cons.MAX_LENGTH_NAME} caracteres");

            string? surnames = newPerson.Surnames?.Trim();
            if (!string.IsNullOrEmpty(surnames) && surnames.Length > cons.MAX_LENGTH_SURNAMES)
                return BadRequest($"Los apellidos no pueden exceder los {cons.MAX_LENGTH_SURNAMES} caracteres");

            string dni = newPerson.Dni;
            if (!Utils.Dni.IsValid(dni)) return BadRequest(dni + " no es un DNI válido");
            var farmerByDni = await _farmersService.GetByDniAsync(dni);
            if (farmerByDni != null && farmerByDni.Id != personId) return BadRequest($"Ya existe un agricultor con este DNI: {dni.Trim()}." +
                $"{(farmerByDni.IsActive ? "" : $"{Environment.NewLine}⚠️El agricultor existente se encuentra oculto⚠️")}");

            string? telephone = newPerson.Telephone;
            if (!string.IsNullOrEmpty(telephone) && !Utils.Telephone.IsValid(telephone))
                return BadRequest("El formato de teléfono no es correcto");

            string? email = newPerson.Email;
            if (!string.IsNullOrEmpty(email) && !Utils.Email.IsValid(email))
                return BadRequest($"'{email}' no es una dirección de correo válida");

            
            string? normalizedEmail = email?.ToLower().Trim();
            if (!string.IsNullOrEmpty(normalizedEmail))
            {
                var farmerByEmail = await _farmersService.GetByEmailAsync(normalizedEmail); 
                if (farmerByEmail != null && farmerByEmail.Id != personId) return BadRequest($"El correo electrónico {email?.Trim()} ya se encuentra en uso." +
                    $"{(farmerByEmail.IsActive ? "": $"{Environment.NewLine}⚠️El agricultor existente se encuentra oculto⚠️")}");
            }
            

            string? address = newPerson.Address;
            if (!string.IsNullOrEmpty(address) && address.Length > cons.MAX_LENGTH_ADDRESS)
                return BadRequest($"La dirección no pude contener más de {cons.MAX_LENGTH_ADDRESS}");
            
            if (!isUpdate)
            {
                int cityId = ((FarmerAddRequestDto)newPerson).CityId;
                _logger.LogDebug("Comprar si existe ciudad con Id: {cityId}", cityId);
                bool cityExists = await _citiesService.ExistsByIdAsync(cityId);
                if (!cityExists) return NotFound("Ciudad no encontrada");

                int cropTypeId = ((FarmerAddRequestDto)newPerson).CropTypeId;
                _logger.LogDebug("Comprobar si existe tipo de cultivo con Id: {cropTypeId}", cropTypeId);
                bool cropTypeIdExists = await _cropTypesService.ExistsByIdAsync(cropTypeId);
                if (!cropTypeIdExists) return NotFound("Tipo de cultivo no encontrado");
            }

            _logger.LogInformation("La comprobación de agricultor ha sido correcta");
            return new ObjectResult("Ok")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }


        /// <summary>
        /// Validación para persona de tipo cliente
        /// </summary>
        /// <param name="newPerson"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        private async Task<ObjectResult> ValidateClientData(IPersonAddRequestDto newPerson, int personId = 0)
        {
            string name = newPerson.Name;
            if (string.IsNullOrEmpty(name))
                return BadRequest("El nombre no puede estar vacío");

            if (name.Length > cons.MAX_LENGTH_NAME)
                return BadRequest($"El nombre {name} exceder de los {cons.MAX_LENGTH_NAME} caracteres");

            string? surnames = newPerson.Surnames?.Trim();
            if (!string.IsNullOrEmpty(surnames) && surnames.Length > cons.MAX_LENGTH_SURNAMES)
                return BadRequest($"Los apellidos no pueden exceder los {cons.MAX_LENGTH_SURNAMES} caracteres");

            string dni = newPerson.Dni;
            if (!Utils.Dni.IsValid(dni)) return BadRequest(dni + " no es un DNI válido");
            bool dniExists = await _clientsService.IsDniInUseAsync(personId, dni);
            if (dniExists) return BadRequest($"Ya existe un cliente con este DNI: {dni.Trim()}.");

            string? telephone = newPerson.Telephone;
            if (!string.IsNullOrEmpty(telephone) && !Utils.Telephone.IsValid(telephone))
                return BadRequest("El formato de teléfono no es correcto");

            string? email = newPerson.Email;
            if (!string.IsNullOrEmpty(email) && !Utils.Email.IsValid(email))
                return BadRequest($"'{email}' no es una dirección de correo válida");

            string? emailLamda = email?.ToLower().Trim();
            bool emailExists = await _clientsService.IsEmailInUseAsync(personId, emailLamda);
            if (emailExists) return BadRequest($"El correo electrónico {email?.Trim()} ya se encuentra en uso");

            if (string.IsNullOrEmpty(emailLamda)) newPerson.Email = null;

            string? address = newPerson.Address;
            if (!string.IsNullOrEmpty(address) && address.Length > cons.MAX_LENGTH_ADDRESS)
                return BadRequest($"La dirección no pude contener más de {cons.MAX_LENGTH_ADDRESS}");


            _logger.LogInformation("El cliente ha sido validado correctamnente");
            return new ObjectResult("Ok")
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
