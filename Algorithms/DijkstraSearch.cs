using MoreLinq;
using System.Collections.Generic;

namespace Algorithms
{
	public static class DijkstraSearch
	{
		public static List<Vertex<TData, TMetric>> FindPath<TData, TMetric>
															(Graph<TData, TMetric> graph,
															Vertex<TData, TMetric> start,
															Vertex<TData, TMetric> finish)
		{
			var frontier = new Frontier<TData, TMetric>();
			frontier.AddVertex(start, null);

			while (!frontier.Contains(finish))
			{
				var shortestEdge = frontier.ShortestEdge();
				frontier.AddVertex(shortestEdge.Ending, shortestEdge);

				if (!frontier.HasEdges)
					break;
			}

			//check whether finish node has been found
			if (!frontier.Contains(finish)) return null;

			var path = new List<Vertex<TData, TMetric>>();
			for (var current = finish; current != null;)
			{
				path.Add(current);
				var shortestEdge = frontier.EdgeForVertex(current);
				current = shortestEdge?.Beginning;
			}
			path.Reverse();
			return path;
		}

		private class Frontier<TData, TMetric>
		{
			private Dictionary<Vertex<TData, TMetric>, Edge<TData, TMetric>> Vertices { get; }
						= new Dictionary<Vertex<TData, TMetric>, Edge<TData, TMetric>>();

			private List<Edge<TData, TMetric>> Edges { get; }
						= new List<Edge<TData, TMetric>>();

			public bool HasEdges => Edges.Count > 0;

			public void AddVertex(Vertex<TData, TMetric> vertex, Edge<TData, TMetric> edge)
			{
				Vertices.Add(vertex, edge);
				Edges.AddRange(vertex.Edges);

				Edges.RemoveAll(e => Vertices.ContainsKey(e.Ending));
			}

			public bool Contains(Vertex<TData, TMetric> vertex)
			{
				return Vertices.ContainsKey(vertex);
			}

			public Edge<TData, TMetric> ShortestEdge()
			{
				return Edges.MinBy(e => e.Metric);
			}

			public Edge<TData, TMetric> EdgeForVertex(Vertex<TData, TMetric> vertex)
			{
				return Vertices[vertex];
			}
		}
	}
}
