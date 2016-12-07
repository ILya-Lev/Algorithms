using Algorithms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class BfsUnitTests
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
				['F'] = new Vertex<char, int> { Value = 'F' }
			};

			var edges = new[]
			{
				new Edge<char,int> (vertices['A'], vertices['B'], 1),
				new Edge<char,int> (vertices['A'], vertices['C'], 2),
				new Edge<char,int> (vertices['A'], vertices['D'], 5),
				new Edge<char,int> (vertices['B'], vertices['C'], 4),
				new Edge<char,int> (vertices['B'], vertices['E'], 2),
				new Edge<char,int> (vertices['B'], vertices['F'], 9),
				new Edge<char,int> (vertices['C'], vertices['D'], 3),
				new Edge<char,int> (vertices['C'], vertices['E'], 7),
				new Edge<char,int> (vertices['E'], vertices['F'], 6),
			};

			foreach (var vertexByValue in vertices)
			{
				var edgesForValue = edges.Where(e => e.Beginning.Value == vertexByValue.Key);
				vertexByValue.Value.Edges.AddRange(edgesForValue);
			}

			_graph.Vertices.AddRange(vertices.Values);
		}

		[TestMethod]
		public void Path_A_F()
		{
			var start = _graph.Vertices.FirstOrDefault(v => v.Value == 'A');
			var end = _graph.Vertices.FirstOrDefault(v => v.Value == 'F');

			var path = new BFS<char, int>().FindPath(_graph, start, end);

			var output = string.Join("->", path.Select(v => v.Value));

			Console.WriteLine(output);
			output.Should().Be("A->B->F");
		}
	}
}
