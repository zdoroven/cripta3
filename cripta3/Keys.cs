using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace cripta3
{
    public static class Keys
    {
        private const string AES_KEY_FILE = "..\\..\\..\\AesKey.txt";
        private const string AES_IV_FILE = "..\\..\\..\\AesIV.txt";
        private const string RSA_KEY_FILE = "..\\..\\..\\RSAKey.txt";

        public static void WriteKeys(RSACryptoServiceProvider rsa, byte[] aesKey, byte[] aesIV)
        {
            FileReadWriter.WriteString(rsa.ToXmlString(true), RSA_KEY_FILE);

            FileReadWriter.WriteBytes(aesKey, AES_KEY_FILE);

            FileReadWriter.WriteBytes(aesIV, AES_IV_FILE);
        }

        public static Tuple<RSAParameters, List<byte[]>> ReadKeys()
        {
            var keys = new List<byte[]>();

            /* Читаем RSA ключи */
            var RSAprovider = new RSACryptoServiceProvider();
            RSAprovider.FromXmlString(FileReadWriter.ReadString(RSA_KEY_FILE));

            var rsaParameters = RSAprovider.ExportParameters(true);

            /* Читаем aes ключи */
            var filenames = new string[] { AES_KEY_FILE, AES_IV_FILE };

            foreach (var filename in filenames)
            {
                var bytes = FileReadWriter.ReadBytes(filename);

                /* Если ничего не прочитали, то возвращаем пустой массив */
                keys.Add(bytes ?? new byte[0]);  
            }

            return Tuple.Create(rsaParameters, keys);
        }
    }
}
