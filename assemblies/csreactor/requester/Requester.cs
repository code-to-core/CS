using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Responder
{
	class Requester
	{
		static IPEndPoint ep;

		[STAThread]
		static void Main(string[] args)
		{
			Parallel.For(1,5000, i =>
			{
				Connect();
			});
			Console.ReadLine();
		}

		private static void Connect()
		{
			
			ep = new IPEndPoint(0, 12003);

			Socket s = new Socket(
				ep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

			SocketAsyncEventArgs e = new SocketAsyncEventArgs();

			e.RemoteEndPoint = ep;
			//e.UserToken = s;
			e.Completed += new EventHandler<SocketAsyncEventArgs>(e_completed);
			s.ConnectAsync(e);
		}

		
		private static void e_completed(object sender, SocketAsyncEventArgs e)
		{
			if (e.ConnectSocket != null)
			{
				Console.WriteLine("Connection Established : ");
			}
			else
			{

				Console.WriteLine("Connection Failed");
			}
		}
	}
}
