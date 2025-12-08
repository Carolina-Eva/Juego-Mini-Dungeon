using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    public class ServicioHash
    {
        public string CalculateHash(string datos)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(datos);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
