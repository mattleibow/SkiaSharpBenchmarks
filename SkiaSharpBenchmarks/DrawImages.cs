using System;
using System.IO;
using BenchmarkDotNet.Attributes;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using SkiaSharp;

namespace SkiaSharpBenchmarks
{
	public class DrawImage
	{
		private Image systemImage;
		private SKImage skiaImage;

		[Params(10000)]
		public int N;

		[Params(1024)]
		public int CanvasSize;

		[Params(1.0f, 0.5f, 2.0f, 0.3f, 3.0f)]
		public float Scale;

		[GlobalSetup]
		public void Setup()
		{
			systemImage = Image.FromFile("skia_1024x1024.png");
			skiaImage = SKImage.FromEncodedData("skia_1024x1024.png");
		}

		[Benchmark(Baseline = true, Description = "System.Drawing Draw Image")]
		public void DrawImageWithSystemDrawing()
		{
			using (var destination = new Bitmap(CanvasSize, CanvasSize))
			using (var graphics = Graphics.FromImage(destination))
			{
				graphics.InterpolationMode = InterpolationMode.Default;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;

				var r = new RectangleF(0, 0, systemImage.Width * Scale, systemImage.Height * Scale);
				graphics.DrawImage(systemImage, r);

				graphics.Flush();
			}
		}

		[Benchmark(Description = "SkiaSharp Draw Image")]
		public void DrawImageWithSkiaSharp()
		{
			using (var surface = SKSurface.Create(new SKImageInfo(CanvasSize, CanvasSize)))
			{
				var canvas = surface.Canvas;

				var paint = new SKPaint
				{
					IsAntialias = true,
					FilterQuality = SKFilterQuality.High
				};

				var r = new SKRect(0, 0, skiaImage.Width * Scale, skiaImage.Height * Scale);
				canvas.DrawImage(skiaImage, r);

				canvas.Flush();
			}
		}
	}
}
