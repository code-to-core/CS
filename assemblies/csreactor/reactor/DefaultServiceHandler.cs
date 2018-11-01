using System;
using System.Text;
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
		protected AsyncSend							m_async_init;

		protected EventQueue						m_event_queue;
		protected AsyncWork							m_async_work;

		public DefaultServiceHandler()
		{
			//
			// TODO: Add constructor logic here
			//
			m_async_receive = new AsyncReceive();
			m_async_send = new AsyncSend();
			m_async_init = new AsyncSend();

			m_event_queue = new EventQueue();
			m_async_work = new AsyncWork();



		}

		~DefaultServiceHandler()
		{
			Console.WriteLine("Destruct DefaultServiceHandler");
			m_async_receive = null;
			m_async_send = null;
			m_async_work = null;
			m_event_queue = null;
		}

		public virtual int open(Socket sock)
		{
			m_sock = sock;

			// Setting non-blocking mode has no effect on async calls
			//sock.Blocking = false;
			// Issue the first work
			m_async_work.open(this);
			m_event_queue.getQ(m_async_work);
			//m_async_work.work();
			//m_event_queue.getQ(m_async_work);

			m_async_receive.open(this, m_sock);
			m_async_init.open(this, m_sock);
			m_async_send.open(this, m_sock);
			// Begin reading bytes from the socket and echo them all back
			byte [] buffer = new byte[128];
			m_async_receive.receive(buffer);

			
			byte [] message = new byte[128];
			message = Encoding.ASCII.GetBytes("hello");

			m_async_init.send(message);

			return 0;
		}

		public override void  handle_disconnect(AsyncOperation ao)
		{
			Console.WriteLine("Disconnect");
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
				Console.WriteLine("handle_receive(), Disconnected");
				while(m_event_queue.itemQsize() > 0)
				{
					Console.WriteLine("{0}", m_event_queue.itemQsize());
					System.Threading.Thread.Sleep(5000);
				}
				m_sock.Close();
				// A good implementation will initiate a shutdown of the socket here, whicl allowing time for the
				//sends to drain. This couls be done by looking at the work queue to see how many sends are 
				// pending
				//m_sock.Close();
			}
		}

		public override void  handle_work(AsyncWork ar)
		{
			byte [] buffer = (byte [])ar.item();

			// Craete a new timer and send the buffer
			//AsyncTimer t = new AsyncTimer();
			//t.open(this, 0);
			//t.work(1, buffer);

			// start a new work
			m_async_send.send(buffer);
			//m_async_work.work();
			m_event_queue.getQ(ar);
		}

		public override void  handle_timer(object o)
		{
			AsyncTimer ar = (AsyncTimer)o;
			byte [] buffer = (byte [])ar.item();
			
			m_async_send.send(buffer);

			// start a new work
			//m_async_work.work();
		}
			
		public override void  handle_send(AsyncSend ar)
		{
			//m_async_work.work();
		}
	}
}
