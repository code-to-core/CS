using System;
using System.Threading;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncTimer.
	/// </summary>
	public class AsyncTimer : AsyncOperation
	{
		public TimerCallback		m_timerCallback;

		private object				m_item;

		private Timer				m_timer;

		public AsyncTimer()
		{
			//
			// TODO: Add constructor logic here
			//
			m_timerCallback = new TimerCallback(this.complete);
		}

		public int wait(long dueTime, object item)
		{
			lock(this)
			{
				m_timer = new Timer(m_timerCallback, item, dueTime, Timeout.Infinite);
			}
			return 0;
		}

		public void complete(object item)
		{
			// Call the handler
			lock(this)
			{
				m_item = item;
				handler().handle_timer(this);
			}
		}

		public object item()
		{
			return m_item;
		}
	}
}
