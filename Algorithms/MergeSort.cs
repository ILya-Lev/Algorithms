using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public static class MergeSort<T> where T : IComparable<T>
	{
		public static T[] SortArray(T[] array)
		{
			return PerformSubSort(array, 0, array.Length);
		}

		private static T[] PerformSubSort(T[] array, int begin, int end)
		{
			if (begin == end - 1)
				return new[] { array[begin] };

			int middle = (end + begin) / 2;
			return PerformMerge(PerformSubSort(array, begin, middle),
								PerformSubSort(array, middle, end)).ToArray();
		}

		private static IEnumerable<T> PerformMerge(T[] left, T[] right)
		{
			int leftIdx = 0, rightIdx = 0;
			while (leftIdx < left.Length && rightIdx < right.Length)
			{
				yield return left[leftIdx].CompareTo(right[rightIdx]) <= 0
					? left[leftIdx++]
					: right[rightIdx++];
			}
			while (leftIdx < left.Length) yield return left[leftIdx++];
			while (rightIdx < right.Length) yield return right[rightIdx++];
		}
	}
}
