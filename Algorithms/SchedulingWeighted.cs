using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithms
{
	public static class SchedulingWeighted
	{
		public static IList<Job> OrderJobs (IList<Job> source) =>
			//sequence.OrderBy(job => job.Length)
			//		.ThenByDescending(job => job.Weight)
			source.OrderByDescending(job => (double) job.Weight / job.Length)
					.ToList();

		public static long ScoreViaBruteForce (IList<Job> source)
		{
			var permutations = GeneratePermutations(source).ToList();
			var scoredPermutations = permutations.Select(p => new
			{
				Score = OverallCompletionScore(p),
				Permutation = p
			})
			.OrderBy(item => item.Score)
			.ToList();

			return scoredPermutations.First().Score;
		}

		public static long OverallCompletionScore (IList<Job> source)
		{
			long score = 0;
			long completionTime = 0;
			foreach (var job in source)
			{
				completionTime += job.Length;
				score += completionTime * job.Weight;
			}
			return score;
		}

		public static IEnumerable<IList<T>> GeneratePermutations<T> (IList<T> source, int begin = 0)
		{
			if (begin >= source.Count)
				yield return source;

			for (int pivot = begin; pivot < source.Count; pivot++)
			{
				var currentPermutation = new List<T>(source);
				Swap(currentPermutation, begin, pivot);
				//yield return currentPermutation;

				var nextPermutation = GeneratePermutations(currentPermutation, begin + 1);
				foreach (var permutation in nextPermutation)
				{
					yield return permutation;
				}
			}
		}

		private static void Swap<T> (IList<T> previousStep, int begin, int pivot)
		{
			var tmp = previousStep[begin];
			previousStep[begin] = previousStep[pivot];
			previousStep[pivot] = tmp;
		}
	}

	[DebuggerDisplay("L = {Length}; W = {Weight}")]
	public class Job
	{
		public int Length { get; set; }
		public int Weight { get; set; }
	}
}
