using Algorithms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class IteratorUnitTests
	{
		[TestMethod]
		public void IteratorAsMutableStructure_Loop_WeirdBehavior()
		{
			var x = new { Items = new List<int> { 1, 2, 3, 4 }.GetEnumerator() };
			while (x.Items.MoveNext())
			{
				Debug.WriteLine(x.Items);
			}
		}

		[TestMethod]
		public void Buffer_LengthIsDivisivleWithoutReminder_ShoulGoThroughAllElements()
		{
			var scale = 3;
			var bufferSize = 7;
			var length = bufferSize * scale;
			var data = Enumerable.Range(1, length);

			var steps = 0;
			var seenElements = 0;

			foreach (var bunch in data.Buffer(bufferSize))
			{
				steps++;
				foreach (var i in bunch)
				{
					Debug.Write(i + " ");
					seenElements++;
				}
				Debug.WriteLine(" ");
			}

			steps.Should()
				.Be(scale
				, $"source data is exactly '{nameof(scale)}'='{scale}' times larger than buffer");
			seenElements.Should().Be(length, "should go through all elements of the source");
		}

		[TestMethod]
		public void Buffer_LengthNotDivisivleWithoutReminder_ShoulGoThroughAllElements()
		{
			var scale = 3;
			var bufferSize = 7;
			var length = bufferSize * scale + bufferSize - 2;
			var data = Enumerable.Range(1, length);

			var steps = 0;
			var seenElements = 0;

			foreach (var bunch in data.Buffer(bufferSize))
			{
				steps++;
				foreach (var i in bunch)
				{
					Debug.Write(i + " ");
					seenElements++;
				}
				Debug.WriteLine(" ");
			}

			steps.Should()
				.Be(scale + 1, $"source data is exactly '{nameof(scale)}' + 1 ='{scale + 1}' times larger than buffer");
			seenElements.Should().Be(length, "should go through all elements of the source");
		}
	}
}
