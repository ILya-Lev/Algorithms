using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public class Node<TData, TMetric>
	{
		public TData Data { get; set; }
		public TMetric DistanceFromParent { get; set; }
		public Node<TData, TMetric> Parent { get; set; }
		public List<Node<TData, TMetric>> ChildNodes { get; } = new List<Node<TData, TMetric>>();
	}

	public class SpaninTree<TData, TMetric>
	{
		public Node<TData, TMetric> Root { get; set; }

		public void AddEdge(Edge<TData, TMetric> edge)
		{
			if (Root == null)
			{
				Root = new Node<TData, TMetric> { Data = edge.Beginning.Value };
				var child = new Node<TData, TMetric>
				{
					Data = edge.Ending.Value,
					DistanceFromParent = edge.Metric,
					Parent = Root
				};

				Root.ChildNodes.Add(child);
				return;
			}

			var parent = FindParent(edge.Beginning);
			if (parent != null)
			{
				var child = new Node<TData, TMetric>
				{
					Data = edge.Beginning.Value,
					DistanceFromParent = edge.Metric,
					Parent = parent
				};
				parent.ChildNodes.Add(child);
				return;
			}

			parent = FindParent(edge.Ending);
			if (parent != null)
			{
				var child = new Node<TData, TMetric>
				{
					Data = edge.Ending.Value,
					DistanceFromParent = edge.Metric,
					Parent = parent
				};
				parent.ChildNodes.Add(child);
				return;
			}

			throw new ArgumentOutOfRangeException($"Edge's endings are not in the tree!");
		}

		private Node<TData, TMetric> FindParent(Vertex<TData, TMetric> vertex)
		{
			if (Root == null) return null;

			var layer = new Queue<Node<TData, TMetric>>();
			layer.Enqueue(Root);
			while (layer.Count > 0)
			{
				var current = layer.Dequeue();
				if (current.Data.Equals(vertex.Value))
					return current;
				current.ChildNodes.ForEach(c => layer.Enqueue(c));
			}
			return null;
		}

		public bool FormsCycle(Edge<TData, TMetric> edge)
		{
			return FindParent(edge.Beginning) != null && FindParent(edge.Ending) != null;
		}
	}

	public class KruskalsMst<TData, TMetric>
	{
		/// <summary>
		/// not correct implementation due to
		/// my spanning tree is connected
		/// but the algorithm could produce disconnected graphs while is applied
		/// </summary>
		/// <param name="graph"></param>
		/// <returns></returns>
		public SpaninTree<TData, TMetric> GenerateSpaninTree(Graph<TData, TMetric> graph)
		{
			var edges = graph.Vertices.SelectMany(v => v.Edges).OrderBy(e => e.Metric).ToList();

			var tree = new SpaninTree<TData, TMetric>();

			foreach (var edge in edges)
			{
				if (!tree.FormsCycle(edge))
					tree.AddEdge(edge);
			}
			return tree;
		}
	}
}
