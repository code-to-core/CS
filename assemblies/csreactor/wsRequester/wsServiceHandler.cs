using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Reactor;


namespace Responder
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

			//bool keepGoing = true;

			WebSocketReceiveResult result = null;


			string line;
			while ((line = Console.ReadLine()) != null) 
			{
				//buffer = Encoding.ASCII.GetBytes("{ \"channel\": \"Index\" }");
				buffer = Encoding.ASCII.GetBytes(line);

				ArraySegment<Byte> segment = new ArraySegment<Byte>(buffer);

				await m_sock.SendAsync(buffer, 0, true, CancellationToken.None);
				do
				{
					result = await m_sock.ReceiveAsync(segment, CancellationToken.None);
					ms.Write(segment.Array, segment.Offset, result.Count);
				}
				while (!result.EndOfMessage);

				ms.Seek(0, SeekOrigin.Begin);

				Console.WriteLine(Encoding.UTF8.GetString(ms.GetBuffer()));
			}
			 return 0;
		}
	}
}
