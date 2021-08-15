using Demo.Units.IOC.DPEIAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Units.IOC.Examples
{
	[IOCService]
	public class Teacher
	{
		public void Classes()
		{
			Console.WriteLine("小菜老师开始上课");
		}
	}
}
