using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace cripta3
{
    class Program
    {
        private const string DOC_FILE = "..\\..\\..\\doc.txt";
        private const string ENC_DOC_FILE = "..\\..\\..\\encryptedDoc.txt";
        private const string DEC_DOC_FILE = "..\\..\\..\\decryptedDoc.txt";

        static void Main(string[] args)
        {
            /* Читаем текст для последующего шифрования */
            var text = FileReadWriter.ReadString(DOC_FILE);
            /* Используем AES для шифрования текста */
            // Пееменные для ключей
            var aesKey = Array.Empty<Byte>();
            var aesIV = Array.Empty<Byte>();
            RSAParameters rsaOpen;
            RSAParameters rsaClose;

            var aes = new AES();
            var rsa = new RSA();

            // Читаем старые ключи, если есть
            var keys = Keys.Read();
            if(keys[0] == "")
            {
                // Нет сохраненных ключей, надо генерить новые
                aes.GenerateKeys();
                aesKey = aes.AesKey;
                aesIV = aes.AesIV;
                rsa.GenerateKeys();
                rsaOpen = rsa.PublicKey;
                rsaClose = rsa.PrivateKey;
            }
            else
            {
                
                aesKey = Encoding.ASCII.GetBytes(keys[2]);
                aesIV = Encoding.ASCII.GetBytes(keys[3]);
            }
            
            
            var encryptedBytes = aes.EncryptStringToBytes_Aes(text, aesKey, aesIV);
            Console.WriteLine("Изначальный текст: " + text + "\n");
            /* Пишем в файл зашифрованный текст */
            //FileReadWriter.WriteString(ByteArrayToString(encryptedBytes), ENC_DOC_FILE);
            Console.WriteLine("Зашифрованный текст: " + ByteArrayToString(encryptedBytes) + "\n");
;            /* Шифруем саенсовые ключи AES с помощью RSA */

            var encAesKey = RSA.RSAEncrypt(aesKey, rsa.PublicKey, false);
            var encAesKeyIV = RSA.RSAEncrypt(aesIV, rsa.PublicKey, false);

            /* Записываем шифрованные сеансовые ключи AES и ключи RSA в файл */
            //Keys.Write(rsa.PrivateKey);

            // Расшифровываем ключи симметричного алгоритма
            var AesKey = RSA.RSADecrypt(encAesKey, rsa.PrivateKey, false);
            var AesIV = RSA.RSADecrypt(encAesKeyIV, rsa.PrivateKey, false);
            // Расшифровываем текст
            var decryptedText = aes.DecryptStringFromBytes_Aes(encryptedBytes, AesKey, AesIV);
            //FileReadWriter.WriteString(decryptedText, DEC_DOC_FILE);
            Console.Write("Расшифрованный текст: " + decryptedText + "\n");
        }

        private static string ByteArrayToString(byte[] bytes)
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in bytes)
                stringBuilder.Append(item.ToString());

            return stringBuilder.ToString();
        }
    }
}
