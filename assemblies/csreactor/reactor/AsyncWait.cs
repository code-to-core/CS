using System;
using System.Threading;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncWait.
	/// </summary>
	public class AsyncWait : AsyncOperation
	{
		public WaitCallback		m_waitCallback;

		private object				m_item;

		public AsyncWait()
		{
			//
			// TODO: Add constructor logic here
			//
			m_waitCallback = new WaitCallback(this.complete);
		}

		public int wait()
		{
			((EventQueue)handle()).getQ(this);
			return 0;
		}

		public void complete(object o)
		{
			// Call the handler
			m_item = o;
			handler().handle_work(this);
		}

		public object item()
		{
			return m_item;
		}
	}
}
