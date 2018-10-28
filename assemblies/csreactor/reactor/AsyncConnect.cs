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
		public AsyncConnect()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int connect(IPEndPoint ep)
		{
			SocketAsyncEventArgs e = new SocketAsyncEventArgs();

			e.RemoteEndPoint = ep;
			e.UserToken = this;
			e.Completed += new EventHandler<SocketAsyncEventArgs>(e_completed);
			((Socket)handle()).ConnectAsync(e);
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
