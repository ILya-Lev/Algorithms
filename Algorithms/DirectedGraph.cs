using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public class DirectedGraph<TData>
	{
		public HashSet<Vertex<TData, int>> Vertices = new HashSet<Vertex<TData, int>>();

		public Dictionary<Vertex<TData, int>, IGrouping<Vertex<TData, int>, Edge<TData, int>>> OutgoingEdges
			= new Dictionary<Vertex<TData, int>, IGrouping<Vertex<TData, int>, Edge<TData, int>>>();

		public Dictionary<Vertex<TData, int>, IGrouping<Vertex<TData, int>, Edge<TData, int>>> IncomingEdges
			= new Dictionary<Vertex<TData, int>, IGrouping<Vertex<TData, int>, Edge<TData, int>>>();
	}
}