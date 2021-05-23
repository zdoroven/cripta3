using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace cripta3
{
    class RSA
    {
        RSAParameters privateKey;
        RSAParameters publicKey;


        public void generateKeys()
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            privateKey = RSA.ExportParameters(true);
            publicKey = RSA.ExportParameters(false);
        }

        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.ImportParameters(RSAKeyInfo);
            return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
        }

        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.ImportParameters(RSAKeyInfo);
            return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
        }

        public void exapmple()
        {
            // генерация ключей rsa
            generateKeys(); 

            UnicodeEncoding byteConverter = new UnicodeEncoding();
            string toEncrypt = "Hello, world";
            // зашифровка тексте
            byte[] encBytes = RSAEncrypt(byteConverter.GetBytes(toEncrypt), publicKey, false);

            string encrypt = byteConverter.GetString(encBytes);
            Console.WriteLine("Encrypt str: " + encrypt);
            Console.WriteLine("Encrypt bytes: " + string.Join(", ", encBytes));

            // расшифровка текста
            byte[] decBytes = RSADecrypt(encBytes, privateKey, false);

            Console.WriteLine("Decrypt str: " + byteConverter.GetString(decBytes));
            Console.WriteLine("Decrypt bytes: " + string.Join(", ", byteConverter.GetBytes(encrypt)));

            Console.ReadKey();
        }

    }
}
