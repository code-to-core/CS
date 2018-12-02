using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Reactor;


namespace wsClient
{
	class wsClient
	{
		static void Main(string[] args)
		{
			//uri = new string("ws://40.122.109.9:8000/index");
			//uri = new string("ws://localhost:8001/demo");

			//Task.Run( async () =>
			//{
				Parallel.For(1,2, async (i) =>
				{
					var tasks = new List<Task>();
					foreach (string uri in args)
					{
						wsConnector c = new wsConnector(new wsClientApp(args));
						tasks.Add(c.connect(uri));
					}

					await Task.WhenAll(tasks.ToArray());

				});
			//});
			
			System.Threading.AutoResetEvent ev = new System.Threading.AutoResetEvent(false);

			ev.WaitOne();
		}

	}
}
