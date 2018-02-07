using System;
using System.Net.Sockets;


namespace Reactor
{
	/// <summary>
	/// Summary description for DefaultServiceHandler.
	/// </summary>
	public class DefaultServiceHandler : AsyncHandler, iServiceHandler
	{
		protected Socket m_sock;

		protected AsyncReceive						m_async_receive;
		protected AsyncSend							m_async_send;

		protected EventQueue						m_event_queue;
		protected AsyncWait							m_async_wait;

		public DefaultServiceHandler()
		{
			//
			// TODO: Add constructor logic here
			//
			m_async_receive = new AsyncReceive();
			m_async_send = new AsyncSend();

			m_event_queue = new EventQueue();
			m_async_wait = new AsyncWait();

			m_async_wait.open(this, m_event_queue);

			// Issue the first wait
			m_async_wait.wait();

		}

		public virtual int open(Socket sock)
		{
			m_sock = sock;
			m_async_receive.open(this, m_sock);
			m_async_send.open(this, m_sock);
			// Begin reading bytes from the socket and echo them all back
			byte [] buffer = new byte[128];
			m_async_receive.receive(buffer);
			return 0;
		}

		public override void  handle_receive(AsyncReceive ar)
		{
			if (ar.bytes_received() > 0)
			{
				m_event_queue.putQ(ar.message());
				// start a new receive
				byte [] buffer = new byte[128];
				m_async_receive.receive(buffer);
			}
			else
			{
				m_sock.Close();
			}
		}

		public override void  handle_work(AsyncWait ar)
		{
			byte [] buffer = (byte [])ar.item();

			// Craete a new timer and send the buffer
			AsyncTimer t = new AsyncTimer();
			t.open(this, 0);
			t.wait(1000, buffer);

			// start a new wait
			m_async_wait.wait();
		}

		public override void  handle_timer(AsyncTimer ar)
		{
			byte [] buffer = (byte [])ar.item();
			
			m_async_send.send(buffer);

			// start a new wait
			m_async_wait.wait();
		}
	}
}
