using Algorithms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class SchedulingWeightedUnitTests
	{
		[TestMethod]
		public void OrderJobs_SimpleCase ()
		{
			var data = new[]
			{
				new Job {Length = 3, Weight = 1},
				new Job {Length = 2, Weight = 2},
				new Job {Length = 1, Weight = 3},
			};

			var scheduledData = SchedulingWeighted.OrderJobs(data);

			scheduledData.Select(job => job.Length)
				.Should()
				.BeInAscendingOrder("for the case optimal solution is one with length ordered as 1,2,3");
		}
		[TestMethod]
		public void ScoreViaBruteForce_SimpleCase ()
		{
			var data = new[]
			{
				new Job {Length = 3, Weight = 1},
				new Job {Length = 2, Weight = 2},
				new Job {Length = 1, Weight = 3},
			};

			var score = SchedulingWeighted.ScoreViaBruteForce(data);

			score.Should().Be(15);
		}

		[TestMethod]
		public void GeneratePermutations_SimpleCase ()
		{
			var data = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };

			var permutations = SchedulingWeighted.GeneratePermutations(data).Distinct().ToList();

			permutations.Count.Should().Be(3628800, "permutations amount is a source sequence length factorial");
		}

		[TestMethod]
		public void OrderJobs_StressTest_ShouldFindOptimalSolution ()
		{
			//acquire
			var sourceData = File.ReadLines(@"resources\jobs.txt").Skip(1)
				.Select(line =>
				{
					var parts = line.Split(' ');
					return new Job { Weight = int.Parse(parts[0]), Length = int.Parse(parts[1]) };
				}).ToList();

			//act
			var byDifference = sourceData.OrderByDescending(j => j.Weight - j.Length)
										.ThenByDescending(j => j.Weight)
										.ToList();
			var scoreForDiff = SchedulingWeighted.OverallCompletionScore(byDifference);

			var byRatio = sourceData.OrderByDescending(j => (double) j.Weight / j.Length).ToList();
			var scoreForRatio = SchedulingWeighted.OverallCompletionScore(byRatio);

			//assert
			scoreForRatio.Should().BeLessThan(scoreForDiff);
		}
	}
}
