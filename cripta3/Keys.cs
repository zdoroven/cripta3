using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cripta3
{
    class Keys
    {
        private const string RSA_CLOSE = "rsa-close";
        private const string RSA_OPEN = "rsa-open";
        private const string AES_KEY = "aes-key";
        private const string AES_IV = "aes-IV";

        private const string KEYS_FILE = "keys.txt";

        public void write(string rsaClose, string rsaOpen, string aesKey, string aesIV)
        {
            using (StreamWriter sw = new StreamWriter("keys.txt"))
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(String.Format("{0}: ", RSA_CLOSE));
                stringBuilder.Append(rsaClose);
                stringBuilder.Append(String.Format("\n{0}: ", RSA_OPEN));
                stringBuilder.Append(rsaOpen);
                stringBuilder.Append(String.Format("\n{0}: ", AES_KEY));
                stringBuilder.Append(aesKey);
                stringBuilder.Append(String.Format("\n{0}: ", AES_IV));
                stringBuilder.Append(aesIV);

                sw.WriteLine(stringBuilder.ToString());   
            }
        }

        public string[] read()
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
