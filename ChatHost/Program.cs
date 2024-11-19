using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;


namespace ChatHost
{
    internal class Program
    {
        static void Main(string[] args)
        {   // чтобы все ресурсы автоматически освобождались
            using (var host = new ServiceHost(typeof(wcfChat.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("--- Host Starting! ---");
                Console.ReadLine();              // чтобы не закрылся
            }


        }
    }
}
