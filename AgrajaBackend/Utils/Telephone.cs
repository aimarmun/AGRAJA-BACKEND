using System.Text.RegularExpressions;

namespace AgrajaBackend.Utils
{
    /// <summary>
    /// Utilidades para teléfonos
    /// </summary>
    public class Telephone
    {
        //todo: buscar un regex mejor
        /// <summary>
        /// Comprueba si un número de teléfono es correcto
        /// </summary>
        /// <param name="phoneNumber">Número de teléfono para comprobar</param>
        /// <returns>True si el número tiene el formato correcto</returns>
        public static bool IsValid(string phoneNumber)
        {
            if(string.IsNullOrEmpty(phoneNumber)) return false;

            Regex regex = new Regex(@"^\d{9}$|^\d{3}[-\s]?\d{2}[-\s]?\d{2}[-\s]?\d{2}$");
            return regex.IsMatch(phoneNumber);
            
        }
    }
}
