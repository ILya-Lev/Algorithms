using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	/// <summary>
	/// a union-find data structure via root rank implementation
	/// </summary>
	public class UnionFind<TData>
	{
		public Dictionary<TData, Node<TData>> Items { get; set; }

		public UnionFind(IEnumerable<TData> items)
		{
			Items = items.Distinct().ToDictionary(item => item, item => new Node<TData>
			{
				Data = item
			});
		}

		/// <summary> returns root of a subtree (part) with this item </summary>
		public Node<TData> Find(TData item)
		{
			if (!Items.ContainsKey(item)) return null;

			var currentNode = Items[item];
			while (currentNode.Parent != null)
				currentNode = currentNode.Parent;
			return currentNode;
		}

		/// <summary> returns new root of fused subtrees (parts) with lhs item and rhs item </summary>
		public Node<TData> Union(TData lhs, TData rhs)
		{
			var lhsRoot = Find(lhs);
			var rhsRoot = Find(rhs);

			if (lhsRoot == null || rhsRoot == null)
				throw new InvalidOperationException("either lhs or rhs item absent in the world");
			if (lhsRoot == rhsRoot)
				return lhsRoot;

			var ranksDifference = lhsRoot.Rank - rhsRoot.Rank;
			if (ranksDifference > 0)
				return FuseParts(lhsRoot, rhsRoot);

			if (ranksDifference < 0)
				return FuseParts(rhsRoot, lhsRoot);

			FuseParts(lhsRoot, rhsRoot);
			lhsRoot.Rank++;
			return lhsRoot;
		}

		private static Node<TData> FuseParts(Node<TData> parentPart, Node<TData> childPart)
		{
			parentPart.ChildNodes.Add(childPart);
			childPart.Parent = parentPart;
			return parentPart;
		}
	}

	public class Node<TData>
	{
		public TData Data { get; set; }
		public int Rank { get; set; }
		public Node<TData> Parent { get; set; }
		public List<Node<TData>> ChildNodes { get; set; } = new List<Node<TData>>();
	}
}
