using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
	public class ConnectedComponentsBfs<TData, TMetrix>
	{
		private HashSet<Vertex<TData, TMetrix>> _seenVertices;

		public List<Graph<TData, TMetrix>> Blobs(Graph<TData, TMetrix> graph)
		{
			_seenVertices = new HashSet<Vertex<TData, TMetrix>>();

			var blobs = new List<Graph<TData, TMetrix>>();
			foreach (var vertex in graph.Vertices)
			{
				if (_seenVertices.Contains(vertex)) continue;

				blobs.Add(ComposeBlob(vertex));

				if (graph.Vertices.Count == _seenVertices.Count) break;
			}

			return blobs;
		}

		private Graph<TData, TMetrix> ComposeBlob(Vertex<TData, TMetrix> seedVertex)
		{
			var blob = new Graph<TData, TMetrix>();
			var layer = new Queue<Vertex<TData, TMetrix>>();
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
