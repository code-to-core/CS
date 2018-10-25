using System;
using System.Net;
using System.Net.Sockets;

namespace Reactor
{
	/// <summary>
	/// Summary description for Connector.
	/// </summary>
	public class Connector : AsyncHandler
	{
		public Socket						m_sock;
        IPEndPoint                          m_ep;
		public AsyncConnect					m_async_connect;

		public iServiceHandlerFactory		m_service_handler_strategy;

		public Connector(iServiceHandlerFactory create_strategy)
		{
			m_service_handler_strategy=create_strategy;
			m_async_connect = new AsyncConnect();
		}

		public int open(IPEndPoint ep)
		{
            m_ep = ep;
			m_sock = new Socket(ep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			return 0;
		}

		public int connect()
		{
			iServiceHandler svc = m_service_handler_strategy.makeServiceHandler();
			return m_async_connect.connect(svc, m_ep);
		}

		public override void handle_connect(IAsyncResult ar)
		{
			AsyncConnect operation = (AsyncConnect) ar.AsyncState;
			Socket sock = (Socket)operation.handle();
 			iServiceHandler svc = operation.serviceHandler();
        	svc.open(sock);
		}
	}
}
