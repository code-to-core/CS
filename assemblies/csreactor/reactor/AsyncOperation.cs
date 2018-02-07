using System;
using System.Net.Sockets;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncOperation.
	/// </summary>
	public class AsyncOperation
	{
		public AsyncOperation()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int open(AsyncHandler handler, object handle)
		{
			this.handler(handler);
			m_handle = handle;
			return 0;
		}

		public int cancel()
		{
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
