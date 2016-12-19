using System;
using System.Collections.Generic;

namespace Algorithms
{
	public class HeapOverList<T> where T : IComparable<T>
	{
		internal readonly List<T> _storage = new List<T>();

		public T Min => _storage.Count > 0 ? _storage[0] : default(T);
		public int Count => _storage.Count;

		public void AddRange(IEnumerable<T> sequence)
		{
			foreach (var item in sequence)
				Add(item);
		}

		public void Add(T item) => FindRightPlace(item, 0, _storage.Count - 1);

		private void FindRightPlace(T item, int begin, int end)
		{
			if (begin > end)
			{
				_storage.Insert(begin, item);
				return;
			}

			var middle = (end + begin) / 2;
			var comparison = item?.CompareTo(_storage[middle]);
			if (comparison == 0)
			{
				_storage.Insert(middle, item);
				return;
			}

			if (comparison < 0)
				FindRightPlace(item, begin, middle - 1);
			else if (comparison > 0)
				FindRightPlace(item, middle + 1, end);
		}

		public T RemoveMin() => RemoveAt(0);

		public T RemoveAt(int index)
		{
			if (index < 0 || index >= _storage.Count)
				throw new ArgumentOutOfRangeException(
					$"Heap contains only {_storage.Count} elements. Cannot remove {index} item.");

			var min = _storage[index];
			_storage.RemoveAt(index);
			return min;
		}

		public int RemoveBy(Predicate<T> predicate) => _storage.RemoveAll(predicate);
	}
}
