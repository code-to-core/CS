using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Reactor;


namespace Responder
{
	class Requester
	{
		static string  uri;

		[STAThread]
		static void Main(string[] args)
		{
			//uri = new string("ws://40.122.109.9:8000/index");
			uri = new string("ws://localhost:8044/v1/erc20/0xD850942eF8811f2A866692A623011bDE52a462C1/transfers/stream/42/6736040");

			Parallel.For(1,2, (i) =>
			{
				wsConnector c = new wsConnector(new wsServiceHandlerFactory());
				Task<int> t=c.connect(uri);
				t.Wait();
			});

			System.Threading.AutoResetEvent ev = new System.Threading.AutoResetEvent(false);

			ev.WaitOne();
		}

	}
}
