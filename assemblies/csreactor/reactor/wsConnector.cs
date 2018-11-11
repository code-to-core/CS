using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Reactor
{
   public class wsConnector
   {
		bool											m_run;
      public ClientWebSocket					m_connect_sock;

      Uri											m_uri;

      public iServiceHandlerFactory			m_service_handler_strategy;

      public wsConnector(iServiceHandlerFactory create_strategy)
      {
			m_run = true;
         m_service_handler_strategy=create_strategy;
      }

		private async Task<bool> connect_task()
		{
			while(m_run)
			{
				Console.WriteLine("Task Connecting to {0}: ", m_uri.ToString());
				try
				{
					m_connect_sock = new ClientWebSocket();
					await m_connect_sock.ConnectAsync(m_uri, CancellationToken.None);
					Console.WriteLine("Task Connected to {0}: ", m_uri.ToString());
				}
				catch(Exception ex)
				{
					Console.WriteLine("Exception {0}: ", ex);
				}
				finally
				{
					Thread.Sleep(1000);
					//await m_connect_sock.CloseAsync();
					m_connect_sock.Dispose();
				}
			}
			return m_run;
		}

      public int connect(string uri)
      {
         m_uri = new Uri(uri);

			Task t = Task.Factory.StartNew( async () =>
			{
				await connect_task();
			});
			return 0;
		}
   }
}
