using Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class SccAssignmentUnitTests
	{
		private Graph<int, int> _graph;

		[TestInitialize]
		public void Initialize()
		{
			var vertices = new Dictionary<int, Vertex<int, int>>();

			var edges = File.ReadLines(@".\resources\scc.txt").Select(line =>
			{
				var values = line.Split(' ');
				return new { Beginning = int.Parse(values[0]), Ending = int.Parse(values[1]) };
			}).Select(item =>
			{
				if (!vertices.ContainsKey(item.Beginning))
					vertices.Add(item.Beginning, new Vertex<int, int> { Value = item.Beginning });
				if (!vertices.ContainsKey(item.Ending))
					vertices.Add(item.Ending, new Vertex<int, int> { Value = item.Ending });

				return new Edge<int, int>(vertices[item.Beginning], vertices[item.Ending], 1);
			}).GroupBy(edge => edge.Beginning.Value)
				.ToDictionary(g => g.Key);

			foreach (var key in vertices.Keys.Where(k => edges.ContainsKey(k)))
			{
				vertices[key].Edges.AddRange(edges[key]);
			}

			_graph = new Graph<int, int>();
			_graph.Vertices.AddRange(vertices.Values);
		}

		[TestMethod]
		public void Discover_ShouldFind5LargestSCC()
		{
			var sccProcessor = new StronglyConnectedComponents<int, int>();
			var result = sccProcessor.Discover(_graph);
			var biggestSizes = result.Select(g => g.Count())
										.OrderByDescending(c => c)
										.Take(5).ToList();
			biggestSizes.ForEach(Console.WriteLine);
		}
	}
}
