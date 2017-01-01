using System.Collections;
using System.Collections.Generic;

namespace Algorithms
{
	public class LinkedNode<T>
	{
		public T Data { get; set; }
		public LinkedNode<T> Next { get; set; }
	}

	public class MyLinkedList<T> : IEnumerable<T>
	{
		public LinkedNode<T> Root { get; set; }

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

		public void AddItem(T data)
		{
			if (Root == null)
			{
				Root = new LinkedNode<T> { Data = data };
				return;
			}

			var current = Root;
			while (current.Next != null)
			{
				current = current.Next;
			}
			current.Next = new LinkedNode<T> { Data = data };
		}
	}
}
