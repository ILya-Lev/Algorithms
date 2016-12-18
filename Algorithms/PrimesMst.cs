using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	/// <summary>
	/// the class implements Prime's algorithms of defining Minimal Spanning Tree (MST)
	/// in particular our goal is to come up with total costs of the tree
	/// i.e. total sum of edges in the tree
	/// </summary>
	public static class PrimesMst
	{
		public static long TotalCost<TData> (this DirectedGraph<TData> graph)
		{
			var rand = new Random(DateTime.UtcNow.Millisecond);
			var pivotVertex = graph.Vertices.ElementAt(rand.Next(0, graph.Vertices.Count - 1));

			var totalCost = 0L;

			var frontier = new Frontier<TData>(graph);
			frontier.AddVertex(pivotVertex);
			while (frontier.SeenSoFar() < graph.Vertices.Count)
			{
				var minOutgoingEdge = frontier.MinEdge();
				totalCost += minOutgoingEdge.Metric;

				var nextVertex = frontier.ExternalVertex(minOutgoingEdge);
				frontier.AddVertex(nextVertex);
			}

			return totalCost;
		}

		private class Frontier<TData>
		{
			private readonly HashSet<Vertex<TData, int>> _vertices;
			private readonly HashSet<Edge<TData, int>> _edges;

			private readonly DirectedGraph<TData> _graph;

			public Frontier (DirectedGraph<TData> graph)
			{
				_graph = graph;
				_vertices = new HashSet<Vertex<TData, int>>();
				_edges = new HashSet<Edge<TData, int>>();
			}

			public Edge<TData, int> MinEdge () => _edges.MinBy(e => e.Metric);

			public void AddVertex (Vertex<TData, int> vertex)
			{
				_vertices.Add(vertex);

				if (_graph.IncomingEdges.ContainsKey(vertex))
					_edges.UnionWith(_graph.IncomingEdges[vertex]);
				if (_graph.OutgoingEdges.ContainsKey(vertex))
					_edges.UnionWith(_graph.OutgoingEdges[vertex]);

				_edges.RemoveWhere(IsInternalEdge);
			}

			private bool IsInternalEdge (Edge<TData, int> edge)
				=> _vertices.Contains(edge.Ending) && _vertices.Contains(edge.Beginning);

			public Vertex<TData, int> ExternalVertex (Edge<TData, int> outgoingEdge)
				=> _vertices.Contains(outgoingEdge.Beginning)
				? outgoingEdge.Ending
				: outgoingEdge.Beginning;

			public int SeenSoFar () => _vertices.Count;
		}
	}
}
