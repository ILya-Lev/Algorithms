using MoreLinq;
using System.Collections.Generic;

namespace Algorithms
{
	public static class DijkstraSearch
	{
		public static List<Vertex<TData, TMetrix>> FindPath<TData, TMetrix>
															(Graph<TData, TMetrix> graph,
															Vertex<TData, TMetrix> start,
															Vertex<TData, TMetrix> finish)
		{
			var frontier = new Frontier<TData, TMetrix>();
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

			var path = new List<Vertex<TData, TMetrix>>();
			for (var current = finish; current != null;)
			{
				path.Add(current);
				var shortestEdge = frontier.EdgeForVertex(current);
				current = shortestEdge?.Beginning;
			}
			path.Reverse();
			return path;
		}

		private class Frontier<TData, TMetrix>
		{
			private Dictionary<Vertex<TData, TMetrix>, Edge<TData, TMetrix>> Vertices { get; }
						= new Dictionary<Vertex<TData, TMetrix>, Edge<TData, TMetrix>>();

			private List<Edge<TData, TMetrix>> Edges { get; }
						= new List<Edge<TData, TMetrix>>();

			public bool HasEdges => Edges.Count > 0;

			public void AddVertex(Vertex<TData, TMetrix> vertex, Edge<TData, TMetrix> edge)
			{
				Vertices.Add(vertex, edge);
				Edges.AddRange(vertex.Edges);

				Edges.RemoveAll(e => Vertices.ContainsKey(e.Ending));
			}

			public bool Contains(Vertex<TData, TMetrix> vertex)
			{
				return Vertices.ContainsKey(vertex);
			}

			public Edge<TData, TMetrix> ShortestEdge()
			{
				return Edges.MinBy(e => e.Metrix);
			}

			public Edge<TData, TMetrix> EdgeForVertex(Vertex<TData, TMetrix> vertex)
			{
				return Vertices[vertex];
			}
		}
	}
}
