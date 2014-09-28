using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace UdpRx
{
    public static class UdpClientRxExtensions
    {
        public static IObservable<UdpReceiveResult> ReceiveObservable(this UdpClient client)
        {
            return client.ReceiveAsync().ToObservable();
        }

        public static IObservable<int> SendObservable(this UdpClient client, byte[] msg, int bytes, string ip, int port)
        {
            return client.SendAsync(msg, bytes, ip, port).ToObservable();
        }

        public static IObservable<int> SendObservable(this UdpClient client, byte[] msg, int bytes, IPEndPoint endPoint)
        {
            return client.SendAsync(msg, bytes, endPoint).ToObservable();
        }

        public static IObservable<UdpReceiveResult> ReceiveStream(this UdpClient client)
        {
            return Observable.Defer(() => client.ReceiveObservable()).Repeat();
        }
    }
}
