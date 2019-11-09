using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace SkiaSharpBenchmarks
{
	[SimpleJob(RuntimeMoniker.Net472, baseline: true)]
	// [SimpleJob(RuntimeMoniker.NetCoreApp30)]
	public class SystemBenchmarks
	{
		private Bitmap bitmap;
		private Bitmap surface;
		private Graphics canvas;

		[Params(1000)]
		public int N;

		[Params(0, 90, 180, 20)]
		public int Rotation;

		[Params(
			InterpolationMode.Default, InterpolationMode.Low, InterpolationMode.High,
			InterpolationMode.Bilinear, InterpolationMode.Bicubic, InterpolationMode.NearestNeighbor,
			InterpolationMode.HighQualityBilinear, InterpolationMode.HighQualityBicubic)]
		public InterpolationMode InterpolationMode;

		[Params(
			SmoothingMode.Default, SmoothingMode.None,
			SmoothingMode.HighSpeed, SmoothingMode.HighQuality,
			SmoothingMode.AntiAlias)]
		public SmoothingMode SmoothingMode;

		[GlobalSetup]
		public void Setup()
		{
			using var client = new HttpClient();
			var data = client.GetByteArrayAsync("https://via.placeholder.com/450").Result;

			using var stream = new MemoryStream(data);
			bitmap = new Bitmap(stream);
			surface = new Bitmap(512, 512, PixelFormat.Format32bppArgb);
			canvas = Graphics.FromImage(surface);
		}

		[IterationSetup]
		public void IterationSetup()
		{
			canvas.InterpolationMode = InterpolationMode;
			canvas.SmoothingMode = SmoothingMode;

			var matrix = new Matrix();
			matrix.Rotate(Rotation);
			canvas.Transform = matrix;
		}

		[Benchmark]
		public void DrawImage()
		{
			canvas.DrawImage(bitmap, new PointF(10, 10));
		}

		[Benchmark]
		public void DrawImageUnscaled()
		{
			canvas.DrawImageUnscaled(bitmap, new Point(10, 10));
		}
	}
}
