using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<int[]> Buffer(this IEnumerable<int> source, int size)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));
			if (size <= 0)
				throw new ArgumentOutOfRangeException(nameof(size), size,
											"Buffer size should be larger than zero");

			return BufferImpl(source, size);
		}

		private static IEnumerable<int[]> BufferImpl(IEnumerable<int> source, int size)
		{
			var buffer = new int[size];
			var current = 0;

			using (var iterator = source.GetEnumerator())
			{
				while (iterator.MoveNext())
				{
					buffer[current % size] = iterator.Current;
					if (current % size == size - 1)
						yield return buffer;
					current++;
				}
			}

			if (current % size != 0)
				yield return buffer.Take(current % size).ToArray();
		}
	}
}
