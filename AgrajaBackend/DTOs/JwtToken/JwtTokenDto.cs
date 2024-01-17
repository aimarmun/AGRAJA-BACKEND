namespace AgrajaBackend.DTOs.JwtToken
{
    /// <summary>
    /// Token Jwt
    /// </summary>
    public class JwtTokenDto
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Token de refresco
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
    }
}
