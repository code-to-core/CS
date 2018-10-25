using System;
using System.Threading;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncWork.
	/// </summary>
	public class AsyncWork : AsyncResult
	{
		public WaitCallback		m_waitCallback;

		private object				m_item;

		public AsyncWork()
		{
			//
			// TODO: Add constructor logic here
			//
			m_waitCallback = new WaitCallback(this.complete);
		}

		~AsyncWork()
		{
			Console.WriteLine("~AsyncWork()");
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
