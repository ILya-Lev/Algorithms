using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public class DFS<TData, TMetrix>
	{
		private Dictionary<Vertex<TData, TMetrix>, Edge<TData, TMetrix>> _seenVertices;

		public List<Vertex<TData, TMetrix>> FindPath(Graph<TData, TMetrix> graph,
													Vertex<TData, TMetrix> start,
													Vertex<TData, TMetrix> finish)
		{
			_seenVertices = new Dictionary<Vertex<TData, TMetrix>, Edge<TData, TMetrix>>();

			if (!FindNode(start, finish))
				return null;

			var path = new List<Vertex<TData, TMetrix>>();
			for (var current = finish; current != start; current = _seenVertices[current].Beginning)
			{
				path.Add(current);
			}
			path.Add(start);
			path.Reverse();
			return path;
		}

		private bool FindNode(Vertex<TData, TMetrix> start, Vertex<TData, TMetrix> finish)
		{
			var descent = new Stack<Vertex<TData, TMetrix>>();
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
