using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Reactor
{
	public class httpAcceptor : AsyncHandler
	{
		private bool						m_run;
		private string						m_prefix;
		private HttpListener				m_http_listener; 

		private iwsServiceHandlerFactory		m_service_handler_strategy;

		public httpAcceptor(iwsServiceHandlerFactory create_strategy)
		{
			m_run = true;
			m_service_handler_strategy=create_strategy;
		}

		public int open(string prefix)
		{
			if(!HttpListener.IsSupported)
			{
				Console.WriteLine("HttpListener not supported");
			}

			if(prefix == null)
				throw new ArgumentException("prefixes");

			m_prefix = new string(prefix);
			m_http_listener = new HttpListener();
			
			m_http_listener.Prefixes.Add(prefix);
			m_http_listener.Start();
			 return 0;
		}

		private int handle_accept(SocketAsyncEventArgs e)
		{
			return 0;
		}

		private static void e_completed(object sender, SocketAsyncEventArgs e)
		{
		}

		private async Task<bool> accept_task(int i)
		{
			while(m_run)
			{
				Console.WriteLine("Task {0} awaiting accept on {1}: ", i, m_prefix);
				HttpListenerContext ctx = await m_http_listener.GetContextAsync();
				string ipAddress = ctx.Request.RemoteEndPoint.Address.ToString();
				if (ctx.Request.IsWebSocketRequest)
				{
					Console.WriteLine("Websocket Request: IPAddress {0}", ipAddress);
					WebSocketContext wsCtx = await ctx.AcceptWebSocketAsync(
						subProtocol: null);
					WebSocket ws = wsCtx.WebSocket;
					iwsServiceHandler svc = m_service_handler_strategy.makeServiceHandler();
					Task t = Task.Factory.StartNew( async () =>
					{
						await svc.open(ws);
						Console.WriteLine("After Open()");
					});
				}
				else
				{
					Console.WriteLine("Http request: IPAddress {0}", ipAddress);
					ctx.Response.StatusCode = 400;
					ctx.Response.Close();
				}
			}
			return m_run;
		}

		public int accept()
		{
			Parallel.For(1,10, async (i) =>
			{
				await accept_task(i);
			});

			return 0;
		}
	}
}
