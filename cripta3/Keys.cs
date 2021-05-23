using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cripta3
{
    class Keys
    {
        public void write(string rsaClose, string rsaOpen, string aes)
        {
            using (StreamWriter sw = new StreamWriter("keys.txt"))
            {
                string toWrite = "";
                toWrite = "rsa-close:" + rsaClose + "\nrsa-open:" + rsaOpen + "\naes:" + aes;
                sw.WriteLine(toWrite);   
            }
        }

        public string[] read()
        {
            string[] result = new string[3];
            string line = "";
            int i = 0;
            using (StreamReader sr = new StreamReader("keys.txt"))
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
