using System;
using System.ServiceModel;

namespace Chat.Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Kernel.ServiceChat))) 
            {
                host.Open();
                Console.WriteLine("Хост активен!");
                Console.ReadLine();
            }
        }
    }
}
