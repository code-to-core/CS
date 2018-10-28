using System;
using System.Net;
using System.Net.Sockets;

namespace Reactor
{
   public class Connector : AsyncHandler
   {
      public Socket                      m_connect_sock;
      IPEndPoint                         m_ep;
      public AsyncConnect                m_async_connect;

      public iServiceHandlerFactory      m_service_handler_strategy;

      public Connector(iServiceHandlerFactory create_strategy)
      {
         m_service_handler_strategy=create_strategy;
      }

      public int open(IPEndPoint ep)
      {
         m_ep = ep;
			return 0;

      }

      public int connect()
      {
			//AsyncTimer t = new AsyncTimer();
			//t.open(this, t);

         m_async_connect = new AsyncConnect();
         m_connect_sock = new Socket(
            m_ep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

			m_async_connect.open(this, m_connect_sock);

			// Set a timeout for the completion either the 
			// handlers connect or timer callbacks will happen first
			//t.wait(3000, m_async_connect);
         return m_async_connect.connect(m_ep);
      }

      public override void handle_connect(AsyncConnect ar)
      {
         Socket sock = (Socket)ar.handle();
         iServiceHandler svc = m_service_handler_strategy.makeServiceHandler();
         svc.open(sock);
      }

		public override void handle_timer(object o)
		{
			AsyncTimer ar = (AsyncTimer)o;
			AsyncConnect op_connect = (AsyncConnect)ar.item();
			Socket sock = (Socket)op_connect.handle();
			if(!sock.Connected)
			{
				// Close the socket, timeout 
				sock.Close();
			}
		}
   }
}
