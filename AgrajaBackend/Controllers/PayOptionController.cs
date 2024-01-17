using AgrajaBackend.DTOs.PayOption;
using AgrajaBackend.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AgrajaBackend.Controllers
{
    /// <summary>
    /// controlador de opciones de pago
    /// </summary>
    [Route("api/[controller]")]
    public class PayOptionController : ControllerBase
    {
        private readonly ILogger<PayOptionController> _logger;
        private readonly IPayOptionsService _payOptionsService;

        /// <summary>
        /// Constructor de controlador
        /// </summary>
        /// <param name="logger">Servicio logger</param>
        /// <param name="service">Servicio de opciones de pago</param>
        public PayOptionController(ILogger<PayOptionController> logger, IPayOptionsService service)
        {
            _logger = logger;
            _payOptionsService = service;
        }

        /// <summary>
        /// Adquiere todas las formas de pago.
        /// </summary>
        /// <returns>Devuelve todas las formas de pago existentes. Si no hay formas de pago devuelve un 404</returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAll()
        {
            var payOptons = await _payOptionsService.GetAllAsync();
            if (payOptons.Length == 0) return NotFound("Todavía no existe ninguna forma de pago");

            var payOptionsDto = PayOptionDto.ParseAll(payOptons);

            return Ok(payOptionsDto);
        }

        /// <summary>
        /// Adquiere una forma de pago por su Id.
        /// Devuelve un código 404 (Not Found) si no se encuentra.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetPayOptionById([FromRoute] int id)
        {
            _logger.LogDebug("Adquiriendo forma de pago con id {payOptionId}", id);

            var payOption = await _payOptionsService.GetByIdAsync(id);
            if (payOption == null) return NotFound($"No se encuntra la forma e pago con Id {id}");

            var payOptionResult = PayOptionDto.Parse(payOption);

            return Ok(payOptionResult);

        }
    }
}
