using System;
using System.Collections.Generic;
using System.Linq;

namespace CostSuite.Core.Common;

public readonly record struct Point2D(double X, double Y)
{
    public static Point2D operator +(Point2D a, Point2D b) => new(a.X + b.X, a.Y + b.Y);
    public static Point2D operator -(Point2D a, Point2D b) => new(a.X - b.X, a.Y - b.Y);
}

public readonly record struct Line2D(Point2D Start, Point2D End)
{
    public double Length => Math.Sqrt(Math.Pow(End.X - Start.X, 2) + Math.Pow(End.Y - Start.Y, 2));
}

public readonly record struct BoundingBox2D(Point2D Min, Point2D Max)
{
    public double Width => Max.X - Min.X;
    public double Height => Max.Y - Min.Y;
}

public class Polyline2D : List<Point2D>
{
    public Polyline2D() { }

    public Polyline2D(IEnumerable<Point2D> points) : base(points) { }

    public double Length()
    {
        double length = 0;
        for (int i = 1; i < Count; i++)
        {
            length += new Line2D(this[i - 1], this[i]).Length;
        }

        return length;
    }

    public BoundingBox2D BoundingBox()
    {
        double minX = this.Min(p => p.X);
        double minY = this.Min(p => p.Y);
        double maxX = this.Max(p => p.X);
        double maxY = this.Max(p => p.Y);
        return new(new(minX, minY), new(maxX, maxY));
    }
}

public class Polygon2D
{
    public IReadOnlyList<Point2D> Outer { get; }
    public IReadOnlyList<IReadOnlyList<Point2D>> Holes { get; }

    public Polygon2D(IEnumerable<Point2D> outer, IEnumerable<IEnumerable<Point2D>>? holes = null)
    {
        Outer = outer.ToList();
        Holes = holes?.Select(h => (IReadOnlyList<Point2D>)h.ToList()).ToList()
            ?? new List<IReadOnlyList<Point2D>>();
    }

    private static double SignedArea(IReadOnlyList<Point2D> pts)
    {
        double sum = 0;
        for (int i = 0; i < pts.Count; i++)
        {
            var a = pts[i];
            var b = pts[(i + 1) % pts.Count];
            sum += (a.X * b.Y) - (b.X * a.Y);
        }

        return sum / 2d;
    }

    public double Area
    {
        get
        {
            double area = Math.Abs(SignedArea(Outer));
            foreach (var hole in Holes)
            {
                area -= Math.Abs(SignedArea(hole));
            }

            return area;
        }
    }

    public BoundingBox2D BoundingBox()
    {
        var all = Outer.Concat(Holes.SelectMany(h => h)).ToList();
        double minX = all.Min(p => p.X);
        double minY = all.Min(p => p.Y);
        double maxX = all.Max(p => p.X);
        double maxY = all.Max(p => p.Y);
        return new(new(minX, minY), new(maxX, maxY));
    }
}

