using System;
using System.Threading;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncHandler.
	/// </summary>
	public class AsyncHandler
	{
		public AsyncCallback					m_acceptCallback;
		public AsyncCallback					m_connectCallback;

		public AsyncHandler()
		{
			//
			// TODO: Add constructor logic here
			//
			m_acceptCallback = new AsyncCallback(this.handle_accept);
			m_connectCallback = new AsyncCallback(this.handle_connect);

		}

		public virtual void handle_timer(AsyncTimer ar)
		{

		}
		public virtual void handle_accept(IAsyncResult ar)
		{

		}
		public virtual void handle_connect(IAsyncResult ar)
		{

		}
		public virtual void handle_receive(AsyncReceive ar)
		{

		}
		public virtual void handle_send(AsyncSend ar)
		{

		}
		public virtual void handle_work(AsyncWork ar)
		{

		}
		public virtual void handle_disconnect(AsyncOperation ao)
		{

		}
	}
}
