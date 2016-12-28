using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	/// <summary>
	/// implements algorithm of defining how many blobs are there in a graph
	/// if graph is completely connected - there is only one blob
	/// </summary>
	/// <typeparam name="TData"></typeparam>
	/// <typeparam name="TMetric"></typeparam>
	public class ConnectedComponentsBfs<TData, TMetric>
	{
		private HashSet<Vertex<TData, TMetric>> _seenVertices;

		public List<Graph<TData, TMetric>> Blobs(Graph<TData, TMetric> graph)
		{
			_seenVertices = new HashSet<Vertex<TData, TMetric>>();

			var blobs = new List<Graph<TData, TMetric>>();
			foreach (var vertex in graph.Vertices)
			{
				if (_seenVertices.Contains(vertex)) continue;

				blobs.Add(ComposeBlob(vertex));

				if (graph.Vertices.Count == _seenVertices.Count) break;
			}

			return blobs;
		}

		private Graph<TData, TMetric> ComposeBlob(Vertex<TData, TMetric> seedVertex)
		{
			var blob = new Graph<TData, TMetric>();
			var layer = new Queue<Vertex<TData, TMetric>>();
			layer.Enqueue(seedVertex);

			while (layer.Count > 0)
			{
				var current = layer.Dequeue();
				if (_seenVertices.Contains(current)) continue;

				blob.Vertices.Add(current);
				_seenVertices.Add(current);

				foreach (var edge in current.Edges.Where(e => !_seenVertices.Contains(e.Ending)))
				{
					layer.Enqueue(edge.Ending);
				}
			}

			return blob;
		}
	}
}
