using BenchmarkDotNet.Running;

namespace SkiaSharpBenchmarks
{
	public class Program
	{
		public static void Main(string[] args) => BenchmarkRunner.Run(typeof(Program).Assembly);
	}
}
