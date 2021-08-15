using Demo.Units.IOC.Examples;
using Demo.Units.IOC.Strategy;
using System;

namespace Demo.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			DefaultAutofacFactory defaultAutofacFactory = new DefaultAutofacFactory();
			Student student = (Student)defaultAutofacFactory.GetObject("Student");
			student.Study();
			student.teacher.Classes();

			//1、Autofac核心思想

			//如何把其他框架对象集成到Autofac

			//通过抽象工厂模式和扩展方式。

			//总结以下
			//1、工厂，2、反射，3、递归算法   --- 内存溢出 ---- 懒加载  --- 即用即创建，4、空间换时间思想（压缩算法 360压缩 --- 代码难懂 ---  如何化码简洁  --- 重构），4、特性。

			//要是一个大的功能，需要有综合能力。
			//综合能力如何提炼呢？

			//步骤
			//1、需要清楚每一个知识点的背后的问题，本质。从什么问题中提炼出来的。
			//2、每一个技术点内部是如何实现的原理。
			//3、每一个技术导致的问题是什么。


			//总结：串联知识。 原则 ------->综合能力。

			//如果想成为架构。把架构串起来。工具（项目）----->  技术

			//如果想成为p6架构 通过什么项目结合什么技术去形成p6架构。

			//p6架构  ---  微服务项目（团队管理 秒杀项目） + 分布式技术 20个

			//进一步，达到如何学习的层面。细节。细节决定成败。


		}
	}
}
