using AgrajaBackend.Services.Contracts;

namespace AgrajaBackend.Services
{
    /// <summary>
    /// Servicio de configuración
    /// </summary>
    public class ConfigService : IConfigService
    {
        /// <summary>
        /// Cadena de conexión a base de datos
        /// </summary>
        public string  ConnectionString { get; private set; }

        /// <summary>
        /// Constructor del servicio.
        /// </summary>
        public ConfigService()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            ConnectionString = configuration.GetConnectionString(Constants.Config.KEY_CONNECTION_STRING) ?? "";
        }


    }
}
