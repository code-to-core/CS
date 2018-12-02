using System;
using Reactor;

namespace wsServer
{
	/// <summary>
	/// Summary description for DefaultServiceHandlerFactory.
	/// </summary>
	public class wsServerApp  : iwsServiceHandlerFactory
	{
		public wsServerApp(string[] args)
		{

		}

		public iwsServiceHandler makeServiceHandler()
		{
			return new wsServiceHandler();
		}
	}
}
