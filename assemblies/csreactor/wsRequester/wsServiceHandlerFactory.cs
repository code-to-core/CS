using System;
using Reactor;

namespace Responder
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
