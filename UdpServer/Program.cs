using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UdpRx;

namespace UdpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = new Random().Next(8000, 9999);
            var device = new DeviceClient(new UdpClient(port));

            using (device.Listen(r => r.Buffer))
            {
                Console.WriteLine("Echoing on port {0}", port);
                Console.ReadLine();
            }
        }
    }
}
