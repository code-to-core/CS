using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Reactor
{
	public class httpAcceptor : AsyncHandler
	{
		private bool						m_run;
		private HttpListener				m_http_listener; 

		private iServiceHandlerFactory		m_service_handler_strategy;

		public httpAcceptor(iServiceHandlerFactory create_strategy)
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

		private async Task<bool> accept_task()
		{
			while(m_run)
			{
				HttpListenerContext ctx = await m_http_listener.GetContextAsync();
				if (ctx.Request.IsWebSocketRequest)
				{
					Console.WriteLine("Websocket Request");
				}
				else
				{
					Console.WriteLine("Http request");
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
				await accept_task();
			});

			return 0;
		}
	}
}
