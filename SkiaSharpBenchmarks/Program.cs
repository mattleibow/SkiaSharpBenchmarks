using BenchmarkDotNet.Running;

namespace SkiaSharpBenchmarks
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
		}
	}
}
