using Algorithms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class UnorderedConnectivityUnitTests
	{
		private Graph<char, int> _graph;

		[TestInitialize]
		public void Initialize()
		{
			_graph = new Graph<char, int>();
			var vertices = new Dictionary<char, Vertex<char, int>>
			{
				['A'] = new Vertex<char, int> { Value = 'A' },
				['B'] = new Vertex<char, int> { Value = 'B' },
				['C'] = new Vertex<char, int> { Value = 'C' },
				['D'] = new Vertex<char, int> { Value = 'D' },
				['E'] = new Vertex<char, int> { Value = 'E' },
				['F'] = new Vertex<char, int> { Value = 'F' },
				['G'] = new Vertex<char, int> { Value = 'G' },
				['H'] = new Vertex<char, int> { Value = 'H' }
			};

			var edges = new[]
			{
				new Edge<char,int> (vertices['A'], vertices['B'], 1),
				new Edge<char,int> (vertices['A'], vertices['C'], 2),
				new Edge<char,int> (vertices['A'], vertices['D'], 5),
				new Edge<char,int> (vertices['B'], vertices['C'], 4),
				new Edge<char,int> (vertices['E'], vertices['F'], 6),
				new Edge<char,int> (vertices['G'], vertices['H'], 7),
			};

			foreach (var vertexByValue in vertices)
			{
				var edgesForValue = edges.Where(e => e.Beginning.Value == vertexByValue.Key);
				vertexByValue.Value.Edges.AddRange(edgesForValue);
			}

			_graph.Vertices.AddRange(vertices.Values);
		}

		[TestMethod]
		public void AmountBlobs_3()
		{
			var processor = new ConnectedComponentsBfs<char, int>();

			var blobs = processor.Blobs(_graph);

			blobs.Count.Should().Be(3);
		}
	}
}
