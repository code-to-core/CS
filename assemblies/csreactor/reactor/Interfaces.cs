using System;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace Reactor
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public interface iServiceHandler
	{
            int open(Socket sock);
	}

        public interface iServiceHandlerFactory
        {
            iServiceHandler makeServiceHandler();
        }
}
