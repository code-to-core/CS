using System;
using System.Net.Sockets;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncAccept.
	/// </summary>
	public class AsyncAccept :  AsyncOperation
	{
		public iServiceHandler				m_serviceHandler;

		public AsyncAccept()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int accept(iServiceHandler svc)
		{
			m_serviceHandler = svc;
			((Socket)handle()).BeginAccept(handler().m_acceptCallback, this);
			return 0;
		}
		public iServiceHandler serviceHandler()
		{
			return m_serviceHandler;
		}

	}
}
