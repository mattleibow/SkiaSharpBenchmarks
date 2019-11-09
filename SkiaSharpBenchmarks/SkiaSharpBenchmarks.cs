using System.Net.Http;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using SkiaSharp;

namespace SkiaSharpBenchmarks
{
	[SimpleJob(RuntimeMoniker.Net472, baseline: true)]
	[SimpleJob(RuntimeMoniker.NetCoreApp30)]
	public class SkiaSharpBenchmarks
	{
		private SKBitmap bitmap;
		private SKImage image;

		private SKBitmap surface;
		private SKCanvas canvas;
		private SKPaint paint;

		[Params(1000, 10000)]
		public int N;

		[Params(0, 90, 180, 270, 360, 20, 45)]
		public int Rotation;

		[Params(
			SKFilterQuality.None,
			SKFilterQuality.Low, SKFilterQuality.Medium, SKFilterQuality.High)]
		public SKFilterQuality Quality;

		[Params(true, false)]
		public bool AntiAlias;

		[GlobalSetup]
		public void Setup()
		{
			using var client = new HttpClient();
			var data = client.GetByteArrayAsync("https://via.placeholder.com/450").Result;

			bitmap = SKBitmap.Decode(data);
			image = SKImage.FromEncodedData(data);
			surface = new SKBitmap(512, 512);
			canvas = new SKCanvas(surface);
			paint = new SKPaint();
		}

		[IterationSetup]
		public void IterationSetup()
		{
			paint.FilterQuality = Quality;
			paint.IsAntialias = AntiAlias;

			canvas.ResetMatrix();
			canvas.RotateDegrees(Rotation);
		}

		[Benchmark]
		public void DrawBitmap()
		{
			canvas.DrawBitmap(bitmap, new SKPoint(10, 10), paint);
		}

		[Benchmark]
		public void DrawImage()
		{
			canvas.DrawImage(image, new SKPoint(10, 10), paint);
		}
	}
}
