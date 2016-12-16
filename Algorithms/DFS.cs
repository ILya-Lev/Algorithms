using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public class DFS<TData, TMetric>
	{
		private Dictionary<Vertex<TData, TMetric>, Edge<TData, TMetric>> _seenVertices;

		public List<Vertex<TData, TMetric>> FindPath(Graph<TData, TMetric> graph,
													Vertex<TData, TMetric> start,
													Vertex<TData, TMetric> finish)
		{
			_seenVertices = new Dictionary<Vertex<TData, TMetric>, Edge<TData, TMetric>>();

			if (!FindNode(start, finish))
				return null;

			var path = new List<Vertex<TData, TMetric>>();
			for (var current = finish; current != start; current = _seenVertices[current].Beginning)
			{
				path.Add(current);
			}
			path.Add(start);
			path.Reverse();
			return path;
		}

		private bool FindNode(Vertex<TData, TMetric> start, Vertex<TData, TMetric> finish)
		{
			var descent = new Stack<Vertex<TData, TMetric>>();
			descent.Push(start);
			_seenVertices.Add(start, null);

			while (descent.Count > 0)
			{
				var current = descent.Pop();

				foreach (var edge in current.Edges.Where(e => !_seenVertices.ContainsKey(e.Ending)))
				{
					_seenVertices.Add(edge.Ending, edge);
					if (edge.Ending == finish)
						return true;

					descent.Push(edge.Ending);
				}
			}
			return false;
		}
	}
}
