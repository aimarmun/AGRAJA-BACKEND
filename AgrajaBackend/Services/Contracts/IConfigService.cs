namespace AgrajaBackend.Services.Contracts
{

    /// <summary>
    /// Interface de configService
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// String de conexión a base de datos
        /// </summary>
        string ConnectionString { get; }
    }
}