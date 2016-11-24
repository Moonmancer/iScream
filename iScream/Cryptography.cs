using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    static class Cryptography
    {
        //used for creation: http://www.unit-conversion.info/texttools/random-string-generator/
        private static string Key = "x4j3gnnhos6hzxeq4kvsczp67i90q6c9"; //32 chars -> 256 bit
        private static string IV = "gikz1lwg0yiz70tm"; //16 chars -> 128 bit

        public static string Decrypt(string encryptedText)
        {
            if (String.IsNullOrEmpty(encryptedText))
                return null;

            byte[] encryptedbytes = Convert.FromBase64String(encryptedText);
            AesCryptoServiceProvider acsp = new AesCryptoServiceProvider();

            acsp.BlockSize = 128;
            acsp.KeySize = 256;
            acsp.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            acsp.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            acsp.Padding = PaddingMode.PKCS7;
            acsp.Mode = CipherMode.CBC;

            ICryptoTransform ict = acsp.CreateDecryptor(acsp.Key, acsp.IV);

            byte[] encryptedBytes = ict.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);

            ict.Dispose();

            return ASCIIEncoding.ASCII.GetString(encryptedBytes);
        }

        public static string Encrypt(string decryptedText)
        {
            if (String.IsNullOrEmpty(decryptedText))
                return null;

            byte[] textbytes = ASCIIEncoding.ASCII.GetBytes(decryptedText);
            AesCryptoServiceProvider acsp = new AesCryptoServiceProvider();

            acsp.BlockSize = 128;
            acsp.KeySize = 256;
            acsp.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            acsp.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            acsp.Padding = PaddingMode.PKCS7;
            acsp.Mode = CipherMode.CBC;

            ICryptoTransform ict = acsp.CreateEncryptor(acsp.Key, acsp.IV);

            byte[] encryptedBytes = ict.TransformFinalBlock(textbytes, 0, textbytes.Length);

            ict.Dispose();

            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
