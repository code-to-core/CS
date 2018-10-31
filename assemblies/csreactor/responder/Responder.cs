using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using Reactor;

namespace Responder
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Responder
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		static IPEndPoint ep;
		static Acceptor a;

		[STAThread]
		static void Main(string[] args)
		{
			
			ep = new IPEndPoint(0, 12003);

			a = new Acceptor(new DefaultServiceHandlerFactory());

			a.open(ep);

			a.accept();
			//wait for shutdown
			System.Threading.AutoResetEvent ev = new System.Threading.AutoResetEvent(false);
			
			ev.WaitOne();

		}
	}
}
