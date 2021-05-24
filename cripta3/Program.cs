using System;

namespace cripta3
{
    class Program
    {
        static void Main(string[] args)
        {
            var aes = new AES();
            aes.GenerateKeys();
            aes.Example();

            Console.WriteLine();

            var rsa = new RSA();
            rsa.GenerateKeys();
            rsa.Exapmple();

        }
    }
}
