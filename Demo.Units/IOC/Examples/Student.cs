using Demo.Units.IOC.DPEIAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Units.IOC.Examples
{
	[IOCService]
	public class Student
	{
		public Teacher teacher { get; set; }
		public void Study()
		{
			Console.WriteLine("学生开始学习");
		}
	}
}
