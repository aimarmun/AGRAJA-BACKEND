using AgrajaBackend.DTOs.JwtToken;
using AgrajaBackend.DTOs.User;
using AgrajaBackend.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace AgrajaBackend.Controllers
{
    /// <summary>
    /// Controlador para login de usuario
    /// </summary>
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor del controlador
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userService"></param>
        public LoginController(
            ILogger<LoginController> logger,
            IUserService userService
        )
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Adquiere el usuario actual
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug("Adquiriendo usuario actual");
            await _userService.InitializeDefaultUsers();
            return Ok(GetCurrentUser());
        }

        
        /// <summary>
        /// Endpoint para login de usuario
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserLoginRequestDto userLogin)
        {
            _logger.LogDebug("Login de usuario: {name}", userLogin.Name);
            await _userService.InitializeDefaultUsers();
            var token = await _userService.GetUserTokenAsync(userLogin);
            if (token == null)
            {
                return NotFound("Usuario o contraseña incorrectos");
            }
            
            return Ok(token);
        }

        /// <summary>
        /// Actualiza la contraseña de un usuario
        /// </summary>
        /// <param name="newPasswordRequestDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserNewPasswordRequestDto newPasswordRequestDto)
        {
            _logger.LogDebug("Cambio contraseña usuario: {name}", newPasswordRequestDto.Name);
            Regex reg = new("(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\\d$@$!%*?&].{8,}");
            if(!reg.IsMatch(newPasswordRequestDto.NewPassword))
            {
                return BadRequest("La contraseña no cumple los requerimientos mínimos exigios: " +
                    "Al menos 8 caracteres, al menos una maýuscula y una mínuscula, números y símbolos");
            }
            var token = await _userService.UpdateUserPasswordAsync(newPasswordRequestDto);
            if(token == null)
            {
                return BadRequest("Datos incorrectos");
            }
            
            return Ok(token);
        }

        /// <summary>
        /// Actualiza los tokens para no tener que volver a iniciar sesión
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] JwtTokenDto token)
        {
            _logger.LogDebug("Actualizando tokens");
            var newToken = await _userService.RefreshTokenAsync(token);

            if (newToken == null) return BadRequest("Error al refrescar los tokens");

            return Ok(newToken);
        }

        /// <summary>
        /// Revoca el token de un usuario
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("Revoke")]
        public async Task<IActionResult> Revoke()
        {
            var user = GetCurrentUser();
            _logger.LogInformation("Revocando token de {usuario}", user?.Name);

            var revokeResult = await _userService.RevokeAsync(user?.Name ?? "");

            if (!revokeResult) return BadRequest("No se revocó ningún token");

            return Ok();
        }


        private UserDto? GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userClaims = identity?.Claims;

            return new UserDto()
            {
                Name = userClaims?.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value ?? "",
                Rol = userClaims?.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value ?? ""
            };
        }
    }
}
