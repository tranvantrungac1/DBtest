using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DBtest
{
    internal class Utils
    {
        public Utils()
        {

        }
        public static string Hash(string text)
        {
            MD5 md5 = MD5.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = md5.ComputeHash(bytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
