using System;
using System.IO;
using BenchmarkDotNet.Attributes;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using SkiaSharp;

namespace SkiaSharpBenchmarks
{
	public class FillGradient
	{
		[Params(1024)]
		public int CanvasSize;

		[Benchmark(Baseline = true, Description = "System.Drawing Fill Gradient")]
		public void DrawGradientWithSystemDrawing()
		{
			using (var destination = new Bitmap(CanvasSize, CanvasSize))
			using (var graphics = Graphics.FromImage(destination))
			{
				graphics.InterpolationMode = InterpolationMode.Default;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;

				var rect = new RectangleF(0, 0, CanvasSize, CanvasSize);
				var brush = new LinearGradientBrush(
					rect,
					Color.Black, Color.White,
					LinearGradientMode.Vertical);

				graphics.FillRectangle(brush, rect);

				graphics.Flush();
			}
		}

		[Benchmark(Description = "SkiaSharp Fill Gradient")]
		public void DrawGradientWithSkiaSharp()
		{
			using (var surface = SKSurface.Create(new SKImageInfo(CanvasSize, CanvasSize)))
			{
				var canvas = surface.Canvas;

				var rect = new SKRect(0, 0, CanvasSize, CanvasSize);
				var paint = new SKPaint
				{
					IsAntialias = true,
					Shader = SKShader.CreateLinearGradient(
						SKPoint.Empty, new SKPoint(0, CanvasSize),
						new [] { SKColors.Black, SKColors.White }, new [] { 0.0f, 1.0f },
						SKShaderTileMode.Clamp)
				};

				canvas.DrawPaint(paint);

				canvas.Flush();
			}
		}
	}
}
