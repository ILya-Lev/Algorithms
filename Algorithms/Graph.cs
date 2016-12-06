using System.Collections.Generic;

namespace Algorithms
{
	public class Graph<TData, TMetrix>
	{
		public List<Vertex<TData, TMetrix>> Vertices { get; } = new List<Vertex<TData, TMetrix>>();
	}

	public class Vertex<TData, TMetrix>
	{
		public TData Value { get; set; }
		public List<Edge<TData, TMetrix>> Edges { get; } = new List<Edge<TData, TMetrix>>();
	}

	public class Edge<TData, TMetrix>
	{
		public Vertex<TData, TMetrix> Beginning { get; }
		public Vertex<TData, TMetrix> Ending { get; }
		public TMetrix Metrix { get; }

		public Edge(Vertex<TData, TMetrix> beginning, Vertex<TData, TMetrix> ending, TMetrix metrix)
		{
			Beginning = beginning;
			Ending = ending;
			Metrix = metrix;
		}
	}
}
