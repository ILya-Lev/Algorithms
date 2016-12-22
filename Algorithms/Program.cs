using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Algorithms
{
	public static class Program
	{
		static void Main(string[] args)
		{
			int[] unsorted = { 3, 9, 2, 1, 8, 5, 8, 3, 2, 7, 1, 9, 6, 0 };
			Print(unsorted);

			var sorted = MergeSort<int>.SortArray(unsorted);
			Print(sorted);
			Print(unsorted);

			var str = @";lnasd!$#!#%$%^&)*&*(_*?></.,|\][}{(_+(=-+_)+_)(~!@!~@~!$~%$%#$^@^%?<>?MV<ZMCV<ALGA";

			Console.WriteLine(str);
			Console.WriteLine(GenerateValidFileName(str));
		}

		private static void Print<T>(T[] array)
		{
			foreach (var item in array)
			{
				Console.Write(item.ToString() + " ");
				Debug.Write(item.ToString() + " ");
			}

			Console.WriteLine();
			Debug.WriteLine("\n");
		}

		private static string GenerateValidFileName(string name)
		{
			var invalidChars = Path.GetInvalidFileNameChars().ToLookup(c => c);

			var filterredName = $"{name}.pdf".ToCharArray()
								.Where(c => !invalidChars.Contains(c)).ToArray();
			return new string(filterredName);
		}

	}
}
