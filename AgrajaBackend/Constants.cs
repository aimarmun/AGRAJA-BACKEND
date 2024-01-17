using Microsoft.Identity.Client;

namespace AgrajaBackend
{
    /// <summary>
    /// Clase para manejar las constantes de la aplicación
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Constantes utilizadas en el modelo PeronData
        /// </summary>
        public static class PersonData
        {
            /// <summary>
            /// Máxima longitud del nombre
            /// </summary>
            public const int MAX_LENGTH_NAME = 64;

            /// <summary>
            /// Máxima longitud de los apellidos
            /// </summary>
            public const int MAX_LENGTH_SURNAMES = 128;

            /// <summary>
            /// Máxima longitud para las direcciones
            /// </summary>
            public const int MAX_LENGTH_ADDRESS = 256;

            /// <summary>
            /// Longitud de un DNI
            /// </summary>
            public const int MAX_LENGHT_DNI = 9;

            /// <summary>
            /// Máxima longitud para campo teléfono
            /// </summary>
            public const int MAX_LENGHT_TELEPHONE = 16;

            /// <summary>
            /// Máxima longitud para correo
            /// </summary>
            public const int MAX_LENGHT_EMAIL = 320;
        }

        /// <summary>
        /// Constantes para el modelo Crate
        /// </summary>
        public static class Crate
        {
            /// <summary>
            /// Máxima longitud para el nombre de la caja
            /// </summary>
            public const int MAX_LENGTH_NAME = 64;

            /// <summary>
            /// Máxima longitud para la descripción
            /// </summary>
            public const int MAX_LENGTH_DESCRIPTION = 256;
        }

        /// <summary>
        /// Constantes para el modelo CropType
        /// </summary>
        public static class CropType
        {
            /// <summary>
            /// Máxima longitud para el nombre
            /// </summary>
            public const int MAX_LENGTH_NAME = 64;

            /// <summary>
            /// Máxima longitud para la descripción
            /// </summary>
            public const int MAX_LENGTH_DESCRIPTION = 128;
            
            /// <summary>
            /// Sublcase CropType para constantes de filtro
            /// </summary>
            public static class Filters
            {
                /// <summary>
                /// Constante utilizada para utilizar todo tipo de cultivos
                /// </summary>
                public const int ALL_CROP_TYPES = 0;
            }
        }

        /// <summary>
        /// Constantes para el modelo PayOptions
        /// </summary>
        public static class PayOptions
        {
            /// <summary>
            /// Máxima longitud para el nombre
            /// </summary>
            public const int MAX_LENGTH_NAME = 64;

            /// <summary>
            /// Máxima longitud para la descripción
            /// </summary>
            public const int MAX_LENGTH_DESCRIPTION = 128;
        }

        /// <summary>
        /// Constantes para el modelo City
        /// </summary>
        public static class City 
        {
            /// <summary>
            /// Máxima longitud para nombre
            /// </summary>
            public const int MAX_LENGTH_NAME = 64;
        }

        /// <summary>
        /// Constantes para servicio Configuration
        /// </summary>
        public static class Config
        {
            /// <summary>
            /// Key utilizada para identificar la cadena de conexión en la BBDD en el archivo de configuración
            /// </summary>
            public const string KEY_CONNECTION_STRING = "SQLconnectionString";

            /// <summary>
            /// Configuraciones de JWT
            /// </summary>
            public static class JWT
            {
                /// <summary>
                /// Key donde se almacena la clave de JWT
                /// </summary>
                public const string KEY_KEY = "Jwt:key";

                /// <summary>
                /// Key donde se almacena el Issuer de JWT
                /// </summary>
                public const string KEY_ISSUER = "Jwt:Issuer";

                /// <summary>
                /// Key donde se almacena el Audience de JWT
                /// </summary>
                public const string KEY_AUDIENCE = "Jwt:Audience";

                /// <summary>
                /// Key para activar/desactivar la validación de audiencia
                /// </summary>
                public const string KEY_VALIDATE_AUDIENCE = "Jwt:ValidateAudience";

                /// <summary>
                /// Key para activar/desactivar la validación del Issuer
                /// </summary>
                public const string KEY_VALIDATE_ISSUER = "Jwt:ValidateIssuer";
            }

            /// <summary>
            /// Constantes de configuración de CORS
            /// </summary>
            public static class CORS
            {
                /// <summary>
                /// Key de configuración: Permitir cualquier cabecera
                /// </summary>
                public const string KEY_ALLOW_ANY_HEADER = "CORS:AllowAnyHeader";

                /// <summary>
                /// Key de configuración: Permitir cualquier método
                /// </summary>
                public const string KEY_ALLOW_ANY_METHOD = "CORS:AllowAnyMethod";

                /// <summary>
                /// Key de configuración: Permitir cualquier origen
                /// </summary>
                public const string KEY_ALLOW_ANY_ORIGIN = "CORS:AllowAnyOrigin";

                /// <summary>
                /// Key de configuración: Orígenes. Varios separados por coma
                /// </summary>
                public const string KEY_WITH_ORIGINS = "CORS:WithOrigins";
            }

            /// <summary>
            /// Constantes de configuración para el servicio de encriptación
            /// </summary>
            public static class Cripto
            {
                /// <summary>
                /// Llave o clave correspondiente el json donde se guarda el iv
                /// </summary>
                public const string KEY_IV = "Cripto:iv";
            }

            /// <summary>
            /// Constantes de configuración de usuarios
            /// </summary>
            public static class Users
            {
                /// <summary>
                /// Resetea los usuarios existentes creando Administrador y Vendedor
                /// </summary>
                public const string KEY_RESET = "Users:reset";

                /// <summary>
                /// Roles de usuario
                /// </summary>
                public class Roles
                {
                    /// <summary>
                    /// Rol administrador.
                    /// Permiso total de la aplicación.
                    /// </summary>
                    public const string ADMIN = "Admin";

                    /// <summary>
                    /// Rol vendedor
                    /// Permisos para vender y contratar agricultores.
                    /// No tiene permisos para modificar o crear cajas, agricultores y clientes
                    /// </summary>
                    public const string SELLER = "Seller";
                }
            }

            /// <summary>
            /// Constantes de configuración de base de datos
            /// </summary>
            public static class Database
            {
                /// <summary>
                /// Tipo de motor de base de datos
                /// </summary>
                public const string KEY_CORE = "DatabaseCore:core";

                /// <summary>
                /// Valor utilizado para indicar motor SQL Server
                /// </summary>
                public const string SQLSERVER_CORE = "SQLSERVER";

                /// <summary>
                /// Valor utilizado para indicar motor MySQL
                /// </summary>
                public const string MYSQL_CORE = "MYSQL";

                /// <summary>
                /// String de conexión de SQL Server
                /// </summary>
                public const string CONNECTIONSTRING_SQLSERVER = "ConnectionStrings:SQLconnectionString";

                /// <summary>
                /// String de conexión de MySQL
                /// </summary>
                public const string CONNECTIONSTRING_MYSQL = "ConnectionStrings:MYSQLconnectionString";
            }
        }
    }
}
