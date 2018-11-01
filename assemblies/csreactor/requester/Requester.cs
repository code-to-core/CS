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
		static IPEndPoint ep;

		[STAThread]
		static void Main(string[] args)
		{
			ep = new IPEndPoint(0, 12003);

			Connector c = new Connector(new DefaultServiceHandlerFactory());

			c.open(ep);

			Parallel.For(1,10, i =>
			//for(int i=0; i<5000; i++) 
			{
				c.connect();
			});
			//}

			Console.ReadLine();
		}

	}
}
