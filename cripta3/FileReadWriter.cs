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
            using (StreamWriter sw = new StreamWriter("keys.txt"))
            {
                sw.WriteLine(text);
            }
        }
    }
}
