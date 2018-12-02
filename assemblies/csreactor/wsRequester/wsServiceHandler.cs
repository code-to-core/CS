using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Reactor;


namespace wsClient
{
	public class wsServiceHandler : iwsServiceHandler
	{
		protected WebSocket m_sock;

		public wsServiceHandler()
		{
		}

		~wsServiceHandler()
		{
			Console.Error.WriteLine("Destruct wsServiceHandler");
		}

		public virtual async Task<int> open(WebSocket sock)
		{
			m_sock = sock;

			var tasks = new List<Task>();


			WebSocketReceiveResult result = null;

			tasks.Add(Task.Run( async () =>
			{
				string line;
				byte [] buffer = new byte[128];
				ArraySegment<Byte> segment = new ArraySegment<Byte>(buffer);
				while ((line = Console.ReadLine()) != null) 
				{
					buffer = Encoding.ASCII.GetBytes(line);
					await m_sock.SendAsync(buffer, 0, true, CancellationToken.None);
				}
				await Task.Delay(10000);
				//await m_sock.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Done", CancellationToken.None);
			}));

			tasks.Add(Task.Run( async () =>
			{
				bool keepGoing = true;
				byte [] buffer = new byte[128];
				ArraySegment<Byte> segment = new ArraySegment<Byte>(buffer);
				while (keepGoing == true)
				{
					MemoryStream ms = new MemoryStream();
					do
					{
						result = await m_sock.ReceiveAsync(segment, CancellationToken.None);
						if(result.MessageType == WebSocketMessageType.Close)
						{
							Console.Error.WriteLine("Received Close");
							await m_sock.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by peer", CancellationToken.None);
							break;
						}
						ms.Write(segment.Array, segment.Offset, result.Count);
					}
					while (!result.EndOfMessage);

					ms.Seek(0, SeekOrigin.Begin);
					Console.WriteLine(Encoding.UTF8.GetString(ms.ToArray()));
				}
				Console.Error.WriteLine("Receiver ending");
			}));

			await Task.WhenAll(tasks.ToArray());
			return 0;
		}
	}
}
