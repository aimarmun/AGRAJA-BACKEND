using System.ComponentModel.DataAnnotations;

namespace AgrajaBackend.Utils
{
    /// <summary>
    /// Utilidades de e-mail
    /// </summary>
    public class Email
    {
        /// <summary>
        /// Comprueba si un correo es válido
        /// </summary>
        /// <param name="email">e-mail para comprobar</param>
        /// <returns></returns>
        public static bool IsValid(string email)
        {
            var attr = new EmailAddressAttribute();

            return attr.IsValid(email?.Trim());
        }
    }
}
