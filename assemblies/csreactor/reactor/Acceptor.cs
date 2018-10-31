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
		public Socket						m_listen_sock;
		public AsyncAccept					m_async_accept;

		public iServiceHandlerFactory		m_service_handler_strategy;

		public Acceptor(iServiceHandlerFactory create_strategy)
		{
			//
			// TODO: Add constructor logic here
			//
			m_service_handler_strategy=create_strategy;

			// Create the new async accept operation
			m_async_accept = new AsyncAccept();
		}

		public int open(IPEndPoint ep)
		{
			m_listen_sock = new Socket(
				ep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			m_listen_sock.Bind(ep);
			m_listen_sock.Listen(1000);
			return m_async_accept.open(this, m_listen_sock);
		}

		public int accept()
		{
			Console.WriteLine("Acceptor::Accept");
			return m_async_accept.accept();
		}

		public override void handle_accept(AsyncAccept operation)
		{
			// retrieve the accepted socket
			Socket sock = (Socket)operation.handle();
			Socket sock2 = operation.m_args.AcceptSocket;
			iServiceHandler svc = m_service_handler_strategy.makeServiceHandler();
			svc.open(sock2);

			// start off another accept
			this.accept();
		}
	}
}
