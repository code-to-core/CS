using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Reactor;


namespace wsServer
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


			byte [] buffer = new byte[128];
			ArraySegment<Byte> segment = new ArraySegment<Byte>(buffer);
			WebSocketReceiveResult result = null;

			while(true)
			{
				MemoryStream ms = new MemoryStream();
				do
				{
					try
					{
						result = await m_sock.ReceiveAsync(segment, CancellationToken.None);
					}
					catch (Exception ex)
					{
						Console.Error.WriteLine(ex.ToString());
						break;
					}
					if(result.MessageType == WebSocketMessageType.Close)
					{
						Console.WriteLine("Received Close");
						await m_sock.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by peer", CancellationToken.None);
						break;
					}

					ms.Write(segment.Array, segment.Offset, result.Count);
				}
				while (!result.EndOfMessage);

				ms.Seek(0, SeekOrigin.Begin);

				await m_sock.SendAsync(ms.GetBuffer(), 0, true, CancellationToken.None);
			}
			return 0;
		}
	}
}
