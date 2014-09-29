using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using UdpRx;

namespace UdpClient
{
    class Program
    {
        static void Main(string[] args)
        {

            var device = new DeviceClient(new System.Net.Sockets.UdpClient());

            while (true)
            {
                Console.Write("Enter Port");
                var port = int.Parse(Console.ReadLine());
                Console.Write("Enter Message");
                var msg = Console.ReadLine();
                var bytes = Encoding.UTF8.GetBytes(msg);

                device.SendReceiveUdpAsync(bytes, "127.0.0.1", port, 1000).Subscribe(result =>
                    Console.WriteLine(Encoding.UTF8.GetString(result)));

            }
        }
    }
}
