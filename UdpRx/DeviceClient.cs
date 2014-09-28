using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace UdpRx
{
    public class DeviceClient
    {
        private UdpClient client;
        IObservable<UdpReceiveResult> receiveStream;

        public DeviceClient(UdpClient client)
        {
            this.client = client;
            receiveStream = client.ReceiveStream().Publish().RefCount();
        }


        public IObservable<byte[]> SendReceiveUdpAsync(byte[] msg, string ip, int port, int timeOut)
        {
            var o = from _ in this.client.SendObservable(msg, msg.Length, ip, port)
                    from r in this.receiveStream
                    where r.RemoteEndPoint.Address.ToString() == ip && r.RemoteEndPoint.Port == port
                    select r.Buffer;

            return o.Take(1).Timeout(TimeSpan.FromMilliseconds(timeOut));
        }

        public IDisposable Listen(Func<UdpReceiveResult, byte[]> process)
        {
            return this.receiveStream.Subscribe(async r =>
            {
                var msg = process(r);
                await client.SendObservable(msg, msg.Length, r.RemoteEndPoint);
            });
        }

    }
}
