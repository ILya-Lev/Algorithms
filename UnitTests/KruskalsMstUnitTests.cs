using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class KruskalsMstUnitTests
	{
		private Graph<char, int> _graph;

		[TestInitialize]
		public void Initialize()
		{
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

			_graph = new Graph<char, int>();
			_graph.Vertices.AddRange(vertices.Values);
		}

		[TestMethod]
		public void GenerateSpaninTree_Analyze()
		{
			var spanningTree = KruskalsMst.GenerateSpaninTree(_graph);
			var totalCost = spanningTree.TotalCost(m => m);
			Console.WriteLine(totalCost);
		}
	}
}
