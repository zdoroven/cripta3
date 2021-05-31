using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace cripta3
{
    public class RSA
    {
        public RSAParameters Key { get; set; }
        public RSACryptoServiceProvider RSAProvider = null;

        public void GenerateKey()
        {
            RSAProvider = new RSACryptoServiceProvider();

            Key = RSAProvider.ExportParameters(true);
        }

        public void setKeys(RSAParameters keys)
        {
            RSAProvider = new RSACryptoServiceProvider();
            RSAProvider.ImportParameters(keys);
            Key = RSAProvider.ExportParameters(true);
        }

        public static byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.ImportParameters(RSAKeyInfo);

            return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.ImportParameters(RSAKeyInfo);

            return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
        }

        public void Example()
        {
            /*UnicodeEncoding byteConverter = new UnicodeEncoding();
            string toEncrypt = "Hello, world";
            
            // зашифровка текстa
            byte[] encBytes = RSAEncrypt(byteConverter.GetBytes(toEncrypt), PublicKey, false);

            string encrypt = byteConverter.GetString(encBytes);
            Console.WriteLine("Encrypt str: " + encrypt);
            Console.WriteLine("Encrypt bytes: " + string.Join(", ", encBytes));

            // расшифровка текста
            byte[] decBytes = RSADecrypt(encBytes, PrivateKey, false);

            Console.WriteLine("Decrypt str: " + byteConverter.GetString(decBytes));
            Console.WriteLine("Decrypt bytes: " + string.Join(", ", byteConverter.GetBytes(encrypt)));*/
        }

    }
}
