namespace AgrajaBackend.Services.Contracts
{
    /// <summary>
    /// Interface de servicio de encriptación
    /// </summary>
    public interface ICriptoService
    {
        /// <summary>
        /// Encripta cadena de texto
        /// </summary>
        /// <param name="textToEncript">texto a encriptar</param>
        /// <param name="key">clave o contraseña</param>
        /// <returns></returns>
        public string Encript(string textToEncript, string key);

        /// <summary>
        /// Desencripta una cadena de texto
        /// </summary>
        /// <param name="textToDecript">texto a desencriptar</param>
        /// <param name="key">clave o contraseña</param>
        /// <returns></returns>
        public string Decript(string textToDecript, string key);
    }
}
