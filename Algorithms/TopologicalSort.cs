using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public class TopologicalSort<TData, TMetric>
	{
		private Dictionary<Vertex<TData, TMetric>, int> _seenVertices;
		private int _number;

		public List<Vertex<TData, TMetric>> OrderedVertices(Graph<TData, TMetric> graph)
		{
			_seenVertices = new Dictionary<Vertex<TData, TMetric>, int>();
			_number = graph.Vertices.Count;

			foreach (var vertex in graph.Vertices)
			{
				if (_seenVertices.ContainsKey(vertex)) continue;

				TraverseFromSeed(vertex);
			}

			return _seenVertices.Select(pair => new { pair.Key, pair.Value })
								.OrderBy(item => item.Value)
								.Select(item => item.Key)
								.ToList();
		}

		private void TraverseFromSeed(Vertex<TData, TMetric> seed)
		{
			var path = new Stack<Vertex<TData, TMetric>>();
			path.Push(seed);

			var preventInfinitLoop = _number;

			while (path.Count > 0 && preventInfinitLoop >= 0)
			{
				var current = path.Peek();
				var edges = current.Edges.Where(e => !_seenVertices.ContainsKey(e.Ending)).ToList();

				if (edges.Count == 0) //it's a sink vertex in frame of current context
				{
					_seenVertices.Add(current, _number--);
					path.Pop();
				}
				else
				{
					edges.ForEach(e => path.Push(e.Ending));
					preventInfinitLoop--;
				}
			}
		}
	}
}
