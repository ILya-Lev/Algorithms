using Algorithms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class PrimesMstUnitTests
	{
		[TestMethod]
		public void TotalCost_ToyGraph_ShouldFindOptimalSolution ()
		{
			var graph = InitializeToyGraph();

			var cost = graph.TotalCost();

			cost.Should().Be(7);
		}

		private static DirectedGraph<char> InitializeToyGraph ()
		{
			var vertices = new[]
			{
				new Vertex<char, int> {Value = 'A'},
				new Vertex<char, int> {Value = 'B'},
				new Vertex<char, int> {Value = 'C'},
				new Vertex<char, int> {Value = 'D'},
			}.ToDictionary(item => item.Value, item => item);

			var edges = new[]
			{
				new Edge<char, int>(vertices['A'], vertices['B'], 2),
				new Edge<char, int>(vertices['A'], vertices['D'], 1),
				new Edge<char, int>(vertices['B'], vertices['D'], 3),
				new Edge<char, int>(vertices['D'], vertices['C'], 5),
				new Edge<char, int>(vertices['C'], vertices['B'], 4),
			};

			var graph = new DirectedGraph<char>();
			graph.Vertices.UnionWith(vertices.Values);

			foreach (var pair in edges.GroupBy(e => e.Beginning).ToDictionary(g => g.Key, g => g))
				graph.OutgoingEdges.Add(pair.Key, pair.Value);

			foreach (var pair in edges.GroupBy(e => e.Ending).ToDictionary(g => g.Key, g => g))
				graph.IncomingEdges.Add(pair.Key, pair.Value);

			return graph;
		}

		[TestMethod]
		public void TotalCost_StreesGraph_ShouldFindOptimalSolution ()
		{
			var graph = InitializeStreesGraph();

			var cost = graph.TotalCost();

			Console.WriteLine(cost);
		}

		private static DirectedGraph<int> InitializeStreesGraph ()
		{
			var vertices = new Dictionary<int, Vertex<int, int>>();
			var edges = File.ReadLines(@"resources\PrimesMST.txt").Skip(1)
				.Select(line =>
				{
					var parts = line.Split(' ')
									.Select(p => int.Parse(p, NumberStyles.AllowLeadingSign))
									.ToList();

					if (!vertices.ContainsKey(parts[0]))
						vertices.Add(parts[0], new Vertex<int, int> { Value = parts[0] });
					var beginning = vertices[parts[0]];

					if (!vertices.ContainsKey(parts[1]))
						vertices.Add(parts[1], new Vertex<int, int> { Value = parts[1] });
					var ending = vertices[parts[1]];

					return new Edge<int, int>(beginning, ending, parts[2]);
				}).ToList();

			var graph = new DirectedGraph<int>();
			graph.Vertices.UnionWith(vertices.Values);

			foreach (var pair in edges.GroupBy(e => e.Beginning).ToDictionary(g => g.Key, g => g))
				graph.OutgoingEdges.Add(pair.Key, pair.Value);

			foreach (var pair in edges.GroupBy(e => e.Ending).ToDictionary(g => g.Key, g => g))
				graph.IncomingEdges.Add(pair.Key, pair.Value);

			return graph;
		}
	}
}
