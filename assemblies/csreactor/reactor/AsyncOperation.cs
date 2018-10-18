using System;
using System.Net.Sockets;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncOperation.
	/// </summary>
	public class AsyncOperation
	{
		protected bool m_stopping;

		public AsyncOperation()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int open(AsyncHandler handler, object handle)
		{
			m_stopping = false;
			this.handler(handler);
			m_handle = handle;
			return 0;
		}

		public int cancel()
		{
			m_stopping = true;
			return 0;
		}

		public void handler(AsyncHandler handler)
		{
			m_handler = handler;
		}

		public AsyncHandler handler()
		{
			return m_handler;
		}

		public object handle()
		{
			return m_handle;
		}

		protected object				m_handle;
		private AsyncHandler			m_handler;
	}
}
