using System;
using System.Diagnostics;

namespace Algorithms
{
	public static class Program
	{
		static void Main(string[] args)
		{
			int[] unsorted = { 3, 9, 2, 1, 8, 5, 8, 3, 2, 7, 1, 9, 6, 0 };
			Print(unsorted);

			var sorted = MergeSort<int>.SortArray(unsorted);
			Print(sorted);
			Print(unsorted);
		}

		private static void Print<T>(T[] array)
		{
			foreach (var item in array)
			{
				Console.Write(item.ToString() + " ");
				Debug.Write(item.ToString() + " ");
			}

			Console.WriteLine();
			Debug.WriteLine("\n");
		}
	}
}
