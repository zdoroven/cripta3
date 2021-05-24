using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cripta3
{
    class Program
    {
        private const string DOC_FILE = "doc.txt";
        private const string ENC_DOC_FILE = "encryptedDoc.txt";
        private const string DEC_DOC_FILE = "decryptedDoc.txt";

        static void Main(string[] args)
        {
            /* Читаем текст для последующего шифрования */
            var text = FileReadWriter.ReadString(DOC_FILE);

            /* Используем AES для шифрования текста */
            var aes = new AES();

            aes.GenerateKeys();

            var encryptedBytes = aes.EncryptStringToBytes_Aes(text, aes.AesKey, aes.AesIV);

            /* Пишем в файл зашифрованный текст */
            FileReadWriter.WriteString(ByteArrayToString(encryptedBytes), ENC_DOC_FILE);

            /* Шифруем саенсовые ключи AES с помощью RSA */
            var rsa = new RSA();
            rsa.GenerateKeys();

            var encAesKey = RSA.RSAEncrypt(aes.AesKey, rsa.PublicKey, false);
            var encAesKeyIV = RSA.RSAEncrypt(aes.AesIV, rsa.PublicKey, false);

            /* Записываем шифрованные сеансовые ключи AES и ключи RSA в файл */
            Keys.Write(rsa.PrivateKey)
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
