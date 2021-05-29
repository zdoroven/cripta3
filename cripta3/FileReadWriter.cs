using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cripta3
{
    public static class FileReadWriter
    {
        /* 
         * Метод для чтения из указаного файла (filename)
         */
        public static string ReadString(string filename)
        {
            var builder = new StringBuilder();

            using (StreamReader sr = new StreamReader(filename))
            {
                var line = "";
                while ((line = sr.ReadLine()) != null)
                    builder.Append(line);
            }

            return builder.ToString();
        }

        /* 
         * Метод записи текста (text) в указанный файл (filename) 
         */
        public static void WriteString(string text, string filename)
        {
/*            if (File.Exists(filename))
            {
                File.Delete(filename);
                File.Create(filename);
            }*/

            using StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine(text);
        }

        public static void WriteBytes(byte[] bytes, string filename)
        {
            using FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate);
            fileStream.Write(bytes, 0, bytes.Length);
        }

        public static byte[] ReadBytes(string filename)
        {
            byte[] bytes = null;

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                // Read the source file into a byte array.
                bytes = new byte[fs.Length];
                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;

                while (numBytesToRead > 0)
                {
                    // Read may return anything from 0 to numBytesToRead.
                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);

                    // Break when the end of the file is reached.
                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
            }

            return bytes;
        }
    }
}
