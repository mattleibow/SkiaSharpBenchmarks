# SkiaSharp Benchmarks

``` ini

BenchmarkDotNet=v0.11.3, OS=ubuntu 16.04
Intel Xeon CPU E5-2673 v4 2.30GHz, 1 CPU, 2 logical and 2 physical cores
.NET Core SDK=2.2.103
  [Host]     : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

```

|                     Method |     N | CanvasSize |       Mean |    Error |    StdDev | Ratio | Rank |
|--------------------------- |------ |----------- |-----------:|---------:|----------:|------:|-----:|
| &#39;System.Drawing Draw Path&#39; | 10000 |       1024 | 5,159.6 ms | 56.54 ms | 52.891 ms |  1.00 |    2 |
|      &#39;SkiaSharp Draw Path&#39; | 10000 |       1024 |   625.5 ms | 11.03 ms |  9.774 ms |  0.12 |    1 |


|                      Method |     N | CanvasSize | Scale |     Mean |     Error |    StdDev | Ratio | RatioSD | Rank |
|---------------------------- |------ |----------- |------ |---------:|----------:|----------:|------:|--------:|-----:|
| **&#39;System.Drawing Draw Image&#39;** | **10000** |       **1024** |   **0.3** | **44.31 ms** | **1.2944 ms** | **1.2108 ms** |  **1.00** |    **0.00** |    **1** |
|      &#39;SkiaSharp Draw Image&#39; | 10000 |       1024 |   0.3 | 78.20 ms | 1.3076 ms | 1.2231 ms |  1.77 |    0.05 |    2 |
|                             |       |            |       |          |           |           |       |         |      |
| **&#39;System.Drawing Draw Image&#39;** | **10000** |       **1024** |   **0.5** | **34.16 ms** | **0.8972 ms** | **0.8393 ms** |  **1.00** |    **0.00** |    **1** |
|      &#39;SkiaSharp Draw Image&#39; | 10000 |       1024 |   0.5 | 79.59 ms | 1.5554 ms | 2.3281 ms |  2.33 |    0.09 |    2 |
|                             |       |            |       |          |           |           |       |         |      |
| **&#39;System.Drawing Draw Image&#39;** | **10000** |       **1024** |     **1** | **33.51 ms** | **0.6374 ms** | **0.5962 ms** |  **1.00** |    **0.00** |    **1** |
|      &#39;SkiaSharp Draw Image&#39; | 10000 |       1024 |     1 | 81.62 ms | 0.9030 ms | 0.8447 ms |  2.44 |    0.04 |    2 |
|                             |       |            |       |          |           |           |       |         |      |
| **&#39;System.Drawing Draw Image&#39;** | **10000** |       **1024** |     **2** | **34.25 ms** | **0.6363 ms** | **0.5952 ms** |  **1.00** |    **0.00** |    **1** |
|      &#39;SkiaSharp Draw Image&#39; | 10000 |       1024 |     2 | 79.66 ms | 1.5399 ms | 1.5124 ms |  2.33 |    0.05 |    2 |
|                             |       |            |       |          |           |           |       |         |      |
| **&#39;System.Drawing Draw Image&#39;** | **10000** |       **1024** |     **3** | **33.53 ms** | **0.6645 ms** | **0.7110 ms** |  **1.00** |    **0.00** |    **1** |
|      &#39;SkiaSharp Draw Image&#39; | 10000 |       1024 |     3 | 77.23 ms | 1.1672 ms | 1.0918 ms |  2.29 |    0.06 |    2 |


To run, just execute this command

```
> dotnet run -c Release -p SkiaSharpBenchmarks
```

## Windows

Windows should work out the box.

## macOS

To run on macOS, you may have to install the `libgdiplus` library.

```
brew install mono-libgdiplus
```

## Linux

To run on Linux, you may have to install the `libgdiplus` library.
