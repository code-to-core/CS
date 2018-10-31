using System;
using System.Threading;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncHandler.
	/// </summary>
	public class AsyncHandler
	{
		public TimerCallback					m_timerCallback;

		public AsyncHandler()
		{
			//
			// TODO: Add constructor logic here
			//
			m_timerCallback = new TimerCallback(this.handle_timer);

		}

		public virtual void handle_timer(object ar)
		{

		}
		public virtual void handle_accept(AsyncAccept ar)
		{

		}
		public virtual void handle_connect(AsyncConnect ar)
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
