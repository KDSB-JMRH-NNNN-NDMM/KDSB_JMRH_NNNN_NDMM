using Microsoft.CodeAnalysis.Scripting;
using System.Security.Cryptography;
using System.Text;


namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class Encriptacion
    {
        public static string HashPassword(string password)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                // ComputeHash - devuelve un arreglo de bytes
                byte[] bytes = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir arreglo de bytes a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        


     }
}
