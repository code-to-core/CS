using System;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncResult.
	/// </summary>
	public class AsyncResult
	{
		private AsyncHandler			m_handler;

		public AsyncResult()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void handler(AsyncHandler handler)
		{
			m_handler = handler;
		}

		public AsyncHandler handler()
		{
			return m_handler;
		}

		public int open(AsyncHandler handler)
		{
			this.handler(handler);
			return 0;
		}
	}
}
