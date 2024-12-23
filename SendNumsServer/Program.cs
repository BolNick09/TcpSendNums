using System.Net.Sockets;
using System.Net;

using TcpLib;
using System.Text;

namespace SendNumsServer
{
    internal class Program
    {
        private static void Main(string[] args) => new Program().Run();


        private void Run()
        {
            Listen().Wait();
        }


        private IList<TcpClient> clients = [];
        private async Task Listen()
        {
            Console.WriteLine("Enter Ip:");
            if (!IPAddress.TryParse (Console.ReadLine(), out var address))
            {
                Console.WriteLine("Невалидный IP, отключение сервера");
                return;
            }
            TcpListener listen = new TcpListener(address, 2024);
            Console.WriteLine("Сервер запущен");
            listen.Start();
            while (true)
            {
                TcpClient client = await listen.AcceptTcpClientAsync();
                lock (clients)
                    clients.Add(client);

                ListenToClient(client);
                client.Close();

            }
        }
        private async void ListenToClient(TcpClient client)
        {
            Random random = new Random();
            int[] nums = new int[10];
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = random.Next(0, 101); 
            }
            string result = string.Join(",", nums);
            Console.WriteLine(result);
            await client.SendString(result);

        }
    }
}
