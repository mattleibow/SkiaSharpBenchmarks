using System;
using System.IO;
using BenchmarkDotNet.Attributes;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using SkiaSharp;

namespace SkiaSharpBenchmarks
{
	[RPlotExporter, RankColumn]
	public class DrawLines
	{
		private PointF[] systemPoints;
		private SKPoint[] skiaPoints;

		[Params(10000)]
		public int N;

		[Params(1024)]
		public int CanvasSize;

		[GlobalSetup]
		public void Setup()
		{
			systemPoints = new PointF[N];
			skiaPoints = new SKPoint[N];

			var rnd = new Random(42);
			for (var i = 0; i < N; i++)
			{
				var x = (float)rnd.NextDouble() * CanvasSize;
				var y = (float)rnd.NextDouble() * CanvasSize;

				systemPoints[i] = new PointF(x, y);
				skiaPoints[i] = new SKPoint(x, y);
			}
		}

		[Benchmark(Baseline = true, Description = "System.Drawing Draw Path")]
		public void DrawPathWithSystemDrawing()
		{
			using (var destination = new Bitmap(CanvasSize, CanvasSize))
			using (var graphics = Graphics.FromImage(destination))
			{
				graphics.InterpolationMode = InterpolationMode.Default;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;

				var pen = new Pen(Color.HotPink, 10);

				var path = new GraphicsPath();
				path.AddLines(systemPoints);

				graphics.DrawPath(pen, path);

				using (var ms = new MemoryStream())
				{
					destination.Save(ms, ImageFormat.Png);
				}
			}
		}

		[Benchmark(Description = "SkiaSharp Draw Path")]
		public void DrawPathWithSkiaSharp()
		{
			using (var surface = SKSurface.Create(new SKImageInfo(CanvasSize, CanvasSize)))
			{
				var canvas = surface.Canvas;

				var paint = new SKPaint
				{
					IsAntialias = true,
					Color = SKColors.HotPink,
					StrokeWidth = 10,
					Style = SKPaintStyle.Stroke
				};

				var path = new SKPath();
				path.AddPoly(skiaPoints);

				canvas.DrawPath(path, paint);

				using (var ms = new MemoryStream())
				{
					surface.Snapshot().Encode(SKEncodedImageFormat.Png, 100).SaveTo(ms);
				}
			}
		}
	}
}
