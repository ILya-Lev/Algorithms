using Algorithms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class QuickSortUnitTests
	{
		[TestMethod]
		public void DescendingToAscending_Sort()
		{
			var data = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

			var sorted = data.QuickSort();

			sorted.Should().BeInAscendingOrder();
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException), "there should be an exception!")]
		public void NullSource_ShouldThrowException()
		{
			int[] data = null;

			var sorted = data.QuickSort();
		}

		public void RandomDataSorter(int limit)
		{
			var rand = new Random(DateTime.UtcNow.Millisecond);
			var data = Enumerable.Range(1, limit).ToList();
			for (int i = 0; i < limit; i++)
			{
				var idx = rand.Next(0, limit - 1);
				data[i] = data[i] ^ data[idx];
				data[idx] = data[i] ^ data[idx];
				data[i] = data[i] ^ data[idx];
			}

			var sorted = data.QuickSort();

			sorted.Should().BeInAscendingOrder();
		}
		[TestMethod]
		public void Random10Data_ShouldSort()
		{
			RandomDataSorter(10);
		}
		[TestMethod]
		public void Random100Data_ShouldSort()
		{
			RandomDataSorter(100);
		}
		[TestMethod]
		public void Random1000Data_ShouldSort()
		{
			RandomDataSorter(1000);
		}
		[TestMethod]
		public void Random10_7Data_ShouldSort()
		{
			RandomDataSorter(10000000);
		}
		[TestMethod]
		public void Random10_7Data_LinqSort()
		{
			var limit = 10000000;
			var rand = new Random(DateTime.UtcNow.Millisecond);
			var data = Enumerable.Range(1, limit).ToList();
			for (int i = 0; i < limit; i++)
			{
				var idx = rand.Next(0, limit - 1);
				data[i] = data[i] ^ data[idx];
				data[idx] = data[i] ^ data[idx];
				data[i] = data[i] ^ data[idx];
			}

			var sorted = data.AsParallel().OrderBy(item => item);

			sorted.Should().BeInAscendingOrder();
		}
	}
}
