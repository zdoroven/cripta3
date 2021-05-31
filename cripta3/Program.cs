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
            var i = -1;
            while(i != 0) {
                Console.WriteLine("Что хочешь?\n 1)Зашифровать - напиши 1\n " +
                    "2)Расшифровать - 2\n 3)Обновить ключи - 3\n 4)Завершить - 0\n");
                i = int.Parse(Console.ReadLine());
                if(i == 1)
                {
                    encrypt();
                } else if(i == 2)
                {
                    try
                    {
                        decrypt();
                    } catch {
                        Console.WriteLine("Не получается расшифровать текст");
                    }
                } else if(i == 3)
                {
                    refreshKeys();
                }
            }
            return;
        }

        static void encrypt()
        {
            /* Читаем текст для последующего шифрования */
            var text = FileReadWriter.ReadString(DOC_FILE);

            /* Используем AES для шифрования текста */
            // Пеpеменные для ключей
            var aesKey = Array.Empty<Byte>();
            var aesIV = Array.Empty<Byte>();

            
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
            }
            else
            {
                rsa.setKeys(keys.Item1);
                aesKey = RSA.RSADecrypt(keys.Item2[0], rsa.Key, false);
                aesIV = RSA.RSADecrypt(keys.Item2[1], rsa.Key, false);
            }
            
            /* Шифруем текст с помощью AES */
            var encryptedBytes = aes.EncryptStringToBytes_Aes(text, aesKey, aesIV);
            Console.WriteLine("Изначальный текст: " + text + "\n");

            /* Пишем в файл зашифрованный текст */
            FileReadWriter.WriteBytes(encryptedBytes, ENC_DOC_FILE);
            Console.WriteLine("Зашифрованный текст: " + BitConverter.ToString(encryptedBytes) + "\n");

            /* Шифруем сеансовые ключи AES с помощью RSA*/
            var encAesKey = RSA.RSAEncrypt(aesKey, rsa.Key, false);
            var encAesKeyIV = RSA.RSAEncrypt(aesIV, rsa.Key, false);

            /* Записываем шифрованные сеансовые ключи AES и ключи шифрования RSA  */
            Keys.WriteKeys(rsa.RSAProvider, encAesKey, encAesKeyIV);
        }

        static void decrypt()
        {

            /* Читаем текст для последующего шифрования */
            var cryptoText = FileReadWriter.ReadBytes(ENC_DOC_FILE);

            /* Используем AES для шифрования текста */
            // Пеpеменные для ключей
            var aesKey = Array.Empty<Byte>();
            var aesIV = Array.Empty<Byte>();

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
            }
            else
            {
                rsa.setKeys(keys.Item1);
                aesKey = RSA.RSADecrypt(keys.Item2[0], rsa.Key, false);
                aesIV = RSA.RSADecrypt(keys.Item2[1], rsa.Key, false);
            }

            Console.WriteLine("Зашифрованный текст: " + BitConverter.ToString(cryptoText) + "\n");


            // Расшифровываем текст
            var decryptedText = aes.DecryptStringFromBytes_Aes(cryptoText, aesKey, aesIV);
            FileReadWriter.WriteString(decryptedText, DEC_DOC_FILE);
            Console.Write("Расшифрованный текст: " + decryptedText + "\n");

            /* Шифруем сеансовые ключи AES с помощью RSA*/
            var encAesKey = RSA.RSAEncrypt(aesKey, rsa.Key, false);
            var encAesKeyIV = RSA.RSAEncrypt(aesIV, rsa.Key, false);

            /* Записываем шифрованные сеансовые ключи AES и ключи шифрования RSA  */
            Keys.WriteKeys(rsa.RSAProvider, encAesKey, encAesKeyIV);
        }

        static void refreshKeys()
        {
            var aesKey = Array.Empty<Byte>();
            var aesIV = Array.Empty<Byte>();

            var aes = new AES();
            var rsa = new RSA();

            aes.GenerateKeys();
            aesKey = aes.AesKey;
            aesIV = aes.AesIV; 
            
            rsa.GenerateKey();
            var encAesKey = RSA.RSAEncrypt(aesKey, rsa.Key, false);
            var encAesKeyIV = RSA.RSAEncrypt(aesIV, rsa.Key, false);

            /* Записываем шифрованные сеансовые ключи AES и ключи шифрования RSA  */
            Keys.WriteKeys(rsa.RSAProvider, encAesKey, encAesKeyIV);
            Console.WriteLine("Ключи обновлены");
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
