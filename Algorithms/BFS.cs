using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public class BFS<TData, TMetrix>
	{
		private Dictionary<Vertex<TData, TMetrix>, Edge<TData, TMetrix>> _seenVertices;

		public List<Vertex<TData, TMetrix>> FindPath(Graph<TData, TMetrix> graph,
													Vertex<TData, TMetrix> start,
													Vertex<TData, TMetrix> finish)
		{
			_seenVertices = new Dictionary<Vertex<TData, TMetrix>, Edge<TData, TMetrix>>
			{
				[start] = null
			};

			if (!FindVertex(start, finish))
				return null;

			var path = new List<Vertex<TData, TMetrix>> { finish };
			path.AddRange(TraversePath(start, finish));
			path.Reverse();
			return path;
		}

		private bool FindVertex(Vertex<TData, TMetrix> start, Vertex<TData, TMetrix> finish)
		{
			var layer = new Queue<Vertex<TData, TMetrix>>();
			layer.Enqueue(start);
			while (layer.Count > 0)
			{
				var current = layer.Dequeue();
				foreach (var edge in current.Edges.Where(e => !_seenVertices.ContainsKey(e.Ending)))
				{
					_seenVertices.Add(edge.Ending, edge);
					if (edge.Ending == finish)
						return true;
					layer.Enqueue(edge.Ending);
				}
			}
			return false;
		}

		private IEnumerable<Vertex<TData, TMetrix>> TraversePath(Vertex<TData, TMetrix> start,
																 Vertex<TData, TMetrix> finish)
		{
			for (var current = finish; current != start;)
			{
				current = _seenVertices[current].Beginning;
				yield return current;
			}
		}
	}
}
