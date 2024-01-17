using AgrajaBackend.Services.Contracts;
using System.Security.Cryptography;
using System.Text;
using cons = AgrajaBackend.Constants.Config.Cripto;

namespace AgrajaBackend.Services
{
    /// <summary>
    /// Servicio para encriptar y desencriptar cadenas de texto
    /// </summary>
    public class CriptoService : ICriptoService
    {
        private string _iv { get; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="configuration"></param>
        public CriptoService(IConfiguration configuration)
        {
            _iv = configuration[cons.KEY_IV] ?? "";

            //string textoOriginal = "Hola, esto es un ejemplo de encriptación en .NET";

            //// Clave y vector de inicialización (IV) deben ser secretos y compartidos entre el proceso de encriptación y desencriptación
            //string clave = "ClaveSecreta123456";

            //string textoEncriptado = Encript(textoOriginal, clave);
            //Console.WriteLine($"Texto Encriptado: {textoEncriptado}");

            //string textoDesencriptado = Decript(textoEncriptado, clave);
            //Console.WriteLine($"Texto Desencriptado: {textoDesencriptado}");
        }

        /// <summary>
        /// Desencripta una cadena de text
        /// </summary>
        /// <param name="textToDecript">Texto a desencriptar</param>
        /// <param name="key">Key o contraseña</param>
        /// <returns></returns>
        public string Decript(string textToDecript, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetKeyDerivation(key);
                aesAlg.IV = GetKeyDerivation(_iv, 16);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(textToDecript)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Encripta una cadena de texto
        /// </summary>
        /// <param name="textToEncript">Texto a encriptar</param>
        /// <param name="key">Key o password para desencriptar</param>
        /// <returns></returns>
        public string Encript(string textToEncript, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetKeyDerivation(key);
                aesAlg.IV = GetKeyDerivation(_iv, 16);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(textToEncript);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private byte[] GetKeyDerivation(string key, int bytes = 32)
        {
            byte[] keyDerivation = new byte[0];
            byte[] salt = Encoding.UTF8.GetBytes("asdlkjpowimx45");
            int iterations = 10000;

            using(Rfc2898DeriveBytes keyDeribationFunction = new Rfc2898DeriveBytes(key, salt, iterations))
            {
                keyDerivation = keyDeribationFunction.GetBytes(bytes);
            }

            return keyDerivation;
        }
    }
}
