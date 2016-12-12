using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public class StronglyConnectedComponents<TData, TMetrix>
	{
		private Dictionary<Vertex<TData, TMetrix>, int> _reverseSeenVertices;
		private int _finishingTime;
		private Dictionary<Vertex<TData, TMetrix>, Vertex<TData, TMetrix>> _seenVertices;

		public IList<IGrouping<Vertex<TData, TMetrix>, Vertex<TData, TMetrix>>> Discover(Graph<TData, TMetrix> graph)
		{
			//reverse initial graph
			var edges = graph.Vertices.SelectMany(v => v.Edges)
				.Select(e => new Edge<TData, TMetrix>(e.Ending, e.Beginning, e.Metrix))
				.ToLookup(e => e.Beginning);

			//evaluate finishing times
			_reverseSeenVertices = new Dictionary<Vertex<TData, TMetrix>, int>();
			_finishingTime = 0;
			foreach (var vertex in graph.Vertices)
			{
				if (_reverseSeenVertices.ContainsKey(vertex)) continue;

				EvaluateFinishingTimes(vertex, edges);
			}

			var verticesByTimes = _reverseSeenVertices.Select(pair => new { pair.Key, pair.Value })
													 .OrderBy(item => item.Value)
													 .Select(item => item.Key)
													 .ToList();

			//evaluate leading vertices
			_seenVertices = new Dictionary<Vertex<TData, TMetrix>, Vertex<TData, TMetrix>>();
			foreach (var leader in verticesByTimes)
			{
				if (_seenVertices.ContainsKey(leader)) continue;

				AssignLeaders(leader);
			}

			//compose result
			return _seenVertices.Select(pair => new { Vertex = pair.Key, Leader = pair.Value })
							   .GroupBy(item => item.Leader, item => item.Vertex)
							   .ToList();
		}

		/// <summary>
		/// runs DFS over reverted Graph - each 'sink' vertex will have index = finishing time
		/// </summary>
		/// <param name="vertex">seed for the algorithm invocation </param>
		/// <param name="edges">reverted edges of initial graph</param>
		private void EvaluateFinishingTimes(Vertex<TData, TMetrix> vertex,
											ILookup<Vertex<TData, TMetrix>, Edge<TData, TMetrix>> edges)
		{
			var path = new Stack<Vertex<TData, TMetrix>>();
			path.Push(vertex);
			while (path.Count > 0)
			{
				var current = path.Peek();
				var currentEdges = edges[current].Where(e => !_reverseSeenVertices.ContainsKey(e.Ending))
												 .ToList();
				if (currentEdges.Count == 0)
				{
					path.Pop();
					_reverseSeenVertices.Add(current, _finishingTime++);
					continue;
				}
				currentEdges.ForEach(e => path.Push(e.Ending));
			}
		}

		/// <summary>
		/// DFS subroutine to assign leader vertex for each Strongly Connected Component (SCC)
		/// leader - node for which DFS invocation in current context (_seenVertices) will lead
		/// to discovering only this SCC
		/// </summary>
		/// <param name="leader">vertex with next finishing time comparing to previous mehod call</param>
		private void AssignLeaders(Vertex<TData, TMetrix> leader)
		{
			var path = new Stack<Vertex<TData, TMetrix>>();
			path.Push(leader);

			while (path.Count > 0)
			{
				var current = path.Peek();
				var currentEdges = current.Edges.Where(e => !_seenVertices.ContainsKey(e.Ending))
					.ToList();
				if (currentEdges.Count == 0)
				{
					path.Pop();
					_seenVertices.Add(current, leader);
					continue;
				}
				currentEdges.ForEach(e => path.Push(e.Ending));
			}
		}
	}
}
