using ContentHub.Client;
using System;
using System.Threading.Tasks;

namespace ContentHub
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await ContentHubConnector.Client().TestConnectionAsync();
                Console.WriteLine("Connection is successful");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to connect " + ex);
            }
            Console.ReadLine();
        }
    }
}
