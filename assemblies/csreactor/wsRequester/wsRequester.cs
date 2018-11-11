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
			uri = new string("ws://localhost:8001/demo");

			wsConnector c = new wsConnector(new DefaultServiceHandlerFactory());

			c.connect(uri);

			Console.ReadLine();
		}

	}
}
