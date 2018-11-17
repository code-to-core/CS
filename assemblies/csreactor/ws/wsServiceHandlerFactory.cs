using System;
using Reactor;

namespace Websocket
{
	/// <summary>
	/// Summary description for DefaultServiceHandlerFactory.
	/// </summary>
	public class wsServiceHandlerFactory  : iwsServiceHandlerFactory
	{
		public iwsServiceHandler makeServiceHandler()
		{
			return new wsServiceHandler();
		}
	}
}
