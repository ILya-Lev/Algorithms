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
		private readonly Dictionary<Vertex<TData, TMetric>, Vertex<TData, TMetric>> _leaderByVertex;
		private readonly Dictionary<Vertex<TData, TMetric>, List<Vertex<TData, TMetric>>> _leaderPopulation;
		private readonly HashSet<Edge<TData, TMetric>> _edges;

		public SpaninTree(List<Vertex<TData, TMetric>> vertices)
		{
			_leaderByVertex = vertices.ToDictionary(v => v, v => v);
			_leaderPopulation
				= vertices.ToDictionary(v => v, v => new List<Vertex<TData, TMetric>> { v });

			_edges = new HashSet<Edge<TData, TMetric>>();
		}

		public void AddEdge(Edge<TData, TMetric> edge)
		{
			//omit (first leader == second leader) check due to cycle formation check
			var firstLeader = _leaderByVertex[edge.Beginning];
			var secondLeader = _leaderByVertex[edge.Ending];

			if (_leaderPopulation[firstLeader].Count >= _leaderPopulation[secondLeader].Count)
			{
				_leaderPopulation[secondLeader].ForEach(v => _leaderByVertex[v] = firstLeader);
				_leaderPopulation[firstLeader].AddRange(_leaderPopulation[secondLeader]);
				_leaderPopulation[secondLeader] = null;
			}
			else
			{
				_leaderPopulation[firstLeader].ForEach(v => _leaderByVertex[v] = secondLeader);
				_leaderPopulation[secondLeader].AddRange(_leaderPopulation[firstLeader]);
				_leaderPopulation[firstLeader] = null;
			}

			_edges.Add(edge);
		}

		public bool FormsCycle(Edge<TData, TMetric> edge)
		{
			//due to the classes constructor - all vertices are always here
			return _leaderByVertex[edge.Beginning] == _leaderByVertex[edge.Ending];
		}

		/// <summary>
		/// as there is no constraint for type argument TMetric to support operator+
		/// I have to stick with int or other hardcoded type
		/// </summary>
		/// <param name="metricConverter">e.g. m => m  for Edge&lt;TData, int&gt;</param>
		/// <returns>total metric of spanning tree - sum of individual metric of each edge</returns>
		public int TotalCost(Func<TMetric, int> metricConverter)
			=> _edges.Sum(e => metricConverter(e.Metric));
	}

	public static class KruskalsMst
	{
		/// <summary>
		/// implementation via union-find data structure - spanning tree above
		/// </summary>
		public static SpaninTree<TData, TMetric> GenerateSpaninTree<TData, TMetric>(
															Graph<TData, TMetric> graph)
		{
			var edges = graph.Vertices.SelectMany(v => v.Edges).OrderBy(e => e.Metric).ToList();

			var tree = new SpaninTree<TData, TMetric>(graph.Vertices);

			foreach (var edge in edges)
			{
				if (!tree.FormsCycle(edge))
					tree.AddEdge(edge);
			}
			return tree;
		}
	}
}
