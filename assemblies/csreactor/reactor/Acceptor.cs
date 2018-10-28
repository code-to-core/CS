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
			m_listen_sock = new Socket(ep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			m_listen_sock.Bind(ep);
			m_listen_sock.Listen(1000);
			return m_async_accept.open(this, m_listen_sock);
		}

		public int accept()
		{
			iServiceHandler svc = m_service_handler_strategy.makeServiceHandler();
			return m_async_accept.accept(svc);
		}

		public override void handle_accept(IAsyncResult ar)
		{
			// retrieve the accepted socket
			AsyncAccept operation = (AsyncAccept) ar.AsyncState;
			Socket sock = (Socket)operation.handle();
			Socket sock2 = sock.EndAccept(ar);
			iServiceHandler svc = operation.serviceHandler();
			svc.open(sock2);

			// start off another accept
			this.accept();
		}
	}
}
