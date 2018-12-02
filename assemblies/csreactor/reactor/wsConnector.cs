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
		int											m_run;
      public ClientWebSocket					m_connect_sock;

      Uri											m_uri;

      public iwsServiceHandlerFactory			m_service_handler_strategy;

      public wsConnector(iwsServiceHandlerFactory create_strategy)
      {
		  m_run =  0;
	
		  m_service_handler_strategy=create_strategy;
      }

		private async Task<int> connect_task()
		{
			while(m_run == 0)
			{
				Console.Error.WriteLine("Task Connecting to {0}: ", m_uri.ToString());

				var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));

				try
				{
					m_connect_sock = new ClientWebSocket();
				//	await m_connect_sock.ConnectAsync(m_uri, CancellationToken.None);
					m_connect_sock.ConnectAsync(m_uri, cancellationTokenSource.Token).Wait(
						cancellationTokenSource.Token);
					
					Console.Error.WriteLine("Task Connected to {0}: ", m_uri.ToString());
					iwsServiceHandler svc = 
						m_service_handler_strategy.makeServiceHandler();
					m_run = await svc.open(m_connect_sock);
				}
				catch(Exception ex)
				{
					Console.Error.WriteLine("Connection failed: {0}", ex.Message);
					// Async wait before re-trying
					await Task.Delay(3000);
				}
				finally
				{
					//await Task.Delay(3000);
					//await m_connect_sock.CloseAsync();
					m_connect_sock.Dispose();
				}
			}
			return m_run;
		}

      public async Task<int> connect(string uri)
      {
         m_uri = new Uri(uri);

			//Task t = Task.Factory.StartNew( async () =>
			//{
				await connect_task();
			//});
			return 0;
		}
   }
}
