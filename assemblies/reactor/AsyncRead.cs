using System;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncRead.
	/// </summary>
	public class AsyncRead : AsyncOperation
	{
		protected AsyncCallback		m_readCallback;

		private byte []				m_buffer;

		private IAsyncResult		m_ar;

		private int					m_bytes_read;

		public AsyncRead()
		{
			m_readCallback = new AsyncCallback(this.complete);
		}

		public int read(byte [] buffer)
		{
			m_buffer = buffer;
			m_ar = m_sock.BeginReceive(m_buffer, 0, m_buffer.Length, 0, m_readCallback, this);
			return 0;
		}

		public void complete(IAsyncResult ar)
		{
			m_read = m_sock.EndReceive(ar);

			// Call the handler
			handler().handle_read(this);

			m_ar = null;
		}
	}
}
