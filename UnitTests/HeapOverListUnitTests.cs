using Algorithms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class HeapOverListUnitTests
	{
		[TestMethod]
		public void Add_DistinctValues_ShoulBeInAscendingOrder()
		{
			var heap = new HeapOverList<int>();
			var data = new[] { 5, 8, 6, 3, 1, 9, 2 }.ToList();

			data.ForEach(item => heap.Add(item));

			heap._storage.Should().BeInAscendingOrder();
		}
		[TestMethod]
		public void Add_DuplicatedValues_ShoulBeInAscendingOrder()
		{
			var heap = new HeapOverList<int>();
			var data = new[] { 5, 8, 6, 3, 1, 5, 8, 6, 9, 9, 2 }.ToList();

			data.ForEach(item => heap.Add(item));

			heap._storage.Should().BeInAscendingOrder();
		}
		[TestMethod]
		public void Add_NullValues_ShoulBeInAscendingOrder()
		{
			var heap = new HeapOverList<TestDataType>();
			var data = new[] { 5, 8, 6, 3, 1, 5, 8, 6, 9, 9, 2 }
						.Select((n, idx) => idx % 4 == 0
										? default(TestDataType)
										: new TestDataType { Value = n })
						.ToList();

			data.ForEach(item => heap.Add(item));

			heap._storage.Should().BeInAscendingOrder();
		}

		private class TestDataType : IComparable<TestDataType>
		{
			public int Value { get; set; }
			public int CompareTo(TestDataType other)
			{
				if (other == null)
					return Comparer<TestDataType>.Default.Compare(this, null);
				return Value.CompareTo(other.Value);
			}
		}

		[TestMethod]
		public void RemoveMin_ShouldReturnMinElement_DecreaseAmountByOne()
		{
			var heap = new HeapOverList<int>();
			heap.AddRange(new[] { 5, 8, 6, 3, 1, 5, 8, 6, 9, 9, 2 });

			var initMin = heap.Min;
			var initCount = heap.Count;

			var removedMin = heap.RemoveMin();

			initMin.Should().Be(removedMin);
			heap.Count.Should().Be(initCount - 1);
			initMin.Should().BeLessOrEqualTo(heap.Min);
			heap._storage.Should().BeInAscendingOrder();
		}
		[TestMethod]
		public void RemoveAtMiddle_ShouldReturnMiddleElement_DecreaseAmountByOne()
		{
			var heap = new HeapOverList<int>();
			heap.AddRange(new[] { 5, 8, 6, 3, 1, 5, 8, 6, 9, 9, 2 });

			var initMin = heap.Min;
			var initCount = heap.Count;

			var removedItem = heap.RemoveAt(heap.Count / 2);
			Console.WriteLine($"removed item {removedItem}");

			initMin.Should().Be(heap.Min);
			heap.Count.Should().Be(initCount - 1);
			heap._storage.Should().BeInAscendingOrder();
		}
		[TestMethod]
		public void RemoveBy_Even_OnlyOddShouldStay()
		{
			var heap = new HeapOverList<int>();
			heap.AddRange(new[] { 5, 8, 6, 3, 1, 5, 8, 6, 9, 9, 2 });

			var initCount = heap.Count;

			var removedCount = heap.RemoveBy(item => item % 2 == 0);

			removedCount.Should().Be(initCount - heap.Count);
			heap._storage.Should().BeInAscendingOrder();
			heap._storage.Should().Contain(item => item % 2 == 1);
		}
		[TestMethod]
		public void RemoveBy_Odd_OnlyEvenShouldStay()
		{
			var heap = new HeapOverList<int>();
			heap.AddRange(new[] { 5, 8, 6, 3, 1, 5, 8, 6, 9, 9, 2 });

			var initCount = heap.Count;

			var removedCount = heap.RemoveBy(item => item % 2 == 1);

			removedCount.Should().Be(initCount - heap.Count);
			heap._storage.Should().BeInAscendingOrder();
			heap._storage.Should().Contain(item => item % 2 == 0);
		}
	}
}
