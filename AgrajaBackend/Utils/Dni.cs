using System.Text.RegularExpressions;

namespace AgrajaBackend.Utils
{
    /// <summary>
    /// Utilidades para DNI
    /// </summary>
    public class Dni
    {
        /// <summary>
        /// Comprueba si un DNI indicado tiene el formato correcto
        /// </summary>
        /// <param name="DNI">DNI que se quiere comprobar</param>
        /// <returns>True si el DNI es correcto</returns>
        public static bool IsValid(string? DNI)
        {
            
            if (string.IsNullOrEmpty(DNI)) return false;

            string dni = DNI.ToUpper().Trim();

            Regex regex = new Regex(@"^\d{8}[a-zA-Z]$");
            if (!regex.IsMatch(dni))
            {
                return false;
            }

            string leters = "TRWAGMYFPDXBNJZSQVHLCKE";
            int dniNumber;

            try
            {
                dniNumber = int.Parse(dni.Substring(0, 8));
            }
            catch
            {
                return false;
            }

            int rest = dniNumber % 23;
            char letra = leters[rest];

            return dni[8] == letra;
            
        }
    }
}
