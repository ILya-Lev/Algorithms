using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
	[TestClass]
	public class MyLinkedListUnitTests
	{
		[TestMethod]
		public void ReverseEven_NormalCheck_ShouldReturnValidSequence()
		{
			var list = new MyLinkedList<int>();
			for (int i = 0; i < 10; i++)
			{
				list.AddItem(i);
			}

			var evenList = MyLinkedList<int>.ReverseEven(list);

			foreach (var item in evenList)
			{
				Console.Write(item + " ");
			}
		}

		[TestMethod]
		public void ReverseEvenInPlace_NormalCheck_ShouldReturnValidSequence()
		{
			var list = new MyLinkedList<int>();
			for (int i = 0; i < 10; i++)
			{
				list.AddItem(i);
			}

			var evenList = MyLinkedList<int>.ReverseEvenInPlace(list);

			foreach (var item in evenList)
			{
				Console.Write(item + " ");
			}
		}
	}
}