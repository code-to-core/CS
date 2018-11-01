using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Reactor
{
	/// <summary>
	/// Summary description for Acceptor.
	/// </summary>
	public class Acceptor : AsyncHandler
	{
		private Socket						m_listen_sock;
		//private AsyncAccept					m_async_accept;
		//private SocketAsyncEventArgs			m_args;

		private iServiceHandlerFactory		m_service_handler_strategy;

		public Acceptor(iServiceHandlerFactory create_strategy)
		{
			//
			// TODO: Add constructor logic here
			//
			m_service_handler_strategy=create_strategy;

			// Create the new async accept operation
			//m_async_accept = new AsyncAccept();
		}

		public int open(IPEndPoint ep)
		{
			m_listen_sock = new Socket(
				ep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			m_listen_sock.Bind(ep);
			m_listen_sock.Listen(1000);
			return 0;
			//return m_async_accept.open(this, m_listen_sock);
		}

		private int handle_accept(SocketAsyncEventArgs e)
		{
			Socket sock2 = e.AcceptSocket;
			iServiceHandler svc = m_service_handler_strategy.makeServiceHandler();
			svc.open(sock2);
			return 0;
		}

		private static void e_completed(object sender, SocketAsyncEventArgs e)
		{
			Acceptor a = (Acceptor)e.UserToken;
			Console.WriteLine("Async Connect");
			a.handle_accept(e);                                // TODO: return to the pool
			a.accept();
		}

		private bool try_accept(SocketAsyncEventArgs e)
		{
			e.UserToken = this;
			e.Completed += new EventHandler<SocketAsyncEventArgs>(e_completed);
			e.AcceptSocket = null;
			return m_listen_sock.AcceptAsync(e);
		}

		public int accept()
		{
			SocketAsyncEventArgs e = new SocketAsyncEventArgs();                // TODO: should come from a pool
			while(try_accept(e) == false)
			{
				Console.WriteLine("Sync Connect");
				handle_accept(e);
			}
			return 0;
		}
	}
}
