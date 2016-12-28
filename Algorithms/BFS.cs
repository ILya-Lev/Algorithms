using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	/// <summary>
	/// implements Breadth First Search algorithm
	/// </summary>
	/// <typeparam name="TData">type of data stored in vertex</typeparam>
	/// <typeparam name="TMetric">type of edge metric, i.e. length (is here due to more generic classes utilization - do not really used in sutu)</typeparam>
	public class BFS<TData, TMetric>
	{
		/// <summary>
		/// is used to avoid the same vertex been explored twice
		/// and to restore the path - contains a vertex and the shortest edge pointing on it
		/// </summary>
		private Dictionary<Vertex<TData, TMetric>, Edge<TData, TMetric>> _seenVertices;

		/// <summary>
		/// for a given graph and a pair of vertices returns a shortest path between them
		/// </summary>
		/// <remarks>
		/// 1. each edge is of the same length
		/// 2. logically edges are not directed (but in current implementation they are)
		/// 3. create a Layer - all vertices have been discovered in a step
		///  (in one edge from some previous vertex)
		///  layer state is managed with a Queue
		/// </remarks>
		/// <param name="graph">graph to search over</param>
		/// <param name="start">start of the path to find</param>
		/// <param name="finish">end of the path to find</param>
		/// <returns>a path from <param name="start"/> to <param name="finish"/></returns>
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
