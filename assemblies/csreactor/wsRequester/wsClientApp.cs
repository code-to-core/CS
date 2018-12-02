using System;
using Reactor;

namespace wsClient
{
	/// <summary>
	/// Summary description for DefaultServiceHandlerFactory.
	/// </summary>
	public class wsClientApp  : iwsServiceHandlerFactory
	{
		public wsClientApp(string[] args)
		{

		}

		public iwsServiceHandler makeServiceHandler()
		{
			return new wsServiceHandler();
		}
	}
}
