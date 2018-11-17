using System;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Reactor
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public interface iwsServiceHandler
	{
            Task<int> open(WebSocket sock);
	}

	public interface iwsServiceHandlerFactory
	{
		iwsServiceHandler makeServiceHandler();
	}
}
