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
			try 
			{
				m_ar = ((Socket)handle()).BeginSend(m_buffer, 0, m_buffer.Length, 0, m_sendCallback, this);
			} 
			catch(SocketException e)
			{
				Console.WriteLine("Exception: {0}", e.SocketErrorCode); 
				handler().handle_disconnect(this);
			}
			return 0;
		}

		public void complete(IAsyncResult ar)
		{
			try
			{
				m_bytes_sent = ((Socket)handle()).EndSend(ar);

				if(m_bytes_sent != m_buffer.Length)
				{
					Console.WriteLine("Incomplete Send");
				}

				// Call the handler
				handler().handle_send(this);
			}
			catch(SocketException e)
			{
				Console.WriteLine("Exception: {0}", e.SocketErrorCode); 
				handler().handle_disconnect(this);
			}

			m_ar = null;
			m_buffer = null;
		}

		public int bytes_sent()
		{
			return m_bytes_sent;
		}
	}
}
