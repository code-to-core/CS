using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using Reactor;

namespace wsServer
{
	class wsServer
	{
		static wsAcceptor acceptor;

		static void Main(string[] args)
		{
			iwsServiceHandlerFactory app = new wsServerApp(args);

			acceptor = new wsAcceptor(app);

			acceptor.open("http://localhost:8001/demo/");

			acceptor.accept();

			//wait for shutdown
			System.Threading.AutoResetEvent ev = new System.Threading.AutoResetEvent(false);
			
			ev.WaitOne();

		}
	}
}
