using System;
using System.Net.Sockets;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncSend.
	/// </summary>
	public class AsyncSend : AsyncOperation
	{
		protected AsyncCallback		m_sendCallback;

		private byte []				m_buffer;

		private IAsyncResult		m_ar;

		private int					m_bytes_sent;

		public AsyncSend()
		{
			m_sendCallback = new AsyncCallback(this.complete);
		}

		public int send(byte [] buffer)
		{
			m_buffer = buffer;
			m_ar = ((Socket)handle()).BeginSend(m_buffer, 0, m_buffer.Length, 0, m_sendCallback, this);
			return 0;
		}

		public void complete(IAsyncResult ar)
		{
			m_bytes_sent = ((Socket)handle()).EndSend(ar);

			// Call the handler
			handler().handle_send(this);

			m_ar = null;
		}

		public int bytes_sent()
		{
			return m_bytes_sent;
		}
	}
}
