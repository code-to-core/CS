using System;
using System.Net.Sockets;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncReceive.
	/// </summary>
	public class AsyncReceive : AsyncOperation
	{
		protected AsyncCallback		m_ReceiveCallback;

		private byte []				m_buffer;

		private IAsyncResult		m_ar;

		private int					m_bytes_received;

		public AsyncReceive()
		{
			m_ReceiveCallback = new AsyncCallback(this.complete);
		}

		public int receive(byte [] buffer)
		{
			m_buffer = buffer;
			try
			{
				m_ar = ((Socket)handle()).BeginReceive(m_buffer, 0, m_buffer.Length, 0, m_ReceiveCallback, this);
			}
			catch(SocketException e)
			{
				cancel();
				Console.WriteLine("Exception: {0}", e.SocketErrorCode); 
			}
			return 0;
		}

		public void complete(IAsyncResult ar)
		{
			try {
				m_bytes_received = ((Socket)handle()).EndReceive(ar);

				if(m_bytes_received == 0)
				{
					handler().handle_disconnect(this);
				}
				else
				{
					handler().handle_receive(this);
				}
			} catch {
				Console.WriteLine("Exception in AsyncReceive::complete()"); 
			}

			m_ar = null;
		}

		public int bytes_received()
		{
			return m_bytes_received;
		}

		public byte [] message()
		{
			return m_buffer;
		}
	}
}
