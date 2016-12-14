using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="d">data type for vertex value</typeparam>
	/// <typeparam name="m">data type for edge metric (e.g. length)</typeparam>
	public class StronglyConnectedComponents<d, m>
	{
		private HashSet<Vertex<d, m>> _reverseSeenVertices;
		private Dictionary<Vertex<d, m>, int> _finishingTimes;
		private int _finishingTime;
		private Dictionary<Vertex<d, m>, Vertex<d, m>> _vertexByLeader;
		private HashSet<Vertex<d, m>> _seenVertices;

		public IList<IGrouping<Vertex<d, m>, Vertex<d, m>>> Discover(Graph<d, m> graph)
		{
			//reverse initial graph
			var edges = graph.Vertices.SelectMany(v => v.Edges)
				.Select(e => new Edge<d, m>(e.Ending, e.Beginning, e.Metrix))
				.GroupBy(e => e.Beginning)
				.ToDictionary(g => g.Key, g => g);

			//evaluate finishing times
			_reverseSeenVertices = new HashSet<Vertex<d, m>>();
			_finishingTimes = new Dictionary<Vertex<d, m>, int>();
			_finishingTime = 0;
			foreach (var vertex in graph.Vertices)
			{
				if (_reverseSeenVertices.Contains(vertex)) continue;

				EvaluateFinishingTimes(vertex, edges);

				if (_reverseSeenVertices.Count == graph.Vertices.Count) break;
			}

			var verticesByTimes = _finishingTimes.Select(pair => new { pair.Key, pair.Value })
												 .OrderByDescending(item => item.Value)
												 .Select(item => item.Key)
												 .ToList();

			//evaluate leading vertices
			_seenVertices = new HashSet<Vertex<d, m>>();
			_vertexByLeader = new Dictionary<Vertex<d, m>, Vertex<d, m>>();
			foreach (var leader in verticesByTimes)
			{
				if (_seenVertices.Contains(leader)) continue;

				AssignLeaders(leader);

				if (_seenVertices.Count == graph.Vertices.Count) break;
			}

			//compose result
			return _vertexByLeader.Select(pair => new { Vertex = pair.Key, Leader = pair.Value })
								.GroupBy(item => item.Leader, item => item.Vertex)
								.ToList();
		}

		/// <summary>
		/// runs DFS over reverted Graph - each 'sink' vertex will have index = finishing time
		/// </summary>
		/// <param name="vertex">seed for the algorithm invocation </param>
		/// <param name="edges">reverted edges of initial graph</param>
		private void EvaluateFinishingTimes(Vertex<d, m> vertex,
		Dictionary<Vertex<d, m>, IGrouping<Vertex<d, m>, Edge<d, m>>> edges)
		{
			var path = new Stack<Vertex<d, m>>();
			path.Push(vertex);
			while (path.Count > 0)
			{
				var current = path.Peek();
				_reverseSeenVertices.Add(current);

				var currentEdges = !edges.ContainsKey(current)
					? new List<Edge<d, m>>()
					: edges[current].Where(e => !_reverseSeenVertices.Contains(e.Ending))
									 .ToList();
				if (currentEdges.Count == 0)
				{
					path.Pop();
					if (!_finishingTimes.ContainsKey(current))
						_finishingTimes.Add(current, _finishingTime++);
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
		private void AssignLeaders(Vertex<d, m> leader)
		{
			var path = new Stack<Vertex<d, m>>();
			path.Push(leader);

			while (path.Count > 0)
			{
				var current = path.Peek();
				_seenVertices.Add(current);

				var currentEdges = current.Edges.Where(e => !_seenVertices.Contains(e.Ending))
												.ToList();
				if (currentEdges.Count == 0)
				{
					path.Pop();
					if (!_vertexByLeader.ContainsKey(current))
						_vertexByLeader.Add(current, leader);
					continue;
				}
				currentEdges.ForEach(e => path.Push(e.Ending));
			}
		}
	}
}
