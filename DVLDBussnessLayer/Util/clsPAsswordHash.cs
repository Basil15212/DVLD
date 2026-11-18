using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DVLDBussnessLayer.Util
{
    public static class clsPAsswordHash
    {
        public static string PassWordHash(string PassWord)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashByts = sha.ComputeHash(Encoding.UTF8.GetBytes(PassWord));
                return BitConverter.ToString(hashByts).Replace("-", "").ToLower();
            }
        }
    }
}
