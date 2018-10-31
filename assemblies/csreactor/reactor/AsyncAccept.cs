using System;
using System.Net;
using System.Net.Sockets;

namespace Reactor
{
	/// <summary>
	/// Summary description for AsyncAccept.
	/// </summary>
	public class AsyncAccept :  AsyncOperation
	{
		public SocketAsyncEventArgs m_args;
		//public iServiceHandler				m_serviceHandler;

		public AsyncAccept()
		{
			//
			// TODO: Add constructor logic here
			//
			m_args = new SocketAsyncEventArgs();
			m_args.UserToken = this;
			m_args.Completed += new EventHandler<SocketAsyncEventArgs>(e_completed);
		}

		public int accept()
		{
			//m_serviceHandler = svc;
			m_args.AcceptSocket = null;

			if(((Socket)handle()).AcceptAsync(m_args) == false)
			{
				Console.WriteLine("Sync Connection Established");
				// TODO: this could cause infinite recursion, need to fix
				handler().handle_accept(this);
			}


			//((Socket)handle()).BeginAccept(handler().m_acceptCallback, this);
			return 0;
		}
		
		private static void e_completed(object sender, SocketAsyncEventArgs e)
		{
			AsyncAccept ac = (AsyncAccept)e.UserToken;
			if (e.AcceptSocket != null)
			{
				Console.WriteLine("Async Connection Established");

				ac.handler().handle_accept(ac);
			}
			else
			{
				Console.WriteLine("Connection Failed");
				((Socket)ac.handle()).Close();
			}
		}

	}
}
