using AgrajaBackend.DTOs.JwtToken;
using AgrajaBackend.DTOs.User;
using AgrajaBackend.Models;
using AgrajaBackend.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using cons = AgrajaBackend.Constants.Config.Users;
using constJwt = AgrajaBackend.Constants.Config.JWT;

namespace AgrajaBackend.Services
{
    /// <summary>
    /// Servicio de usuarios
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Devuelve true si es la configuración por defecto y require de una actualízación de las contraseñas
        /// </summary>
        public bool ConfigPendig { get; set; }

        private bool _initialized = false;

        private ICriptoService _criptoService;
        private IConfiguration _config;
        private AppDbContext _context;
        private ILogger<UserService> _logger;

        /// <summary>
        /// Constructor de dervicio de usuarios
        /// </summary>
        /// <param name="cpriptoService"></param>
        /// <param name="context"></param>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public UserService(
            ICriptoService cpriptoService, 
            AppDbContext context, 
            IConfiguration config, 
            ILogger<UserService> logger
        )
        {
            _criptoService = cpriptoService;
            _context = context;
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Inicializa los usuarios por defecto con sus contraseñas por defecto.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeDefaultUsers()
        {
            if (!_initialized && _context.Users.Count() > 0 && !bool.Parse(_config[cons.KEY_RESET] ?? false.ToString())) return;

            _config[cons.KEY_RESET] = false.ToString();
            
            _initialized = true;
            ConfigPendig = true;

            _logger.LogInformation("Se crearán los usuarios por defecto");
            
            var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower().Equals("administrador"));
            var sellerUser = await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower().Equals("vendedor"));

            if (adminUser != null) _context.Users.Remove(adminUser);
            if (sellerUser != null) _context.Users.Remove(sellerUser);

            adminUser = new User()
                {
                    Name = "Administrador",
                    IsActive = true,
                    Rol = "Admin",
                    Password = _criptoService.Encript("admin", "admin")
                };

            sellerUser = new User()
                {
                    Name = "Vendedor",
                    IsActive = true,
                    Rol = "Seller",
                    Password = _criptoService.Encript("seller", "seller")
                };

            _context.Users.Add(adminUser);
            _context.Users.Add(sellerUser);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Añade un nuevo usuario (no implementado)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adquiere todos los usuarios (no implementado)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<User[]> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Actualizar un usuario (no implementado)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<JwtTokenDto?> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adquiere de manera asíncrona el token de un usuario válido
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public async Task<JwtTokenDto?> GetUserTokenAsync(UserLoginRequestDto userLogin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                    u.Name.ToLower().Equals(userLogin.Name.ToLower().Trim())
                );

            if (user == null) return null;

            string decripPass = "";
            
            try
            {
                decripPass = _criptoService.Decript(user.Password, userLogin.Password);
            }
            catch
            {
                _logger.LogWarning("No se puede desencriptar la contraseña de {name}", userLogin.Name);
            }

            if (!decripPass.Equals(userLogin.Password)) return null;

            JwtSecurityToken token = GenerateJwtToken(user);

            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new JwtTokenDto
            {
                Token =  new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }

        private JwtSecurityToken GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[constJwt.KEY_KEY] ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var token = new JwtSecurityToken(
                    _config[constJwt.KEY_ISSUER] ?? "",
                    _config[constJwt.KEY_AUDIENCE] ?? "",
                    claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: credentials
                );
            return token;
        }
        
        private JwtSecurityToken GenerateJwtToken(Claim[] claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[constJwt.KEY_KEY] ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                    _config[constJwt.KEY_ISSUER] ?? "",
                    _config[constJwt.KEY_AUDIENCE] ?? "",
                    claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: credentials
                );
            return token;
        }

        /// <summary>
        /// Actualiza la contraseña de un usuario
        /// </summary>
        /// <param name="userNewPassword"></param>
        /// <returns></returns>
        public async Task<JwtTokenDto?> UpdateUserPasswordAsync(UserNewPasswordRequestDto userNewPassword)
        {
            var user = _context.Users.FirstOrDefault(u =>
                    u.Name.ToLower().Equals(userNewPassword.Name.ToLower().Trim())
                );

            if (user == null) return null;

            var decripPass = _criptoService.Decript(user.Password, userNewPassword.Password);

            if (!decripPass.Equals(userNewPassword.Password)) return null;

            user.Password = _criptoService.Encript(userNewPassword.NewPassword, userNewPassword.NewPassword);

            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            
            user.RefreshToken = refreshToken;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();


            return new JwtTokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshToken = refreshToken
            };
        }

        /// <summary>
        /// Refresca los tokens. Utilizar cuando caducan para refrescar los tokens y no pedir credenciales nuevamente.
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        public async Task<JwtTokenDto?> RefreshTokenAsync(JwtTokenDto jwtToken)
        {
            //Seguridad, evitar que si se envian tokens vacios se puedan refrescar
            if (string.IsNullOrEmpty(jwtToken.Token.Trim())) return null;
            if (string.IsNullOrEmpty(jwtToken.RefreshToken.Trim())) return null;

            var principal = GetPrincipalFromExpiredToken(jwtToken.Token);
            var username = principal?.Claims?.Where(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))?.FirstOrDefault()?.Value;

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Name.Equals(username));
            if (user == null || user.RefreshToken != jwtToken.RefreshToken || principal?.Claims == null) return null;

            var newJwtToken = GenerateJwtToken(principal.Claims.ToArray());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new JwtTokenDto { 
                Token = new JwtSecurityTokenHandler().WriteToken(newJwtToken), 
                RefreshToken = newRefreshToken 
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[constJwt.KEY_KEY] ?? "")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        /// <summary>
        /// Revoca los tokens de un usuario
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> RevokeAsync(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Name.Equals(username));
            if (user == null) return false;

            user.RefreshToken = null;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
