using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public class BFS<TData, TMetric>
	{
		private Dictionary<Vertex<TData, TMetric>, Edge<TData, TMetric>> _seenVertices;

		public List<Vertex<TData, TMetric>> FindPath(Graph<TData, TMetric> graph,
													Vertex<TData, TMetric> start,
													Vertex<TData, TMetric> finish)
		{
			_seenVertices = new Dictionary<Vertex<TData, TMetric>, Edge<TData, TMetric>>
			{
				[start] = null
			};

			if (!FindVertex(start, finish))
				return null;

			var path = new List<Vertex<TData, TMetric>> { finish };
			path.AddRange(TraversePath(start, finish));
			path.Reverse();
			return path;
		}

		private bool FindVertex(Vertex<TData, TMetric> start, Vertex<TData, TMetric> finish)
		{
			var layer = new Queue<Vertex<TData, TMetric>>();
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

		private IEnumerable<Vertex<TData, TMetric>> TraversePath(Vertex<TData, TMetric> start,
																 Vertex<TData, TMetric> finish)
		{
			for (var current = finish; current != start;)
			{
				current = _seenVertices[current].Beginning;
				yield return current;
			}
		}
	}
}
