using System;
using System.Net;
using System.Net.Sockets;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncAccept.
	/// </summary>
	public class AsyncConnect :  AsyncOperation
	{
		public iServiceHandler				m_serviceHandler;
		public AsyncConnect()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public int connect(iServiceHandler svc, IPEndPoint ep)
		{
			m_serviceHandler = svc;
			((Socket)handle()).BeginConnect(ep, handler().m_connectCallback, this);
			return 0;
		}

		public iServiceHandler serviceHandler()
		{
			return m_serviceHandler;
		}

	}
}
