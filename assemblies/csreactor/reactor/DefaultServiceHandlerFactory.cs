using System;

namespace Reactor
{
	/// <summary>
	/// Summary description for DefaultServiceHandlerFactory.
	/// </summary>
	public class DefaultServiceHandlerFactory : iServiceHandlerFactory
	{
		public iServiceHandler makeServiceHandler()
		{
			return new DefaultServiceHandler();
		}
	}
}
