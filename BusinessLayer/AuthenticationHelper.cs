using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace BusinessLayer
{
    public class AuthenticationHelper
    {
        const int salt_bitsize = 64;
        public const byte salt_bytesize = salt_bitsize / 8;

        private HashAlgorithm sha256 = SHA256.Create();
        protected RandomNumberGenerator rand = RandomNumberGenerator.Create();


        public virtual Tuple<string, string> hash(string password)
        {
            byte[] salt = new byte[salt_bytesize];
            rand.GetBytes(salt);
            string saltstring = Convert.ToHexString(salt);
            string hash = hashSHA256(password, saltstring);
            return Tuple.Create(hash, saltstring);
        }

        public virtual bool verify(string login_password, string hashed_registered_password, string saltstring)
        {
            string hashed_login_password = hashSHA256(login_password, saltstring);
            if (hashed_registered_password.Equals(hashed_login_password)) return true;
            else return false;
        }

        private string hashSHA256(string password, string saltstring)
        {
            byte[] hashinput = Encoding.UTF8.GetBytes(saltstring + password);
            byte[] hashoutput = iteratedSha256(hashinput, 25000);
            return Convert.ToHexString(hashoutput);
        }

        private byte[] iteratedSha256(byte[] hashinput, int iterations)
        {
            byte[] hashvalue = { };
            for (int i = 0; i < iterations; i++)
            {
                hashvalue = sha256.ComputeHash(hashinput);
                hashinput = hashvalue;
            }
            return hashvalue;
        }

    }
}
