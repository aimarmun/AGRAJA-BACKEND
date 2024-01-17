

using AgrajaBackend.DTOs.CropType;
using AgrajaBackend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgrajaBackend.Controllers
{
    /// <summary>
    /// Controlador de tipo de cultivo
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class CropTypeController : ControllerBase
    {
        private readonly ICropTypesService _cropTypesService;
        private readonly ILogger<CropTypeController> _logger;


        /// <summary>
        /// Constructor de controlador de tipo de cultivo
        /// </summary>
        /// <param name="logger">servicio de log</param>
        /// <param name="cropTypesService">servicio de tipo de cultivo</param>
        public CropTypeController(ILogger<CropTypeController> logger, ICropTypesService cropTypesService)
        {
            _logger = logger;
            _cropTypesService = cropTypesService;
        }

        /// <summary>
        /// Adquiere todos los tipos de cultivo
        /// </summary>
        /// <returns>Retorna los tipos de cultivo o un NotFound</returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAll()
        {
            _logger.LogDebug("Adquiriendo lista de tipos de cultivo");
            var cropTypes = await _cropTypesService.GetAllAsync();
            if (cropTypes.Length == 0) return NotFound("Todavía no existe ningún tipo de cultivo");

            var cropTypesDtos = CropTypeDto.ParseAll(cropTypes);

            return Ok(cropTypesDtos);
        }
    }
}
