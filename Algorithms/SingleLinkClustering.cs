using MoreLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Algorithms
{
	public class SingleLinkClustering
	{
		/// <summary>
		/// returns k-1 most far points; where k = clustersAmount
		/// </summary>
		public List<Point> DefineClusters(List<Point> point, int clustersAmount)
		{
			var distances = DefineDistances(point).AsParallel().ToList();
			distances.Sort((lhs, rhs) => lhs.Length.CompareTo(rhs.Length));
			return distances.TakeLast(clustersAmount - 1)
							.SelectMany(d => new Point[] { d.From, d.To })
							.Distinct()
							.ToList();
		}

		private IEnumerable<Distance> DefineDistances(List<Point> points)
		{
			for (int i = 0; i < points.Count; i++)
				for (int j = i + 1; j < points.Count; j++)
				{
					var distance = EvaluateDistance(points[i], points[j]);
					yield return new Distance
					{
						From = points[i],
						To = points[j],
						Length = distance
					};
				}
		}

		private double EvaluateDistance(Point from, Point to)
			=> Math.Sqrt((@from.X - to.X) * (@from.X - to.X)
					   + (@from.Y - to.Y) * (@from.Y - to.Y));

		private class Distance
		{
			public Point From { get; set; }
			public Point To { get; set; }
			public double Length { get; set; }
		}
	}
}
