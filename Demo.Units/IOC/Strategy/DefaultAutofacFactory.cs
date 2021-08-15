using Demo.Units.IOC.DPEIAttribute;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Demo.Units.IOC.Strategy
{
	/// <summary>
	/// 创建一个Autofac IOC工厂（通用）
	/// List
	/// Set
	/// 字典
	/// 
	/// 为什么用字典  -->目的：为了性能 0 1 一对一 查询最快
	/// </summary>
	public class DefaultAutofacFactory
	{
		/// <summary>
		/// 1.IOC容器（存储对象）
		/// </summary>
		private Dictionary<string, object> iocContainer = new Dictionary<string, object>();

		/// <summary>
		/// 1.IOCType容器（存储对象）
		/// </summary>
		private Dictionary<string, Type> iocTypeContainer = new Dictionary<string, Type>();
		/// <summary>
		/// Autofac
		/// 目标1： 创建对象(功能完成）
		/// 目标2： 存储对象（功能完成）
		/// 目标3： 对象依赖（属性赋值）
		/// </summary>
		public DefaultAutofacFactory()
		{
			//1.加载项目中所有类型（反射类类型的结合）
			Assembly assembly = Assembly.Load("Demo.Units");


			//2.使用反射从程序集获取所有对象类型。
			Type[] types = assembly.GetTypes();
			foreach (var type in types)
			{
				IOCService iOCservice = type.GetCustomAttribute<IOCService>();
				if (iOCservice != null)
				{
					iocTypeContainer.Add(type.Name, type);
					//如何做到指定获取对象的实例
					//如何过滤对象？
					//特性：
				}
			}
			#region 弃用  去掉了 types[] type
			//foreach (var type in types)
			//{
			//	#region 放弃  只能一层对象依赖问题
			//	////3|创建对象
			//	//Object _object = Activator.CreateInstance(type);

			//	////3.1 对象属性赋值Student ---->Teacher
			//	////反射获取
			//	//PropertyInfo[] propertyInfos = type.GetProperties(); //给所有的属性赋值（目的通用）
			//	//foreach (var propertyInfo in propertyInfos)
			//	//{
			//	//	foreach (var type1 in types)
			//	//	{
			//	//		//1、通过属性名称和type名称匹配出需要的对象类型
			//	//		if (propertyInfo.PropertyType.Name.Equals(type1.Name))
			//	//		{
			//	//			//2.开始创建匹配成功的对象 Teacher ----->School
			//	//			Object _objectValue = Activator.CreateInstance(type1);
			//	//			#region 二次循环
			//	//			PropertyInfo[] propertyInfos1 = type.GetProperties(); //给所有的属性赋值（目的通用）
			//	//			foreach (var propertyInfo1 in propertyInfos1)
			//	//			{
			//	//				foreach (var type2 in types)
			//	//				{
			//	//					//1、通过属性名称和type名称匹配出需要的对象类型
			//	//					if (propertyInfo.PropertyType.Name.Equals(type2.Name))
			//	//					{
			//	//						//2.开始创建匹配成功的对象 
			//	//						Object _objectValue1 = Activator.CreateInstance(type2);
			//	//						propertyInfo.SetValue(_object, _objectValue1);
			//	//					}
			//	//				}

			//	//			}
			//	//			#endregion
			//	//			propertyInfo.SetValue(_object, _objectValue);
			//	//		}
			//	//	} 
			//	#endregion

			//	#region 递归
			//	//1、对象创建（使用递归创建对象）
			//	object _object = CreateObject(type, types);
			//	#endregion
			//	//缺陷：
			//	//1、只能一层对象依赖问题。
			//	//2、代码层级多了，导致代码难懂。 --->简易好懂
			//	//3、无法解决n层代码依赖的问题。 --->能够解决。

			//	//原因：对象层次依赖多导致的。
			//	//方案：递归
			//	//落地：如何落地递归
			//	//步骤：1、什么是递归，2、如何通过递归改造代码

			//	//4.对象存储。如何创建key
			//	//要求：1、key唯一 2.key方便记忆
			//	//GUID：记不住
			//	//雪花算法：记不住
			//	//命名空间 + 类名 
			//	iocContainer.Add(type.FullName, _object);

			//	//改良之后，相应的出现新的缺陷：内存溢出问题。

			//	//方案：加内存 1亿  --->少量的内存，然后处理大量的对象
			//	//根据需要创建，长时间不用，自动销毁
			//	//懒加载
			//} 
			#endregion


		}

		/// <summary>
		/// 递归 自己调用自己
		/// 步骤
		/// 1、从代码中找出通用的逻辑
		/// 2、从通用的逻辑中找出通用的参数
		/// 3、自己调用自己创建的对象逻辑
		/// 
		/// 
		/// 缺陷
		/// 1、性能比较低
		/// 原因：两层for循环
		/// 过程： 10000 * 10000 =  1亿次 一次时间是20ms  几分钟   --->几秒秒  目标
		/// 方案：空间换时间。怎么空间换时间。
		/// 工具：字典 1对1
		/// 步骤
		/// 1、把Type存储到字典
		/// 2、从字典中取出Type
		/// 
		/// 总结道理：性能的提升是以其他代价为基础的。
		/// 空间换时间
		/// 
		/// 时间换空间
		/// </summary>
		public object CreateObject(Type type1) //去掉 type[] types参数
		{
			//2.开始创建匹配成功的对象 Teacher ----->School  -->City
			Object _object = Activator.CreateInstance(type1);
			PropertyInfo[] propertyInfos1 = type1.GetProperties(); //给所有的属性赋值（目的通用）
			foreach (var propertyInfo1 in propertyInfos1)
			{
				#region 放弃 10000 需要 乘 10000
				//foreach (var type2 in types)
				//{
				//	//1、通过属性名称和type名称匹配出需要的对象类型
				//	if (propertyInfo1.PropertyType.Name.Equals(type2.Name))
				//	{

				//		//2.开始创建匹配成功的对象 
				//		Object _objectValue1 = CreateObject(type2, types);
				//		propertyInfo1.SetValue(_object, _objectValue1);
				//	}
				//} 
				#endregion

				#region 采用 1000 需要 乘 1
				Type typeValue = iocTypeContainer[propertyInfo1.PropertyType.Name];
				//2.开始创建匹配成功的对象 
				Object _objectValue1 = CreateObject(typeValue);
				propertyInfo1.SetValue(_object, _objectValue1);
				#endregion
			}
			return _object;
		}

		public object GetObject(string TypeName)
		{
			Type type = iocTypeContainer[TypeName];
			//1、对象创建（使用递归创建对象）
			object _object = CreateObject(type);
			iocContainer.Add(type.FullName, _object);

			return _object;
		}
	}
}
