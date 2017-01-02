using System.Collections;
using System.Collections.Generic;

namespace Algorithms
{
	public class LinkedNode<T>
	{
		public T Data { get; set; }
		public LinkedNode<T> Next { get; set; }
	}

	public class MyLinkedList<T> : ICollection<T>, IEnumerable<T>
	{
		public LinkedNode<T> Root { get; set; }
		public int Count { get; private set; }
		public bool IsReadOnly { get; } = false;

		public static MyLinkedList<T> ReverseEven(MyLinkedList<T> source)
		{
			var evenNodes = new Stack<LinkedNode<T>>();
			var current = source.Root;
			while (current != null)
			{
				var next = current.Next;
				if (next != null)
					evenNodes.Push(next);
				current = next?.Next;
			}

			if (evenNodes.Count == 0) return null;

			var newRoot = evenNodes.Peek();
			while (evenNodes.Count != 0)
			{
				current = evenNodes.Pop();
				current.Next = evenNodes.Count != 0 ? evenNodes.Peek() : null;
			}

			return new MyLinkedList<T> { Root = newRoot };
		}

		public static MyLinkedList<T> ReverseEvenInPlace(MyLinkedList<T> source)
		{
			var even = source.Root.Next;
			var odd = even.Next;
			even.Next = null;
			while (odd?.Next != null)
			{
				var tmp = even;
				even = odd.Next;
				odd = even.Next;
				even.Next = tmp;
			}
			return new MyLinkedList<T> { Root = even };
		}

		public IEnumerator<T> GetEnumerator()
		{
			var current = Root;
			while (current != null)
			{
				yield return current.Data;
				current = current.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			if (Root == null)
			{
				Root = new LinkedNode<T> { Data = item };
				Count = 1;
				return;
			}

			var current = Root;
			while (current.Next != null)
			{
				current = current.Next;
			}
			current.Next = new LinkedNode<T> { Data = item };
			Count++;
		}

		public void Clear()
		{
			Root = null; //utilize GC yohoo!
			Count = 0;
		}

		public bool Contains(T item)
		{
			for (var current = Root; current != null; current = current.Next)
			{
				if (current.Data.Equals(item)) return true;
			}
			return false;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new System.NotImplementedException();
		}

		public bool Remove(T item)
		{
			var removed = false;
			var predecessor = Root;
			var current = Root;
			while (current != null)
			{
				if (current.Data.Equals(item))
				{
					current = current.Next;
					predecessor.Next = current;

					removed = true;
					Count--;

					continue;
				}
				if (predecessor != current)
					predecessor = predecessor.Next;
				current = current.Next;
			}
			return removed;
		}
	}
}
