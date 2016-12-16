using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithms
{
	public class Graph<TData, TMetric>
	{
		public List<Vertex<TData, TMetric>> Vertices { get; } = new List<Vertex<TData, TMetric>>();
	}

	[DebuggerDisplay("value = {Value}; edges amount = {Edges.Count}")]
	public class Vertex<TData, TMetric>
	{
		public TData Value { get; set; }
		public List<Edge<TData, TMetric>> Edges { get; } = new List<Edge<TData, TMetric>>();
	}

	public class Edge<TData, TMetric>
	{
		public Vertex<TData, TMetric> Beginning { get; }
		public Vertex<TData, TMetric> Ending { get; }
		public TMetric Metric { get; }

		public Edge(Vertex<TData, TMetric> beginning, Vertex<TData, TMetric> ending, TMetric metric)
		{
			Beginning = beginning;
			Ending = ending;
			Metric = metric;
		}
	}
}
