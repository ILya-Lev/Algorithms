using Algorithms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class StronglyConnectedComponentsUnitTests
	{
		private Graph<char, int> _graph;

		[TestInitialize]
		public void Initialize()
		{
			var vertices = new[]
			{
				new Vertex<char,int> {Value = 'A'},
				new Vertex<char,int> {Value = 'B'},
				new Vertex<char,int> {Value = 'C'},
				new Vertex<char,int> {Value = 'D'},
				new Vertex<char,int> {Value = 'E'},
				new Vertex<char,int> {Value = 'F'},
				new Vertex<char,int> {Value = 'G'},
				new Vertex<char,int> {Value = 'H'},
				new Vertex<char,int> {Value = 'I'},
				new Vertex<char,int> {Value = 'J'},
				new Vertex<char,int> {Value = 'K'}
			}.ToDictionary(v => v.Value, v => v);

			var edges = new[]
			{
				new {Beinging = 'A', Ending = 'B'},
				new {Beinging = 'B', Ending = 'C'},
				new {Beinging = 'C', Ending = 'A'},

				new {Beinging = 'B', Ending = 'D'},
				new {Beinging = 'D', Ending = 'E'},

				new {Beinging = 'E', Ending = 'F'},
				new {Beinging = 'F', Ending = 'G'},
				new {Beinging = 'G', Ending = 'E'},

				new {Beinging = 'C', Ending = 'H'},
				new {Beinging = 'C', Ending = 'I'},

				new {Beinging = 'H', Ending = 'I'},
				new {Beinging = 'I', Ending = 'J'},
				new {Beinging = 'I', Ending = 'K'},
				new {Beinging = 'J', Ending = 'K'},
				new {Beinging = 'K', Ending = 'H'},

				new {Beinging = 'I', Ending = 'G'},
				new {Beinging = 'J', Ending = 'F'},
			}.Select(item => new Edge<char, int>(vertices[item.Beinging], vertices[item.Ending], 1))
			.GroupBy(edge => edge.Beginning.Value)
			.ToDictionary(g => g.Key, g => g);

			//as there are a few edges per one vertex - group all edges by vertex value
			//to make search faster place each group into one value cell of a dictionary

			foreach (var value in vertices.Keys)
			{
				vertices[value].Edges.AddRange(edges[value]);
			}

			_graph = new Graph<char, int>();
			_graph.Vertices.AddRange(vertices.Values);
		}

		[TestMethod]
		public void Discover_ShouldFind4SCC()
		{
			var sccProcessor = new StronglyConnectedComponents<char, int>();
			var result = sccProcessor.Discover(_graph);
			result.Count.Should().Be(4);
		}
	}
}
