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

        public static void Write(RSAParameters rsaClose, RSAParameters rsaOpen, byte[] aesKey, byte[] aesIV)
        {
            using (FileStream fileStream = new FileStream(KEYS_FILE, FileMode.OpenOrCreate))
            {
                // Write the data to the file, byte by byte.
                for (int i = 0; i < dataArray.Length; i++)
                {
                    fileStream.WriteByte(dataArray[i]);
                }

                for (int i = 0; i < dataArray.Length; i++)
                {
                    fileStream.WriteByte(dataArray[i]);
                }


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
            }
        }

        public static string[] Read()
        {
            string[] result = new string[4];
            string line = "";
            int i = 0;

            using (StreamReader sr = new StreamReader(KEYS_FILE))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    result[i] = line.Split(':')[1];
                    i++;
                }
            }
            return result;
        }
    }
}
