using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using Reactor;

namespace Websocket
{
	class Websocket
	{
		static httpAcceptor a;

		[STAThread]
		static void Main(string[] args)
		{
			a = new httpAcceptor(new DefaultServiceHandlerFactory());

			a.open("http://localhost:8001/demo/");

			a.accept();

			//wait for shutdown
			System.Threading.AutoResetEvent ev = new System.Threading.AutoResetEvent(false);
			
			ev.WaitOne();

		}
	}
}
