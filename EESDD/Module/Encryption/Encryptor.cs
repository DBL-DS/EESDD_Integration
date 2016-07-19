using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EESDD.Module.Encryption
{
    public enum StringCase
    {
        UPPERCASE,
        LOWERCASE
    }
    public static class Encryptor
    {
        public static string GetMD5 (string plaintext)
        {
            return GetMD5(plaintext, StringCase.LOWERCASE);
        }

        public static string GetMD5(string plaintext, StringCase CASE)
        {
            byte[] plainbytes = Encoding.GetEncoding("UTF-8").GetBytes(plaintext);
            var cryptor = new MD5CryptoServiceProvider();
            byte[] cipherbytes = cryptor.ComputeHash(plainbytes);

            var strBuilder = new StringBuilder();
            for (int i = 0; i < cipherbytes.Length; i++)
            {
                if (CASE == StringCase.UPPERCASE)
                    strBuilder.Append(cipherbytes[i].ToString("X2"));
                else
                    strBuilder.Append(cipherbytes[i].ToString("x2"));
            }
            string ciphertext = strBuilder.ToString();

            return ciphertext;
        }
    }
}
