using System;
using SkiaSharp;
using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace SkiaSharpBenchmarks
{
	public class DelegatesBenchmarks
	{
		private const int SIZE = 1000;

		private IntPtr memory;
		private SKData[] array;

		[Params(1, 10, 100, 1_000, 1_000_000)]
		public int N;

		[GlobalSetup]
		public void Setup()
		{
			memory = Marshal.AllocCoTaskMem(SIZE);
			array = new SKData[N];
		}

		[GlobalCleanup]
		public void Cleanup()
		{
			Marshal.FreeCoTaskMem(memory);
		}

		[Benchmark]
		public void NoDelegate()
		{
			for (var i = 0; i < N; i++)
				array[i] = SKData.Create(memory, SIZE);

			for (var i = 0; i < N; i++)
				array[i].Dispose();
		}

		[Benchmark]
		public void NoContextAnonymousDelegate()
		{
			for (var i = 0; i < N; i++)
				array[i] = SKData.Create(memory, SIZE, (a, b) => { });

			for (var i = 0; i < N; i++)
				array[i].Dispose();
		}

		[Benchmark]
		public void WithContextAnonymousDelegate()
		{
			for (var i = 0; i < N; i++)
				array[i] = SKData.Create(memory, SIZE, (a, b) => { }, "CONTEXT");

			for (var i = 0; i < N; i++)
				array[i].Dispose();
		}

		[Benchmark]
		public void NoContextMethodDelegate()
		{
			for (var i = 0; i < N; i++)
				array[i] = SKData.Create(memory, SIZE, DestroyMethod, "CONTEXT");

			for (var i = 0; i < N; i++)
				array[i].Dispose();
		}

		[Benchmark]
		public void WithContextMethodDelegate()
		{
			for (var i = 0; i < N; i++)
				array[i] = SKData.Create(memory, SIZE, DestroyMethod, "CONTEXT");

			for (var i = 0; i < N; i++)
				array[i].Dispose();
		}

		public void DestroyMethod(IntPtr address, object context)
		{
		}
	}
}
