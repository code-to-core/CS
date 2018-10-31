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
		SocketAsyncEventArgs m_args;

		public AsyncConnect()
		{
			//
			// TODO: Add constructor logic here
			//
			m_args = new SocketAsyncEventArgs();
			m_args.UserToken = this;
			m_args.Completed += new EventHandler<SocketAsyncEventArgs>(e_completed);
		}

		public int connect(IPEndPoint ep)
		{
			m_args.RemoteEndPoint = ep;
			((Socket)handle()).ConnectAsync(m_args);
			return 0;
		}

		private static void e_completed(object sender, SocketAsyncEventArgs e)
		{
			if (e.ConnectSocket != null)
			{
				Console.WriteLine("Connection Established : ");

				AsyncConnect ac = (AsyncConnect)e.UserToken;
				ac.handler().handle_connect(ac);
			}
			else
			{
				Console.WriteLine("Connection Failed");
			}
		}

	}
}
