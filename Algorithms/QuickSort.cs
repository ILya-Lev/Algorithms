using System;
using System.Collections.Generic;

namespace Algorithms
{
	public static class QuickSortUtility
	{
		public static IList<T> QuickSort<T>(this IList<T> source) where T : IComparable<T>
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			var sequence = new T[source.Count];
			source.CopyTo(sequence, 0);

			QuickSortImpl<T>.SortAroundPivot(sequence, 0, sequence.Length);
			return sequence;
		}

		private static class QuickSortImpl<T> where T : IComparable<T>
		{
			private static readonly Random _rand = new Random(DateTime.UtcNow.Millisecond);

			public static void SortAroundPivot(IList<T> source, int begin, int end)
			{
				if (begin >= end) return;

				if (begin < end - 1)
					Swap(source, begin, ChoosePivot(begin, end - 1));
				var pivotIdx = begin;

				var start = begin + 1;
				var finish = end;
				for (int i = start; i < finish; i++)
				{
					var compare = source[pivotIdx].CompareTo(source[i]);
					if (compare < 0)
					{
						Swap(source, i, finish - 1);
						finish--;
						i--;    // we do not need to increase i in this iteration
					}
					else if (compare > 0)
					{
						Swap(source, i, pivotIdx);
						pivotIdx++;
					}
				}

				SortAroundPivot(source, begin, pivotIdx);
				SortAroundPivot(source, pivotIdx + 1, end);
			}

			private static int ChoosePivot(int begin, int end) => _rand.Next(begin, end - 1);

			private static void Swap(IList<T> source, int lhs, int rhs)
			{
				var tmp = source[lhs];
				source[lhs] = source[rhs];
				source[rhs] = tmp;
			}
		}
	}
}
