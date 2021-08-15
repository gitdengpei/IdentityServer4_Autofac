using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Units.IOC.DPEIAttribute
{
	/// <summary>
	/// IOC类型过滤特性
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class IOCService:Attribute
	{
		public IOCService()
		{

		}
	}
}
