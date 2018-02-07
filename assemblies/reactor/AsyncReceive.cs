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
			m_ar = ((Socket)handle()).BeginReceive(m_buffer, 0, m_buffer.Length, 0, m_ReceiveCallback, this);
			return 0;
		}

		public void complete(IAsyncResult ar)
		{
			m_bytes_received = ((Socket)handle()).EndReceive(ar);

			// Call the handler
			handler().handle_receive(this);

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
