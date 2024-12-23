using System.Net;
using System.Net.Sockets;

using TcpLib;

namespace SendNumsClient
{
    internal class Program
    {
        private TcpClient server = new();
        private static void Main(string[] args) => new Program().Start();


        private void Start()
        {
            Run().Wait();
        }

        private async Task Run()
        {
            Console.WriteLine("Enter Ip:");
            if (!IPAddress.TryParse(Console.ReadLine(), out var address))
            {
                Console.WriteLine("Невалидный IP, отключение сервера");
                return;
            }
            await server.ConnectAsync(address, 2024);
            Console.WriteLine("Соединение с сервером установлено");
            string receivedStr = await server.ReceiveString();

            string[] numberStrings = receivedStr.Split(',');
            int[] convertedNumbers = Array.ConvertAll(numberStrings, int.Parse);

            Console.WriteLine("Получены значения с сервера:");
            foreach (int number in convertedNumbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
