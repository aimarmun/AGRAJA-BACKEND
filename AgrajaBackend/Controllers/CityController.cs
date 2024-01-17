using AgrajaBackend.DTOs.City;
using AgrajaBackend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgrajaBackend.Controllers
{

    /// <summary>
    /// Controlador de ciudad
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        readonly ICitiesService _citiesService;


        /// <summary>
        /// Constructor de controlador de ciudades
        /// </summary>
        /// <param name="service">servicio de ciuades</param>
        public CityController(ICitiesService service) 
        {
            _citiesService = service;
        }

        /// <summary>
        /// Adquiere todas las ciudades donde se encuentran agricultores
        /// </summary>
        /// <returns>Todas las ciudades o NotFound si todavía no se encuentran</returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAll()
        {
            var cities = await _citiesService.GetAllAsync(); //_context.Cities.ToArrayAsync();
            if (cities.Length == 0) return NotFound("Todavía no existe ninguna ciudad");

            var citiesDtos = CityDto.ParseAll(cities);

            return Ok(citiesDtos);
        }
    }
}
