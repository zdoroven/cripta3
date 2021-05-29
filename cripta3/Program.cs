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
            
        }

        private void temp()
        {
            /* Читаем текст для последующего шифрования */
            var text = FileReadWriter.ReadString(DOC_FILE);

            /* Используем AES для шифрования текста */
            // Пеpеменные для ключей
            var aesKey = Array.Empty<Byte>();
            var aesIV = Array.Empty<Byte>();

            RSAParameters rsaKey;

            var aes = new AES();
            var rsa = new RSA();

            // Читаем старые ключи, если есть
            var keys = Keys.ReadKeys();

            if (keys.Item2[0].Length == 0 || keys.Item2[1].Length == 0)
            {
                // Нет сохраненных ключей, надо генерировать новые
                aes.GenerateKeys();
                aesKey = aes.AesKey;
                aesIV = aes.AesIV;

                rsa.GenerateKey();
                rsaKey = rsa.Key;
            }
            else
            {
                rsaKey = keys.Item1;
                aesKey = keys.Item2[0];
                aesIV = keys.Item2[1];
            }

            /* Шифруем текст с помощью AES */
            var encryptedBytes = aes.EncryptStringToBytes_Aes(text, aesKey, aesIV);
            Console.WriteLine("Изначальный текст: " + text + "\n");

            /* Пишем в файл зашифрованный текст */
            FileReadWriter.WriteBytes(encryptedBytes, ENC_DOC_FILE);
            /*Console.WriteLine("Зашифрованный текст: " + ByteArrayToString(encryptedBytes) + "\n");*/

            /* Шифруем саенсовые ключи AES с помощью RSA
*/          var encAesKey = RSA.RSAEncrypt(aesKey, rsa.Key, false);
            var encAesKeyIV = RSA.RSAEncrypt(aesIV, rsa.Key, false);

            /* Записываем шифрованные сеансовые ключи AES и ключи шифрования RSA  */
            Keys.WriteKeys(rsa.RSAProvider, encAesKey, encAesKeyIV);

            // Расшифровываем ключи симметричного алгоритма
            var AesKey = RSA.RSADecrypt(encAesKey, rsa.Key, false);
            var AesIV = RSA.RSADecrypt(encAesKeyIV, rsa.Key, false);

            // Расшифровываем текст
            var decryptedText = aes.DecryptStringFromBytes_Aes(encryptedBytes, AesKey, AesIV);
            FileReadWriter.WriteString(decryptedText, DEC_DOC_FILE);
            Console.Write("Расшифрованный текст: " + decryptedText + "\n");
        }
    }
}
