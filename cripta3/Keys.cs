using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace cripta3
{
    public static class Keys
    {
        private const string RSA_CLOSE = "rsa-close";
        private const string RSA_OPEN = "rsa-open";
        private const string AES_KEY = "aes-key";
        private const string AES_IV = "aes-IV";

        private const string KEYS_FILE = "keys.txt";

        private const string AES_KEY_FILE = "AesKey.txt";
        private const string AES_IV_FILE = "AesIV.txt";
        private const string RSA_PUB_KEY_FILE = "RSAPublic.txt";
        private const string RSA_PRI_KEY_FILE = "RSAPrivate.txt";

        public static void Write(string rsaClose, string rsaOpen, byte[] aesKey, byte[] aesIV)
        {
            using (FileStream fileStream = new FileStream(RSA_PRI_KEY_FILE, FileMode.OpenOrCreate))
            {
                var bytes = Encoding.UTF8.GetBytes(rsaClose);
                fileStream.Write(bytes, 0, bytes.Length);
            }
            using (FileStream fileStream = new FileStream(RSA_PUB_KEY_FILE, FileMode.OpenOrCreate))
            {
                var bytes = Encoding.UTF8.GetBytes(rsaOpen);
                fileStream.Write(bytes, 0, bytes.Length);
            }
            using (FileStream fileStream = new FileStream(AES_KEY_FILE, FileMode.OpenOrCreate))
            {
                fileStream.Write(aesKey, 0, aesKey.Length);
            }
            using (FileStream fileStream = new FileStream(AES_IV_FILE, FileMode.OpenOrCreate))
            {
                fileStream.Write(aesIV, 0, aesIV.Length);
            }
            //                using (FileStream fileStream = new FileStream(KEYS_FILE, FileMode.OpenOrCreate))
            //          {
            // Write the data to the file, byte by byte.
            /*for (int i = 0; i < dataArray.Length; i++)
            {
                fileStream.WriteByte(dataArray[i]);
            }

            for (int i = 0; i < dataArray.Length; i++)
            {
                fileStream.WriteByte(dataArray[i]);
            }
            */

            // Set the stream position to the beginning of the file.
            // fileStream.Seek(0, SeekOrigin.Begin);

            // Read and verify the data.
            /*for (int i = 0; i < fileStream.Length; i++)
            {
                if (dataArray[i] != fileStream.ReadByte())
                {
                    Console.WriteLine("Error writing data.");
                    return;
                }
            }
            Console.WriteLine("The data was written to {0} " +
                "and verified.", fileStream.Name);*/
            //}
        }

        public static string[] Read()
        {
            string[] result = new string[4];
            string line = "";
            var arr = new string[4];
            arr[0] = RSA_PUB_KEY_FILE;
            arr[1] = RSA_PRI_KEY_FILE;
            arr[2] = AES_KEY_FILE;
            arr[3] = AES_IV_FILE;
            for (int i = 0; i < 4; i++)
            {
                using (StreamReader sr = new StreamReader(arr[i]))
                {
                    if ((line = sr.ReadLine()) != null)
                        result[i] = line;
                    else
                        result[i] = "";
                    line = "";
                }
            }
            return result;
        }
    }
}
