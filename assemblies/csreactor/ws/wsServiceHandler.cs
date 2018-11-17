using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Reactor;


namespace Websocket
{
	public class wsServiceHandler : AsyncHandler, iwsServiceHandler
	{
		protected WebSocket m_sock;

		public wsServiceHandler()
		{
		}

		~wsServiceHandler()
		{
			Console.WriteLine("Destruct wsServiceHandler");
		}

		public virtual async Task<int> open(WebSocket sock)
		{
			m_sock = sock;

			MemoryStream ms = new MemoryStream();

			byte [] buffer = new byte[128];
			ArraySegment<Byte> segment = new ArraySegment<Byte>(buffer);
			WebSocketReceiveResult result = null;

				while(true)
				{
					do
					{
						result = await m_sock.ReceiveAsync(segment, CancellationToken.None);
						ms.Write(segment.Array, segment.Offset, result.Count);
					}
					while (!result.EndOfMessage);

					ms.Seek(0, SeekOrigin.Begin);

					await m_sock.SendAsync(ms.GetBuffer(), 0, true, CancellationToken.None);
				}

		}
	}
}
